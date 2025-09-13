using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillLevelFragment : UI_Base
{
    enum Images
    {
        SkillImage,
    }
    enum Texts
    {
        SkillName,
        Explain_Txt,
        Point_Txt,
    }
    enum Objects
    {
        BarContent,
    }
    enum Buttons
    {
        Upgrade_Btn,
    }
    private Define.HeroType _hero;
    private Define.Skill _skill;
    private Skill_Base _data;
    private List<SkillData> skillData;
    private Image[] barImages;
    public override bool Init()
    {
        if(base.Init() == false)
            return false;

        BindObject(typeof(Objects));
        BindImage(typeof(Images));  
        BindText(typeof(Texts));
        BindButton(typeof(Buttons));

        barImages = new Image[skillData.Count];
        for(int i = 0; i < barImages.Length; i++)
        {
            int index = i;  
            Manager.UI.MakeItem<Image>(GetObject((int)Objects.BarContent).transform, "Bar", (image) =>
            {
                barImages[index] = image;
            });
        }

        Refresh();

        GetButton((int)Buttons.Upgrade_Btn).gameObject.BindEvent(BtnAction);
        return true;
    }
    public void SetInfo(Define.HeroType hero, Define.Skill skill, Skill_Base data, List<SkillData> skillData)
    {
        _data = data;
        _hero = hero;
        _skill = skill;
        this.skillData = skillData; 

        
    }
    private void Refresh()
    {
        int count = 0;
        bool[] boolean = _data.GetBoolean(_skill);

        foreach( bool b in boolean )
        {
            if(!b)
                break;
            count++;
        }

        if(count >= skillData.Count)
            count = skillData.Count -1;
        
        SkillData data = skillData[count];
        GetImage((int)Images.SkillImage).sprite = data.Image;
        GetText((int)Texts.SkillName).text = data.SkillName;
        GetText((int)Texts.Explain_Txt).text = data.Explain;
        GetText((int)Texts.Point_Txt).text = data.SkillPoint.ToString();

        for(int i = 0; i < barImages.Length; i++)
        {
            Image image = barImages[i];
            if (boolean[i] == true)
                image.color = Color.green;
            else
                image.color = Color.white;                
        }
    }

    private void BtnAction()
    {
        int count = 0;
        bool[] boolean = _data.GetBoolean(_skill);

        foreach (bool b in boolean)
        {
            if (!b)
                break;
            count++;
        }

        if (count >= skillData.Count)
            return;

        Manager.Skill.SkillUpgrade(_hero, _skill, skillData[count].SkillPoint);
        Refresh();
    }
}