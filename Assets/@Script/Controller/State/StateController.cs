using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateController : BaseController
{
    protected PlayerController player;
    protected Skill_Base skill;
    public override bool Init()
    {
        if(base.Init() == false)
            return false;

        player = GetComponent<PlayerController>();
        skill = GetComponent<Skill_Base>();
        return true;
    }
    public abstract void Move();
    public abstract void Idle();
    public abstract void Attack();
}
