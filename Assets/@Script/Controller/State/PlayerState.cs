using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : StateController
{
    public override void Attack()
    {
        if (player.targeting || player.target == null)
            return;

        player.endPoint = player.target.transform.position;

        if (Vector2.Distance(player.target.transform.position, transform.position) <= player._status.Arange)
        {
            player.rb.velocity = Vector2.zero;
            
            if (player.target == null)
            {
                player.isAtk = false;
                player.State = Define.State.Idle;
            }
            player.targeting = true;
            player.NormalAtk();
            StartCoroutine(player.WaitCool(player.plaStatus.AtkSpeed, () => player.targeting = false));
        }
        else
        {
            player.rb.velocity = Vector2.zero;
            player.State = Define.State.Move;
            return;
        }
    }

    public override void Idle()
    {
        if (player.rb.velocity == Vector2.zero)
            return;
        player.rb.velocity = Vector2.zero;
    }

    public override void Move()
    {
        if (Vector2.Distance(player.endPoint, transform.position) <= 0.2f)
        {
            player.rb.velocity = Vector2.zero;
            player.State = Define.State.Idle;
            return;
        }
        if (player.isAtk && (Vector2.Distance(player.endPoint, transform.position) <= player.plaStatus.Arange))
        {
            player.rb.velocity = Vector2.zero;
            player.State = Define.State.Attack;
            return;
        }
        player.dir = ((Vector3)player.endPoint - transform.position).normalized;
        player.rb.MovePosition(Vector2.MoveTowards(player.rb.position, player.endPoint, player._status.Speed * Time.deltaTime));
    }
}
