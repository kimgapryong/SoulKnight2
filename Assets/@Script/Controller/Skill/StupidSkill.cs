using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StupidSkill : Skill_Base
{
    public override void Skill1()
    {
        Define.Skill type = Define.Skill.Skill1;
        SkillData data = GetSkillData(type);

        if (data == null || !CheckMp(type) || skill_1)
            return;

        skill_1 = true;
        List<MonsterController> monsters = Manager.Creature.SearchAllMonster(creature as PlayerController);

        foreach (MonsterController monster in monsters)
        {
            GameObject clone = Manager.Resources.Instantiate($"Skills/Magic", transform.position, Quaternion.identity);
            Vector3 dir = (monster.transform.position - clone.transform.position).normalized;

            ProjectileController projectile = clone.AddComponent<ProjectileController>();
            projectile.SetInfo(creature, dir, 6, GetDamage(data.Damage));
            Destroy(clone.gameObject, 5);
        }
        StartCoroutine(WaitCool(data.CoolTime, () => { skill_1 = false; }));
    }

    public override void Skill2()
    {
        Define.Skill type = Define.Skill.Skill2;
        SkillData data = GetSkillData(type);

        if (data == null || !CheckMp(type) || creature.target == null || skill_2)
            return;

        skill_2 = true;

        StartCoroutine(Chain("Magic", data.Again, GetDamage(data.Damage), 7));
        StartCoroutine(WaitCool(data.CoolTime, () => { skill_2 = false; }));
    }

    public override void Skill3()
    {
        Define.Skill type = Define.Skill.Skill3;
        SkillData data = GetSkillData(type);

        if (data == null || !CheckMp(type) || skill_3)
            return;

        skill_3 = true;

        List<PlayerController> players = Manager.Creature.SearchAllPlayer(creature);
        PlayerController me = creature as PlayerController;
        foreach (PlayerController player in players)
        {
            if(data.Again == 1)
            {
                me.Heal(GetDamage(data.Damage));
                break;
            }
            player.Heal(GetDamage(data.Damage));
        }
        StartCoroutine(WaitCool(data.CoolTime, () => { skill_3 = false; }));
    }

    public override void Skill4()
    {
        Define.Skill type = Define.Skill.Skill4;
        SkillData data = GetSkillData(type);

        if (data == null || !CheckMp(type) || creature.target == null || skill_4)
            return;

        skill_4 = true;

        creature.target.StartPoison(data.SkillTime, data.DotDamage, data.DotSpeed);
        StartCoroutine(WaitCool(data.CoolTime, () => { skill_4 = false; }));
    }

  
}
