using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinoSkill : Skill_Base
{
    public override void Skill1()
    {
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
        
    }

    public override void Skill3()
    {
        throw new System.NotImplementedException();
    }

    public override void Skill4()
    {
        throw new System.NotImplementedException();
    }

   
}
