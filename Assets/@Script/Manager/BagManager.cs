using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagManager
{
    private Dictionary<Define.HeroType, ItemDatas[]> _itemDataDic = new Dictionary<Define.HeroType, ItemDatas[]>();
    public ItemDatas[] GetItemDatas(Define.HeroType hero)
    {
        ItemDatas[] itemDatas = null;
        if (_itemDataDic.TryGetValue(hero, out itemDatas))
            return itemDatas;

        return null;
    }
    public bool AddItem(Define.HeroType type, Item_Base data)
    {
        ItemData itemData = data._data;
        ItemDatas[] datas = null;
        if(_itemDataDic.TryGetValue(type, out datas) == false)
            return false;

        
        //가방에 원래 있는지
        for(int i = 0; i < datas.Length; i++)
        {
            if (datas[i].count == 0)
                continue;
            if (datas[i]._data._data.Type == data._data.Type)
            {
                if (datas[i].count >= itemData.MaxCount)
                    return false;

                datas[i].count++;
                _itemDataDic[type] = datas;

                MainCanvas main = Manager.UI.SceneUI as MainCanvas;
                main.SlotRefresh();
                return true;
            }
        }

        //없으면 추가하기
        for(int i = 0; i < datas.Length; i++)
        {
            if (datas[i].count == 0)
            {
                ItemDatas newItem = new ItemDatas() { _data = data, count = 1, itemAction = data.ItemAblity };
                datas[i] = newItem;
                _itemDataDic[type] = datas;

                MainCanvas main = Manager.UI.SceneUI as MainCanvas;
                main.SlotRefresh();
                return true;
            }
        }
        
       
        return false;
    }
    public void UseItem(Define.HeroType type, Item_Base item)
    {
        Debug.Log("아이템 사용");
        ItemDatas[] items = GetItemDatas(type);
        for(int i = 0; i < items.Length;i++)
        {
            if (items[i]._data != item)
                continue;


            if (items[i].count <= 0)
                return;

            items[i].count--;
            items[i].itemAction?.Invoke();
            _itemDataDic[type] = items;

            MainCanvas main = Manager.UI.SceneUI as MainCanvas;
            main.SlotRefresh();
            
            break;
        }
    }
    public void Init()
    {
        foreach(Define.HeroType h in System.Enum.GetValues(typeof(Define.HeroType)))
        {
            ItemDatas[] itemDatas = new ItemDatas[6];
            _itemDataDic.Add(h, itemDatas);
        }
    }
}
public struct ItemDatas
{
    public Item_Base _data;
    public int count;
    public Action itemAction;
}
