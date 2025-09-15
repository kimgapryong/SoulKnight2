using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonsterController
{
    public Define.Skill[] skillType;

    public override bool Init()
    {
        if(base.Init() == false)
            return false;

        StartCoroutine(StartSkill());

        return true;
    }
    protected IEnumerator StartSkill()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5f, 8f));
            Define.Skill curSkill = skillType[Random.Range(0, skillType.Length)];
            _skill._skillDic[curSkill]?.Invoke();
        }
    }
}
