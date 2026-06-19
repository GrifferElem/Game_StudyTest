using SKCell;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    public static InventoryUIManager instance;

    private ItemFrame selectedItemFrame;

    [SerializeField] private TMP_Text itemNameText;
    [SerializeField] private GameObject itemFramePrefab;
    [SerializeField] private Transform itemFrameContainer;//为了额能以网格的形式显示需要生成再带有gridLayoutGroup组件的物体下

    private void Awake()
    {
        instance = this;
    }
    /// <summary>
    /// 让 UI 面板显示背包里的所有物品
    /// </summary>
    public void UpdateUIDisplay()
    {
        //保留已有的Child，直接更新信息是更好的方法，这边的方法仅作演示

        //摧毁grid 下所有子物体（之前遗留的 itemFrame)
        itemFrameContainer.ClearChildren();

        foreach(var i in InventoryManager.items)
        {
            ItemFrame itemFrame = Instantiate(itemFramePrefab, itemFrameContainer).GetComponent<ItemFrame>();
            itemFrame.SetItem(i.Value);
            itemFrame.UpdateInfo();
        }
    }

    public void OnClickItemFrame(ItemFrame itemFrame)
    {
        if (selectedItemFrame != null)
        {
            selectedItemFrame.OnUnSelect();
        }
        selectedItemFrame = itemFrame;
        selectedItemFrame.OnSelect();

        int id = itemFrame.GetItem().id;
        itemNameText.text = ItemData.GetItemInfo(id).name;
    }
}
