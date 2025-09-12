using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class AutoState : StateController
{
    private Coroutine _auto;
    public override void Attack()
    {
        if(player.targeting || player.target == null)
            return;

        player.target = Manager.Creature.SearchMonster(player.transform);
        player.endPoint = player.target.transform.position;

        if (Vector2.Distance(player.target.transform.position, transform.position) <= player._status.Arange)
        {
            player.targeting = true;
            player.rb.velocity = Vector2.zero;


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
        if(player.rb.velocity == Vector2.zero)
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
        if (Vector2.Distance(player.target.transform.position, transform.position) <= player._status.Arange && player.isAtk)
        {
            Debug.Log("공격 체인지");
            player.rb.velocity = Vector2.zero;
            player.State = Define.State.Attack;
            return;
        }
        player.dir = ((Vector3)player.endPoint - transform.position).normalized;
        player.rb.MovePosition(Vector2.MoveTowards(player.rb.position, player.endPoint, player._status.Speed * Time.deltaTime));
    }
    public void StartAutoSkill()
    {
        if (_auto != null)
            StopAutoSkill();

        _auto = StartCoroutine(AutoSkill());
    }
    public void StopAutoSkill()
    {
        if(_auto == null)
            return;

        StopCoroutine(_auto);
        _auto = null;
    }
    private IEnumerator AutoSkill()
    {
        Skill[] skills = (Skill[])System.Enum.GetValues(typeof(Skill));
        while (true)
        {
            while(player.State != Define.State.Attack)
            {
                yield return null;
            }
            int value = Random.Range(0, skills.Length);
            Debug.Log(value);
            Skill curskill = skills[value];
            skill._skillDic[curskill]?.Invoke();
            yield return new WaitForSeconds(4f);
        }
    }

}
