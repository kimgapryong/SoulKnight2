using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPop : UI_Pop
{
    enum Objects
    {
        Content,
    }
    enum Texts
    {
        Point_Txt,
    }
    enum Buttons
    {
        Close_Btn,
    }
    private Define.HeroType _hero;
    private Skill_Base _skill;
    private PlayerStatus _status;
    public override bool Init()
    {
        if(base.Init() == false)
            return false;

        BindObject(typeof(Objects));
        BindText(typeof(Texts));
        BindButton(typeof(Buttons));

        _status.skillAction = PointAction;
        PointAction(_status.Point);

        foreach(var skill in _skill._data)
        {
            Manager.UI.MakeSubItem<SkillLevelFragment>(GetObject((int)Objects.Content).transform, callback: (fa) =>
            {
                fa.SetInfo(_hero, skill.Type, _skill, skill.Datas);
            });
        }

        GetButton((int)Buttons.Close_Btn).gameObject.BindEvent(() =>
        {
            ClosePopupUI();
            Time.timeScale = 1.0f;
            _status.skillAction = null;
        });

        return true;
    }
    public void SetInfo(Define.HeroType hero, Skill_Base skill)
    {
        _hero = hero;
        _skill = skill;
        PlayerController player = skill.creature as PlayerController;
        _status = player.plaStatus;
    }
    
    public void PointAction(int point)
    {
        GetText((int)Texts.Point_Txt).text = $"Æ÷ÀÎÆ®:{point}";
    }
}
