using DG.Tweening;
using SKCell;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 更新每个背包格子的内容
/// </summary>
public class ItemFrame : MonoBehaviour
{
    private Item item;

    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Image itemIconImage;
    [SerializeField] private TMP_Text itemCountText;


    public void OnClick()
    {
        InventoryUIManager.instance.OnClickItemFrame(this);
    }
    public void SetItem(Item item)
    {
        this.item = item;
    }
    public Item GetItem()
    {
        return this.item;
    }
    public void UpdateInfo()
    {
        itemIconImage.sprite = ItemData.GetItemSprite(item.id);
        itemCountText.text = item.count.ToString();
    }
    public void OnSelect()
    {
        canvasGroup.DOFade(1, 0.5f);
    }
    public void OnUnSelect()
    {
        canvasGroup.DOFade(0, 0.5f);
    }
}
