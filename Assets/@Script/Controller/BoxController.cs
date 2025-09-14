using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonsterController
{
    public List<GameObject> items = new List<GameObject>();
    public float percent;
    public override bool Init()
    {
        State = Define.State.Idle;
        return true;
    }
    protected override void Move() { }
    protected override void Attack() { }
    protected override void Idle() { }

    public override void OnDamage(CreatureController attker, float damage)
    {
        OnDie(attker);
    }
    protected override void OnDie(CreatureController attker)
    {
        if(Manager.Random.RollBackPercent(percent))
            Instantiate(items[Random.Range(0, items.Count)], transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
