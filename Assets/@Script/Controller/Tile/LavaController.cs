using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaController : BaseController
{
    public float damage;
    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player != null)
            player.OnDamage(null, damage);
    }
}
