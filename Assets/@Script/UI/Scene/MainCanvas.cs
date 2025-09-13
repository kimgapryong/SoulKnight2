using System;
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

    public bool _skill;
    public bool _change;

    public override bool Init()
    {
        if(base.Init() == false)
            return false;

        BindImage(typeof(Images));
        BindObject(typeof(Objects));

        foreach(var skill in _skillList)
            skill.SetInfo(this);
        foreach(var change in _changeList)
            change.SetInfo(this);

        Manager.Instance.ChangeAction -= ChangeAction;
        Manager.Instance.ChangeAction += ChangeAction;

        StartCoroutine(WaitAction(ChangeAction));

        return true;
    }

    public void ChangeAction()
    {
        foreach(var skill in _skillList)
        {
            skill.Refresh(Manager.Skill.GetSkill(Manager.Player._type));
        }

        foreach(var change in _changeList)
        {
            if(change.hero == Manager.Player._type)
            {
                change.selectImage.color = Color.blue;
                continue;
            }

            change.selectImage.color = Color.white;
        }
    }

    private IEnumerator WaitAction(Action callback)
    {
        while(!_skill || !_change)
        {
            yield return null;
        }
        callback?.Invoke();
    }
}
