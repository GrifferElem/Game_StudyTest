using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropBox : MonoBehaviour, IDropHandler
{
    public event Action<Vector2> OnObjDrapEvent;

    public void OnDrop(PointerEventData eventData)
    {
        OnObjDrapEvent?.Invoke(gameObject.GetComponent<RectTransform>().anchoredPosition);
    }
}
