using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvas : UI_Scene
{
    enum Images
    {
        Skill_1,
        Skill_2,
        Skill_3,
        Skill_4,
    }
    enum Objects
    {
        ChangeContent1,
        ChangeContent2,
        ChangeContent3,
    }
    public List<SkillFragment> _skillList = new List<SkillFragment>();
    public List<ChangeFragment> _changeList = new List<ChangeFragment>();

    public override bool Init()
    {
        if(base.Init() == false)
            return false;

        BindImage(typeof(Images));
        BindObject(typeof(Objects));

        foreach(var skill in _skillList)
        {
            skill.SetInfo();
        }

        Manager.Instance.ChangeAction -= ChangeAction;
        Manager.Instance.ChangeAction += ChangeAction;
        ChangeAction();


        return true;
    }

    public void ChangeAction()
    {
        foreach(var skill in _skillList)
        {
            skill.Refresh(Manager.Skill.GetSkill(Manager.Player._type));
        }
    }
}
