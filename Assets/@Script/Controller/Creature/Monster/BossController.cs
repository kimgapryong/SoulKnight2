using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossController : MonsterController
{
    public CreatureData bossData;
    public Define.Skill[] skillType;

    protected Action skillAction;
    protected bool _useSkill;

    // 게임 시작용
    public bool startStage;
    public override bool Init()
    {
        if(base.Init() == false)
            return false;

        StartCoroutine(StartSkill());
        BossSetData();
        State = Define.State.Idle;

        return true;
    }
    protected override void ChangeAnim(Define.State type)
    {
        string key = "Side";
        if (Mathf.Abs(dir.x) - Mathf.Abs(dir.y) < 0)
        {
            if (dir.y < 0)
                key = "F";
            else if (dir.y > 0)
                key = "B";
        }
        switch (type)
        {
            case Define.State.Idle:
                string idleKey = animKey == "Walk_Side" ? "Idle_Side" : animKey == "Walk_F" ? "Idle_F" : animKey == "Walk_B" ? "Idle_B" : "Idle_Side";
                anim.Play(idleKey);
                break;
            case Define.State.Attack:

                break;
            case Define.State.Move:
                animKey = $"Walk_{key}";
                anim.Play(animKey);
                break;
        }
    }
    private void BossSetData()
    {
        if(bossData == null)
            return;

        MonsterStatus status = gameObject.GetOrAddComponent<MonsterStatus>();
        Manager.UI.MakeSubItem<HpFragment>(gameObject.transform, "HpCanvas", (fa) =>
        {
            fa.SetInfo(status);
            fa.GetComponent<Canvas>().sortingOrder = 3;
        });

        status.SetData(bossData);
        SetStatus(status);
        _back = true;
    }
    protected override void Idle()
    {
        if (_useSkill || target != null || !startStage)
            return;

        if (Vector2.Distance(transform.position, target.transform.position) <= _status.MoveArange)
        {
            State = Define.State.Move;
            return;
        }
    }
    protected override void Attack()
    {
        if (_atk || _useSkill)
            return;

        
        anim.Play("Attack");
        if(skillAction != null)
        {
            State = Define.State.Idle;
            _useSkill = true;
            skillAction?.Invoke();

            StartCoroutine(WaitCool(2f, () => { State = Define.State.Move;  _useSkill = false; }));
            return;
        }

        _atk = true;
        Ability(); // 보통 공격
        StartCoroutine(WaitCool(_status.AtkSpeed, () => { State = Define.State.Move; _atk = false; }));
    }
    
    protected virtual IEnumerator StartSkill()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(4f, 6f));
            Define.Skill curSkill = skillType[Random.Range(0, skillType.Length)];
            skillAction = _skill._skillDic[curSkill];
        }
    }
}
