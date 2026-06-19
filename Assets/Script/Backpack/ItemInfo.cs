using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 物品在固有的信息
/// </summary>
public class ItemInfo
{
    public int id;

    public string name;//名字
    //public Sprite icon;//图片，考虑到用excel 时不能放置sprite，所以并不适合在这里插件Sprite 类型
    public int rarity;//稀有度
}

/// <summary>
/// 物品在背包里的信息
/// </summary>
public class Item
{
    public int id;
    public int count = 0;
}