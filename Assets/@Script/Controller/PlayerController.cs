using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CreatureController
{
    public Define.HeroType _type;
    public Define.AI _ai;

    public PlayerState playerState;
    public AutoState autoState;
    

    public PlayerStatus plaStatus;
    private string animKey;

    public bool targeting;
    public bool isAtk;

    public override bool Init()
    {
        if(base.Init() == false)
            return false;

        State = Define.State.Idle;
        playerState = GetComponent<PlayerState>();
        autoState = GetComponent<AutoState>();

        return true;
    }
    public void SetStatus(PlayerStatus status)
    {
        plaStatus = status;
        _status = status;
        Speed = status.Speed;
    }
    public override void UpdateMethod()
    {
        if (_ai == Define.AI.Auto)
            target = Manager.Creature.SearchMonster(transform);

        base.UpdateMethod();
    }
    protected override void ChangeAnim(Define.State type)
    {
        string key = "Side";
        if(Mathf.Abs(dir.x) - Mathf.Abs(dir.y) < 0)
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
                animKey =$"Walk_{key}" ;
                anim.Play(animKey);
                break;
        }
    }
    protected override void Move()
    {
        
        if (_ai == Define.AI.Player)
            playerState.Move();
        else
            autoState.Move();

    }
    protected override void Attack()
    {
        rb.velocity = Vector3.zero;
        if (_ai == Define.AI.Player)
            playerState.Attack();
        else
            autoState.Attack();

    }
    protected override void Idle()
    {
        rb.velocity = Vector3.zero;
        if (_ai == Define.AI.Player)
            playerState.Idle();
        else
            autoState.Idle();
    }
 

    public void ChangeMode(Define.AI ai)
    {
        _ai = ai;
    }
    public void SetTarget(MonsterController monster)
    {
        if(_ai == Define.AI.Auto)
            return;

        target = monster;
        State = Define.State.Attack;
        Manager.Creature.SetState(this, State);
    }
    public void SetPoint(Vector2 point)
    {

        endPoint = point;
        State = Define.State.Move;
        Manager.Creature.SetState(this, State);
    }
}
