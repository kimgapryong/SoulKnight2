using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotFragment : UI_Base
{
    enum Images
    {
        ItemImage,
    }
    enum Texts
    {
        ItemCount,
    }
    private MainCanvas main;
    public override bool Init()
    {
        if(base.Init() == false )
            return false;

        BindImage(typeof(Images));
        BindText(typeof(Texts));

        main._slot = true;
        return true;
    }

    public void SetInfo(MainCanvas main)
    {
        this.main = main;
    }
    public void DeSet()
    {
        GetImage((int)Images.ItemImage).gameObject.SetActive(false);
        GetText((int)Texts.ItemCount).gameObject.SetActive(false);
    }
    public void Set(ItemDatas datas)
    {
        GetImage((int)Images.ItemImage).gameObject.SetActive(true);
        GetImage((int)Images.ItemImage).sprite = datas._data._data.Image;

        GetText((int)Texts.ItemCount).gameObject.SetActive(true);
        GetText((int)Texts.ItemCount).text = $"{datas.count}/{datas._data._data.MaxCount}";
    }
}
