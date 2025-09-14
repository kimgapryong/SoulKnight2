using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonMonster : MonsterController
{
    protected override void Ability()
    {
        if (target == null)
            return;

        Vector2 dir = (target.transform.position - transform.position).normalized;
        GameObject arrow = Manager.Resources.Instantiate("Projectile/Poison", transform.position, Quaternion.identity);
        ProjectileController projectile = arrow.AddComponent<ProjectileController>();
        projectile.SetInfo(this, dir, 6, monStatus.Damage);

    }
}
