using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : CreatureController
{
    private bool _back;
    private bool _atk;

    private bool sturn;
    public MonsterStatus monStatus;
    public override bool Init()
    {
        if (base.Init() == false)
            return false;
        
        State = Define.State.Idle;

        return true;
    }
    protected override void ChangeAnim(Define.State state)
    {
        switch (state)
        {
            case Define.State.Attack:
                anim.Play("Hit");
                break;
            case Define.State.Move:
                anim.Play("Walk");
                break;
            case Define.State.Idle:
                anim.Play("Walk");
                break;
        }
    }

    public void SetStatus(MonsterStatus status)
    {
        monStatus = status;
        _status = status;
    }
    public override void UpdateMethod()
    {
        SearchPlayer();
        base.UpdateMethod();
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
        dir = (target.transform.position - transform.position).normalized;
        rb.MovePosition(Vector2.MoveTowards(rb.position, target.transform.position, _status.Speed * Time.deltaTime));
    }
    private void SearchPlayer()
    {
        target = Manager.Creature.SearchPlayer(this);
    }

    public void Sturn(float time, float damange)
    {
        sturn = true;
        sp.color = Color.gray;
        State = Define.State.Idle;
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
        Manager.Creature._monsterList.Remove(this);
        Destroy(gameObject);
    }
}
