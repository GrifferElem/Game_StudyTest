using SKCell;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HiddenText : MonoBehaviour
{
    private Coroutine currentCoroutine;

    public Transform transformToMove;
    public Vector3 p1, p2;

    private void Start()
    {
        StartProcedure(SKCurve.QuinticDoubleIn, 2.0f, (t) =>
        {
            transformToMove.position = Vector3.Lerp(p1, p2, t);
        });
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            StopProcedure();
        }
    }
    /// <summary>
    /// 在action中，x值会逐渐（线性地）从0变到1，而action中的参数是我们采样的y值
    /// </summary>
    /// <param name="curve"></param>
    /// <param name="time"></param>
    /// <param name="action"></param>
    /// <param name="onComplete"></param>
    /// <param name="id"></param>
    private void StartProcedure(SKCurve curve, float time, Action<float> action, Action onComplete = null, string id = "")
    {
        currentCoroutine = StartCoroutine(StartProcedureCR(curve, time, action, onComplete, id));
    }
    private void StopProcedure()
    {
        StopCoroutine(currentCoroutine);
        currentCoroutine = null;
    }


    private IEnumerator StartProcedureCR(SKCurve curve, float time, Action<float> action, Action onComplete = null, string id = "")
    {
        float deltaTime = Time.fixedDeltaTime; //0.02
        float count = time / deltaTime; //50 * time
        float step = 1 / count;

        float x = 0;
        for (int i = 0; i < count; i++)
        {
            action(SKCurveSampler.SampleCurve(curve, x));
            x += step;
            yield return new WaitForFixedUpdate()
;
        }
        if (onComplete != null)
            onComplete();
    }
}
