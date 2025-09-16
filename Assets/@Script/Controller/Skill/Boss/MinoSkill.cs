using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MinoSkill : Skill_Base
{
    public List<CreatureData> _monData;
    private bool _dash;
    public override void Skill1()
    {
        Debug.Log(creature._status.Damage);
        foreach(PlayerController player in Manager.Creature._playerList)
        {
            GameObject ep = Manager.Resources.Instantiate("Skills/CircleExplosion", player.transform.position, Quaternion.identity);
            Animator anim = ep.GetComponent<Animator>();
            anim.Play("CircleExplosion");

            float time = GetClipLength(anim, "CircleExplosion");
            StartCoroutine(WaitCool(time, () =>
            {
                GameObject exp = Manager.Resources.Instantiate("Skills/Explosion", ep.transform.position, Quaternion.identity);
                Animator expAnim = exp.GetComponent<Animator>();
                expAnim.Play("Explosion");

                float time = GetClipLength(expAnim, "Explosion");
                exp.AddComponent<ProjectileController>().SetParticle(time, creature._status.Damage, creature);
                Destroy(ep);

            }));
        }
    }

    public override void Skill2()
    {
        int count = 9;        // 폭발 개수
        float radius = 6f;    // 보스 중심에서의 반지름

        Vector3 bossPos = transform.position;

        for (int i = 0; i < count; i++)
        {
            float angle = (2 * Mathf.PI / count) * i;

            Vector3 pos = bossPos +
                          new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;

            StartCoroutine(CoBossExplosionCircle(pos));

        }
  
    }

    private IEnumerator CoBossExplosionCircle(Vector3 pos)
    {
        // 1) 경고 이펙트 생성
        GameObject ep = Manager.Resources.Instantiate("Skills/CircleExplosion", pos, Quaternion.identity);
        Animator anim = ep.GetComponent<Animator>();
        anim.Play("CircleExplosion");

        float warnTime = GetClipLength(anim, "CircleExplosion");

        // 2) 경고가 끝나면 폭발 생성
        yield return new WaitForSeconds(warnTime);

        GameObject exp = Manager.Resources.Instantiate("Skills/Explosion", pos, Quaternion.identity);
        Animator expAnim = exp.GetComponent<Animator>();
        expAnim.Play("Explosion");

        float expTime = GetClipLength(expAnim, "Explosion");

        exp.AddComponent<ProjectileController>().SetParticle(expTime, creature._status.Damage, creature);

        Destroy(ep); // 경고 이펙트 제거

      
    }

    public override void Skill3()
    {
        foreach (CreatureData monData in _monData)
        {
            MonsterController mon = Manager.Creature.CreateMonster(monData);
            mon.gameObject.transform.position = transform.position + new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f));
        }
    }

    public override void Skill4()
    {
        if (creature.target == null) return;

        Debug.Log("슼ㄹ 4");
        Vector3 dir = (creature.target.transform.position - transform.position).normalized;

        float dashSpeed = 15f; // 원하는 속도
        float dashDuration = 1.4f;

        _dash = true;
        StartCoroutine(CoDash(dir, dashSpeed, dashDuration));
    }

    private IEnumerator CoDash(Vector3 dir, float speed, float duration)
    {
        float timer = 0f;
        while (timer < duration)
        {
            creature.rb.velocity = dir * speed;
            timer += Time.deltaTime;
            yield return null;
        }

        // 돌진 끝나면 다시 속도 초기화
        creature.rb.velocity = Vector2.zero;
        _dash = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            creature.rb.velocity = Vector3.zero;
            _dash = false;
        }
            

        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player == null) 
            return;

        creature.rb.velocity = Vector3.zero;
        if (_dash)
            player.OnDamage(creature, creature._status.Damage);
    }

}
