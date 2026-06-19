using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    const float DAY_TIME = 0.15f;//太阳升起
    const float NIGHT_TIME = 0.85f;//太阳落下
    const float NIGHTLIGHT_INTENSITY = 2f;//晚上灯光亮度
    const float NIGHTLIGHT_DAMP = 0.05f;//昼夜灯光过渡时间

    [SerializeField] private Light sunLight;//太阳光

    public float dayLength = 10;//一天长度
    public bool autoUpdate = false;
    [Range(0, 1)]
    public float timeOfDay = 0;//0-1，代表当前一天内的时间
    public Gradient lightColor;//一天中太阳光的颜色

    private List<Light> nightLights = new List<Light>();//s所有夜灯

    private void Start()
    {
        //找到所有夜灯物体
        Transform nightLightParent = GameObject.Find("NightLights").transform;
        int count = nightLightParent.childCount;
        for(int i = 0; i < count; i++)
        {
            //把每个夜灯加入List
            nightLights.Add(nightLightParent.GetChild(i).GetComponent<Light>());
        }
    }
    private void FixedUpdate()
    {
        if (autoUpdate)
        {
            timeOfDay += Time.fixedDeltaTime / dayLength;//每帧更新当前时间
            if (timeOfDay >= 1)
            {
                timeOfDay = 0;//下一天
            }
        }
        UpdateLighting();
    }
    //根据时间更新场景光照
    private void UpdateLighting()
    {
        sunLight.color = lightColor.Evaluate(timeOfDay);//调整阳光颜色
        Vector3 lightAngle = new Vector3(Mathf.Lerp(-180, -360, timeOfDay), -90, 0);
        sunLight.transform.rotation = Quaternion.Euler(lightAngle);
        //调整每个夜灯的亮度
        for(int i= 0; i < nightLights.Count; i++)
        {
            Light light = nightLights[i];
            //夜晚时 intensity 为NIGHTLIGHT_INTENSITY，白天 intensity 为0
            //Smoothstep:平滑过渡
            light.intensity = (1 - Smoothstep(DAY_TIME - NIGHTLIGHT_DAMP, DAY_TIME + NIGHTLIGHT_DAMP, timeOfDay) *
                Smoothstep(NIGHT_TIME + NIGHTLIGHT_DAMP, NIGHT_TIME - NIGHTLIGHT_DAMP, timeOfDay)) *
                NIGHTLIGHT_INTENSITY;
        }
    }


    private float Smoothstep(float a, float b, float x)
    {
        float t = Mathf.Clamp01((x - a) / (b - a));
        return t * t * (3f - (2f * t));
    }
}
 