using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : CreatureController
{
    public Define.MonsterType _type;
    private bool _back;
    private bool _atk;

    private bool sturn;
    public MonsterStatus monStatus;
    private string animKey;
    public override bool Init()
    {
        if (base.Init() == false)
            return false;
        
        State = Define.State.Idle;

        return true;
    }
    protected override void ChangeAnim(Define.State type)
    {
        if(anim == null)
            return;
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
                animKey = $"Walk_{key}";
                anim.Play(animKey);
                break;
            case Define.State.Attack:

                break;
            case Define.State.Move:
                animKey = $"Walk_{key}";
                anim.Play(animKey);
                break;
        }
    }

    public void SetStatus(MonsterStatus status)
    {
        monStatus = status;
        _status = status;
        Speed = status.Speed;
    }
    public override void UpdateMethod()
    {
        SearchPlayer();
        float localY = (dir.x > 0) ? -180 : (dir.x < 0) ? 0 : transform.eulerAngles.y;
        Vector3 newPos = transform.eulerAngles;
        newPos.y = localY;
        transform.eulerAngles = newPos;

        switch (State)
        {
            case (Define.State.Idle):
                Idle();
                break;
            case (Define.State.Move):
                Move();
                break;
            case (Define.State.Attack):
                Attack();
                break;
        }
    }
    protected override void Idle()
    {
        if(sturn)
            return;
            
        if(Vector2.Distance(transform.position, target.transform.position) <= _status.MoveArange)
        {
            State = Define.State.Move;
            return;
        }

        RandomMove();
    }
    protected override void Move()
    {
        if(_back)
            return;
        
        if (Vector2.Distance(transform.position, target.transform.position) > _status.MoveArange)
        {
            State= Define.State.Idle;
            target = null;
            return;
        }
        else if(Vector2.Distance(transform.position, target.transform.position) <= _status.Arange)
        {
            State = Define.State.Attack;
            return;
        }

        NormalMove();
    }
    protected override void Attack()
    {
        if(_atk)
            return;

        _atk = true;
        anim.Play("Attack");
        Ability();
        StartCoroutine(WaitCool(_status.AtkSpeed, () => { State = Define.State.Move; _atk = false; }));
    }

    protected virtual void Ability() { target.OnDamage(this, monStatus.Damage); }
    protected virtual void Apply(Vector2 pos, float power)
    {
        State = Define.State.Idle;
        _back = true;   

        rb.velocity = Vector2.zero;
        Vector2 dir = (rb.position - pos).normalized;
        rb.AddForce(dir * power, ForceMode2D.Impulse);
        StartCoroutine(WaitCool(0.4f, () => { _back = false; }));
    }
    float moveTime = 1f;
    float curTime = 0;
    private void RandomMove()
    {
        if(curTime >= moveTime)
        {
            curTime = 0f;
            dir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        }
        curTime += Time.deltaTime;
        rb.MovePosition(rb.position + dir * _status.Speed * Time.deltaTime);
    }
    private void NormalMove()
    {
        if(target.transform.position == null)
            return;

        dir = (target.transform.position - transform.position).normalized;
        rb.MovePosition(Vector2.MoveTowards(rb.position, target.transform.position, _status.Speed * Time.deltaTime));
    }
    private void SearchPlayer()
    {
        target = Manager.Creature.SearchPlayer(transform);
    }

    public void Sturn(float time, float damage)
    {
        sturn = true;
        sp.color = Color.gray;
        State = Define.State.Idle;

        monStatus.CurHp -= damage;
        StartCoroutine(WaitCool(time, () =>{ sturn = false; sp.color = Color.white; } ));

    }
    public override void OnDamage(CreatureController attker, float damage)
    {
        Apply(attker.transform.position, 5f);

        if (_die || _damage)
            return;

        _damage = true;
        sp.color = Color.red;
        _status.CurHp -= damage;

        if(sturn)
            StartCoroutine(WaitCool(0.2f, () => { _damage = false; sp.color = Color.gray; }));
        else
            StartCoroutine(WaitCool(0.2f, () => { _damage = false; sp.color = Color.white; }));

        if (_status.CurHp <= 0)
            OnDie(attker);
    }
    protected override void OnDie(CreatureController attker)
    {
        base.OnDie(attker);
        PlayerController pla = attker as PlayerController;
        if (pla == null)
            return;

        pla.plaStatus.AddExp(monStatus.Amount);
        Manager.Game.AddMissionvalue(_type);
        Manager.Creature._monsterList.Remove(this);
        _monsterSpwaner.m_Spwaner.Remove(this);
        Destroy(gameObject);
    }
    private MonsterSpwaner _monsterSpwaner;
    public void SetSpwanList(MonsterSpwaner spwan)
    {
        _monsterSpwaner = spwan;
    }
}
