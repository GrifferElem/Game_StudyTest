using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager
{
    public static Dictionary<int, Item> items = new();

    /*public void AddItem(Item item)
    {
        if(items.ContainsKey(item.id))
        {
            items[item.id].count += item.count;
        }
        else
        {
            items.Add(item.id, item);//Dictionary 里存的不是一个新的 Item,而是存了 item 这个对象的引用（地址）,items[id] 和 item两个变量都指向同一个对象
              //外部修改了item.count会影响items[id].count
        }
    }*/
    public static void AddItem(int id,int count)
    {
        if (items.ContainsKey(id))
        {
            items[id].count += count;
        }
        else
        {
            items.Add(id, new Item()
            {
                id = id,
                count = count
            });
        }
    }
    public static void RemoveItem(Item item)
    {
        if (items.ContainsKey(item.id))
        {
            items.Remove(item.id);
        }
        else
        {
            return;
        }
    }
    public static void UseItem(Item item)
    {

    }
}
