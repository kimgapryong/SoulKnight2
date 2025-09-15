using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElkeSkill : Skill_Base
{
    public List<CreatureData> _monData;
    public List<Transform> fireHole;
    private ElkeCanvas _canvas;
    public void SetCanvas(ElkeCanvas canvas)
    {
        _canvas = canvas;
    }
    public override void Skill1()
    {
        StartCoroutine(CorSkill1());
    }

    private IEnumerator CorSkill1()
    {
        yield return StartCoroutine(_canvas.Skill1());
        yield return new WaitForSeconds(1f);
        foreach (Transform t in fireHole)
        {
            Vector2 dir = Vector2.up;
            GameObject arrow = Manager.Resources.Instantiate("Skills/ElkeFire", t.position, Quaternion.identity);
            ProjectileController projectile = arrow.AddComponent<ProjectileController>();
            projectile.SetInfo(creature, dir, 12, 100, true);
        }
    }
    public override void Skill2()
    {
        StartCoroutine(CorSkill2());
    }
    private IEnumerator CorSkill2()
    {
        yield return StartCoroutine(_canvas.Skill2());
        yield return new WaitForSeconds(1f);

        Vector2 dir = Vector2.up;
        GameObject arrow = Manager.Resources.Instantiate("Skills/ElkeFire", transform.position, Quaternion.identity);
        ProjectileController projectile = arrow.AddComponent<ProjectileController>();
        projectile.SetInfo(creature, dir, 12, 100, true);
    }

    public override void Skill3()
    {
        foreach (CreatureData monData in _monData)
        {
            MonsterController mon = Manager.Creature.CreateMonster(monData);
            mon.gameObject.transform.position = creature.target.transform.position + new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f));
        }
    }

    public override void Skill4()
    {
        
    }
}
