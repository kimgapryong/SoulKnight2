using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElkeController : BossController
{
    private ElkeCanvas _canvas;
    private ElkeSkill elkeSkill;
    public override bool Init()
    {
        if(base.Init() == false)    
            return false;

        elkeSkill = GetComponent<ElkeSkill>();
        Manager.UI.ShowPopUI<ElkeCanvas>(callback: (pop) =>
        {
            _canvas = pop;
            elkeSkill.SetCanvas(pop);
        });

        State = Define.State.Move;
        StartCoroutine(StartSkill());
        return true;
    }
    protected override void Attack()
    {
        
    }
    protected override void Idle()
    {
        
    }
    protected override void Move()
    {
        rb.MovePosition(Vector2.MoveTowards(rb.position, endPoint, 11 * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CreatureController creature = collision.GetComponent<CreatureController>();
        if (creature == null) 
            return;

        creature.OnDamage(this, 2000f);
    }
}
