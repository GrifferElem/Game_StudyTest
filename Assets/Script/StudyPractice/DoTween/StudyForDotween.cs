using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class StudyForDotween : MonoBehaviour
{
    public TMP_Text tmpText;
    public string displayText = "Hello!";
    public float duration = 2f;
    public Image image;
    public GameObject obj;

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Camera.main.DOShakePosition(2f);
            //
            var sequence1 = DOTween.Sequence();
            sequence1.Append(DOTween.To(() => tmpText.text,//Getter：获取当前文本的值（起点）
                x => tmpText.text = x,//Setter：把新的文本值 x 设置给 tmpText.text
                displayText,//最终值（终点）
                duration// 动画时长
                ).SetEase(Ease.Linear)//设置为线性，让字符匀速出现
                );
            sequence1.Append(tmpText.DOColor(Color.red, duration));

            sequence1.OnComplete(() =>
            {
                var sequence2 = DOTween.Sequence().SetLoops(-1);
                sequence2.Append(tmpText.DOFade(0, 0.1f));
                sequence2.Append(tmpText.DOFade(1, 0.1f));
            });
            //
            //obj.transform.DOMove(new Vector3(transform.position.x + 10, transform.position.y,transform.position.z), duration);
            //obj.transform.DOMove(new Vector3(transform.position.x + 10, transform.position.y,transform.position.z), duration).From();
            obj.transform.DOScale(0, duration).From();
            image.DOFade(0, duration).From();
        }
    }
}
