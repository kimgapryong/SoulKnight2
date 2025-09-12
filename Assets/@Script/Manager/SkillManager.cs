using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager
{
    private Dictionary<Define.HeroType, Skill_Base> _heroSkillDic = new Dictionary<Define.HeroType, Skill_Base>();
    public void AddSkill(Define.HeroType type, Skill_Base skill)
    {
        _heroSkillDic[type] = skill;
    }
    public Skill_Base GetSkill(Define.HeroType type)
    {
        if(_heroSkillDic.TryGetValue(type, out Skill_Base skillBase))
            return skillBase;

        return null;
    }
    public void SkillUpgrade(Define.HeroType type, Define.Skill skill, int point)
    {
        Skill_Base skillBase = _heroSkillDic[type];
        PlayerController player = skillBase.creature as PlayerController;
        PlayerStatus status = player.plaStatus;

        bool[] boolean = skillBase.GetBoolean(skill);
        if(point > status.Point || boolean[boolean.Length - 1] == true)
            return;

        status.Point -= point;

        //값 변경 및 저장
        for(int i = 0; i < boolean.Length; i++)
        {
            if (boolean[i])
                continue;

            boolean[i] = true;
            skillBase.SetBoolean(skill,boolean);
            skillBase.ChangeData(skill, i);
            break;
        }


    }
}
