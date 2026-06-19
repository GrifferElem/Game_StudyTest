using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class DraggerUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 startPos;
    private Vector3 currentPos;
    private Vector2 offset;
    private Vector3 currentTarget;

    public bool isDrag = false;
    public bool isReturn = false;
    public bool isDrop = false;
    public Texture2D cursorImage;
    public DropBox dropBox;
    public RectTransform rectTrans;

    private void Start()
    {
        startPos = rectTrans.anchoredPosition;
        Cursor.SetCursor(cursorImage, Vector2.zero, CursorMode.Auto);
        dropBox.OnObjDrapEvent += OnSnap;
    }
    private void Update()
    {
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            currentTarget = startPos;
            isReturn = true;
            isDrop = false;
        }
        if (isReturn && !isDrag)
        {
            MoveTo(currentTarget);
        }
        if (isDrop && !isDrag)
        {
            MoveTo(currentTarget);
        }
    }

    private void OnSnap(Vector2 vector)
    {
        isReturn = false;
        isDrop = true;
        currentTarget = vector;
    }
    private void MoveTo(Vector3 target)
    {
        rectTrans.anchoredPosition = Vector3.Lerp(rectTrans.anchoredPosition, target, 5f * Time.deltaTime);
        if (Vector3.Distance(target, rectTrans.anchoredPosition) < 0.1f)
        {
            rectTrans.anchoredPosition = target;
            isReturn = false;
            isDrop = false;
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        isDrag = true;
        currentPos = rectTrans.anchoredPosition;
        offset = (Vector2)currentPos - eventData.position;
    }
    public void OnDrag(PointerEventData eventData)
    {
        rectTrans.anchoredPosition = eventData.position+offset;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        isDrag = false; 
        isReturn = true;
        currentTarget = startPos;
    }
}
