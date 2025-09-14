using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class GrandSkill : Skill_Base
{
    private PlayerController player;
    public override bool Init()
    {
        if(base.Init() == false)
            return false;

        foreach(var value in _data)
        {
            _skillDataDic[value.Type] = value.Datas[0];
            bool[] boolean = GetBoolean(value.Type);
            boolean[0] = true;
            SetBoolean(value.Type, boolean);
        }
        player = GetComponent<PlayerController>();
        return true;
    }
    public override void Skill1()
    {
        Define.Skill type = Define.Skill.Skill1;
        SkillData data = GetSkillData(type);

        if(data == null || !CheckMp(type) || creature.target == null || skill_1)
            return;

        skill_1 = true;
        

        GameObject clone = Manager.Resources.Instantiate("Skills/Slash", creature.target.transform.position, Quaternion.identity);
        Animator anim = clone.GetComponent<Animator>();

        anim.Play("Slash");
        float time = GetClipLength(anim, "Slash");
        StartCoroutine(WaitCool(time, () =>
        {
            creature.target.OnDamage(creature, GetDamage(data.Damage));
            Destroy(clone);
        }));

        if (player.fight)
            StartCoroutine(WaitCool(1f, () => { skill_1 = false; }));
        else
            StartCoroutine(WaitCool(data.CoolTime, () => { skill_1 = false; }));
    }

    public override void Skill2()
    {
        Define.Skill type = Define.Skill.Skill2;
        SkillData data = GetSkillData(type);

        Debug.Log("할배1");
        Debug.Log(data);
        Debug.Log(!CheckMp(type));
        if(data == null || !CheckMp(type) || skill_2)
            return;

        Debug.Log("할배2");
        skill_2 = true;
        List<MonsterController> monsters = Manager.Creature.SearchAllMonster(creature as PlayerController);

        foreach(var mon in monsters)
        {
            GameObject clone = Manager.Resources.Instantiate("Skills/Slash", mon.transform.position, Quaternion.identity);
            Animator anim = clone.GetComponent<Animator>();

            anim.Play("Slash");
            float time = GetClipLength(anim, "Slash");
            StartCoroutine(WaitCool(time, () =>
            {
                creature.target.OnDamage(creature, GetDamage(data.Damage));
                Destroy(clone);
            }));
        }

        if (player.fight)
            StartCoroutine(WaitCool(1f, () => { skill_2 = false; }));
        else
            StartCoroutine(WaitCool(data.CoolTime, () => { skill_2 = false; }));

    }

    public override void Skill3()
    {
        Define.Skill type = Define.Skill.Skill3;
        SkillData data = GetSkillData(type);

        if(data == null || !CheckMp(type) || creature.target == null || skill_3)
            return;

        skill_3 = true;

        GameObject clone = Manager.Resources.Instantiate("Skills/Slash", transform.position, Quaternion.identity);
        Vector3 dir = (creature.target.transform.position - clone.transform.position).normalized;
        Animator anim = clone.GetComponent<Animator>();

        anim.Play("AllSlash");
        float time = GetClipLength(anim, "AllSlash");

        ProjectileController projectile = clone.AddComponent<ProjectileController>();
        projectile.SetInfo(creature, dir, 6, GetDamage(data.Damage));
        Destroy(clone.gameObject, time);
        if (player.fight)
            StartCoroutine(WaitCool(1f, () => { skill_3 = false; }));
        else
            StartCoroutine(WaitCool(data.CoolTime, () => { skill_3 = false; }));
    }

    public override void Skill4()
    {
        Define.Skill type = Define.Skill.Skill4;
        SkillData data = GetSkillData(type);

        if (data == null || !CheckMp(type) || skill_4)
            return;

        skill_4 = true;
        List<MonsterController> monsters = Manager.Creature.SearchAllMonster(creature as PlayerController);

        Debug.Log($"몬스터갯수{monsters.Count}");
        foreach(var mon in monsters)
        {
            GameObject clone = Manager.Resources.Instantiate("Skills/Slash", mon.transform.position, Quaternion.identity);
            Animator anim = clone.GetComponent<Animator>();

            anim.Play("SutrnSlash");
            float time = GetClipLength(anim, "SutrnSlash");

            Destroy(clone.gameObject, time);
            if(Manager.Random.RollBackPercent(data.Persent))
                mon.Sturn(data.PersentTime, GetDamage(data.Damage));
        }
        if (player.fight)
            StartCoroutine(WaitCool(1f, () => { skill_4 = false; }));
        else
            StartCoroutine(WaitCool(data.CoolTime, () => { skill_4 = false; }));
    }

  
}
