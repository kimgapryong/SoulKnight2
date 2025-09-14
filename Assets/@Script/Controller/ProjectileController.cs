using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : BaseController
{
    private CreatureController attker;
    private bool check;
    private bool chain;

    private Vector3 dir;
    private float speed;
    private float damage;
    private bool penetration;
    public void SetInfo(CreatureController attker,Vector3 dir, float speed, float damage, bool penetration = false, float time = 5)
    {
        this.attker = attker;
        this.dir = dir;
        this.speed = speed;
        this.damage = damage;
        this.penetration = penetration;

        AlignRotationToDir();

        if (penetration)
            Destroy(gameObject, 5);
    }
    public void SetParticle(float time, float damage, CreatureController attker)
    {
        this.damage = damage;
        this.attker = attker;
        check = true;
        penetration= true;
        Destroy(gameObject, time);
    }
    public void SetChain(float damage, CreatureController attker)
    {
        this.attker = attker;
        this.damage = damage;
        chain = true;
    }
    private void AlignRotationToDir()
    {
        if (dir.sqrMagnitude < 0.00001f) return;
        dir = dir.normalized;

        float angle = Vector2.SignedAngle(Vector2.right, (Vector2)dir);
        transform.rotation = Quaternion.Euler(0f,0f, angle);
    }
    private void Update()
    {
        if (check || chain)
            return;

        transform.position += dir * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CreatureController m = null;
        if(attker is PlayerController)
            m = collision.GetComponent<MonsterController>();
        else if(attker is MonsterController)
            m = collision.GetComponent<PlayerController>();

        Debug.Log(m);
        if(m == null) return;

        Debug.Log(damage + "µ¥¹ÌÁö");
        m.OnDamage(attker,damage);

        if (!penetration && !chain)
            Destroy(gameObject);
    }
}
