 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData
{
    //# 注意一般是 从外部数据库 导入，而不是在这里新建
    public static Dictionary<int, ItemInfo> itemInfoDict = new()
    {
        {0,new ItemInfo()
            {
                id = 0,
                name = "Apple",
                rarity = 2
            } 
        },
        {1,new ItemInfo()
            {
                id = 1,
                name = "Orrangle",
                rarity = 2
            } 
        },
    };
    public static ItemInfo GetItemInfo(int id)
    {
        //这里更好的做法是 在游戏开始时从外部数据库(csv)初始化所有的 itemInfo,
        //并把他们存储在一个数据结构里
        //这样在这个函数里只需要通过id 查询该数据结构即可，不需要创建新的数据结构

        ItemInfo item = itemInfoDict[id];

        return item;
    }

    public static Sprite GetItemSprite(int id)
    {
        return Resources.Load<Sprite>($"item_{id}");
    }
}