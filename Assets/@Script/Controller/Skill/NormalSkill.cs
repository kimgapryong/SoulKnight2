using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalSkill : Skill_Base
{
    public override void Skill1()
    {
        Define.Skill type = Define.Skill.Skill1;
        SkillData data = GetSkillData(type);

        if (data == null || !CheckMp(type) || creature.target == null || skill_1)
            return;

        skill_1 = true;

        StartCoroutine(Combo("Arrow", 0.1f, data.Again, GetDamage(data.Damage)));
        StartCoroutine(WaitCool(data.CoolTime, () => { skill_1 = false; }));
    }
    
    public override void Skill2()
    {
        Define.Skill type = Define.Skill.Skill2;
        SkillData data = GetSkillData(type);

        if (data == null || !CheckMp(type) || skill_2)
            return;

        skill_2 = true;
        List<MonsterController> monsters = Manager.Creature.SearchAllMonster(creature as PlayerController);

        foreach (MonsterController monster in monsters)
        {
            GameObject clone = Manager.Resources.Instantiate($"Skills/Arrow", transform.position, Quaternion.identity);
            Vector3 dir = (monster.transform.position - clone.transform.position).normalized;

            ProjectileController projectile = clone.AddComponent<ProjectileController>();
            projectile.SetInfo(creature, dir, 6, GetDamage(data.Damage));
            Destroy(clone.gameObject, 5);
        }
        StartCoroutine(WaitCool(data.CoolTime, () => { skill_2 = false; }));
    }

    public override void Skill3()
    {
        Define.Skill type = Define.Skill.Skill3;
        SkillData data = GetSkillData(type);

        if (data == null || !CheckMp(type) || creature.target == null || skill_3)
            return;

        skill_3 = true;

        MakeProjectile("Arrow", data.Again, GetDamage(data.Damage));
        StartCoroutine(WaitCool(data.CoolTime, () => { skill_3 = false; }));
    }

    public override void Skill4()
    {
        Define.Skill type = Define.Skill.Skill4;
        SkillData data = GetSkillData(type);

        if (data == null || !CheckMp(type) || creature.target == null || skill_4)
            return;

        skill_4 = true;

        GameObject clone = Manager.Resources.Instantiate($"Skills/ArrowRain", creature.target.transform.position + Vector3.up * 5f, Quaternion.identity);
        ProjectileController projectile = clone.AddComponent<ProjectileController>();
        projectile.SetParticle(1.2f);

        StartCoroutine(WaitCool(data.CoolTime, () => { skill_4 = false; }));
    }

   
}
