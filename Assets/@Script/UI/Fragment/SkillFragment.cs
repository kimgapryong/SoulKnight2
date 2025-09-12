using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillFragment : UI_Base
{
    public Define.Skill _type;
    private SkillData _skill = null;
    enum Images
    {
        ItemImage,
    }
   enum Texts
    {
        SkillName,
    }
    private Image itemImage;
    private Image useImage;
    public override bool Init()
    {
        if(base.Init() == false )   
            return false;

    
        return true;
    }
    public void SetInfo()
    {
        BindText(typeof(Texts));
        BindImage(typeof(Images));
        itemImage = GetComponent<Image>();
        useImage = GetImage((int)Images.ItemImage);

    }
    public void Refresh(Skill_Base skill)
    {
        SkillData data = skill.GetSkillData(_type);
        Debug.Log(data);
        if(data == null)
        {
            Debug.Log(skill._data);
            foreach(var value in skill._data)
            {
                if(value.Type == _type)
                {
                    useImage.sprite = value.Datas[0].Image;
                    useImage.color = Color.gray;
                    GetText((int)Texts.SkillName).text = data.SkillName;
                    break;
                }
                Debug.Log(value);
            }
            return;
        }

        _skill = data;

        Debug.Log("¿Ö ¾ÈµÅ");
        itemImage.sprite = data.Image;
        useImage.sprite = data.Image;
        useImage.color = Color.white;
        GetText((int)Texts.SkillName).text = data.SkillName;
    }
    

}
