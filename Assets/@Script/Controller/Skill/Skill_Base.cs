using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill_Base : BaseController
{
    public SkillValue[] _data;

    protected Dictionary<Define.Skill, bool[]> _skillCheckDic = new Dictionary<Define.Skill, bool[]>();
    protected Dictionary<Define.Skill, SkillData> _skillDataDic = new Dictionary<Define.Skill, SkillData>();
    public Dictionary<Define.Skill, Action> _skillDic = new Dictionary<Define.Skill, Action>();

    public CreatureController creature;
    private Status _status;

    protected bool skill_1;
    protected bool skill_2;
    protected bool skill_3;
    protected bool skill_4;

    public override bool Init()
    {
        if(base.Init() == false)    
            return false;

        SetData();
        SetInfo();
        return true;
    }
    private void SetData()
    {
        foreach(Define.Skill skill in System.Enum.GetValues(typeof(Define.Skill)))
        {
            switch (skill)
            {
                case Define.Skill.Skill1:
                    _skillDic.Add(skill, Skill1);
                    break;
                case Define.Skill.Skill2:
                    _skillDic.Add(skill, Skill2);
                    break;
                case Define.Skill.Skill3:
                    _skillDic.Add(skill, Skill3);
                    break;
                case Define.Skill.Skill4:
                    _skillDic.Add(skill, Skill4);
                    break;
            }
        }
        foreach (var data in _data)
        {
            bool[] boolean = new bool[data.Datas.Count];
            _skillCheckDic.Add(data.Type, boolean);
        }
    }
    public void SetInfo()
    {
        creature = GetComponent<CreatureController>();
        _status = creature._status;

        if (creature is PlayerController)
        {
            PlayerController player = creature as PlayerController;
            Manager.Skill.AddSkill(player._type, this);
        }
            
    }
    public abstract void Skill1();
    public abstract void Skill2();
    public abstract void Skill3();
    public abstract void Skill4();

    public float GetClipLength(Animator anim, string name)
    {
        foreach(var c in anim.runtimeAnimatorController.animationClips)
            if(c != null && c.name == name)
                return c.length;

        return 0;
    }
    public float GetDamage(float percent)
    {
        return _status.Damage * (percent / 100);
    }
    protected bool CheckMp(Define.Skill skill)
    {
        SkillData skillData;
        if(_skillDataDic.TryGetValue(skill, out skillData) == false)
            return false;

        if(_status.CurMp <  skillData.Mp)
            return false;

        return true;
    }
    public SkillData GetSkillData(Define.Skill skill)
    {
        if(_skillDataDic.TryGetValue(skill, out SkillData data))
            return data;

        return null;
    }
    public bool[] GetBoolean(Define.Skill type)
    {
        if(_skillCheckDic.TryGetValue(type, out bool[] boolean))
            return boolean;

        return null;
    }
    public void SetBoolean(Define.Skill type , bool[] boolean)
    {
        _skillCheckDic[type] = boolean;
    }
    public void ChangeData(Define.Skill type, int idx)
    {
        SkillValue value = null;
        foreach(var skill in _data)
        {
            if (type == skill.Type)
            {
                value = skill;
                break;
            }
        }

        _skillDataDic[type] = value.Datas[idx];
    }

    public IEnumerator WaitCool(float time, Action callback)
    {
        yield return new WaitForSeconds(time);
        callback?.Invoke();
    }

    protected IEnumerator Combo(string path, float wait, int count, float damage)
    {
        for(int i = 0; i < count; i++)
        {
            GameObject clone = Manager.Resources.Instantiate($"Skills/{path}", transform.position, Quaternion.identity);
            Vector3 dir = (creature.target.transform.position - clone.transform.position).normalized;

            ProjectileController projectile = clone.AddComponent<ProjectileController>();
            projectile.SetInfo(creature, dir, 6, damage);
            Destroy(clone.gameObject, 5);

            yield return new WaitForSeconds(wait);
        }
    }
    protected void MakeProjectile(string path, int count, float damage)
    {
        Vector2 baseDir = (creature.target.transform.position - transform.position).normalized;
        for(int i = 0; i < count; i++)
        {
            float angle = (i - (count - 1) * 0.5f) * 10f;
            Vector2 dir = (Vector2)(Quaternion.Euler(0, 0, angle) * baseDir);

            GameObject clone = Manager.Resources.Instantiate($"Skills/{path}", transform.position, Quaternion.identity);
            
            ProjectileController projectile = clone.AddComponent<ProjectileController>();
            projectile.SetInfo(creature, dir, 6, damage, true);
        }
    }
    protected IEnumerator Chain(string path, int count, float damage, float speed)
    {
        int result = 0;
        CreatureController target = creature.target;
        GameObject clone = Manager.Resources.Instantiate($"Skills/{path}", transform.position, Quaternion.identity);

        ProjectileController projectile = clone.AddComponent<ProjectileController>();
        projectile.SetChain();
        Debug.Log(count);

        while (result < count)
        {
            if (target == null)
            {
                // 새 타겟 찾기
                target = Manager.Creature.SearchMonster(clone.transform);
                if (target == null)
                    break; // 더 이상 체인할 대상 없음
            }

            Vector3 dir = (target.transform.position - clone.transform.position).normalized;
            Debug.Log(dir);
            while (target != null && Vector2.Distance(clone.transform.position, target.transform.position) > 0.1f)
            {
                clone.transform.position += dir * speed * Time.deltaTime;
                yield return null;
            }

            // 타겟 갱신
            target = Manager.Creature.SearchMonster(clone.transform);
            Debug.Log("다음");
            result++;
        }
        Destroy(clone);

    }
}

[Serializable]
public class SkillValue
{
    public Define.Skill Type;
    public List<SkillData> Datas;
}