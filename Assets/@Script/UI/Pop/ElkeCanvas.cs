using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElkeCanvas : UI_Pop
{
    enum Objects
    {
        Skill1,
        Skill2
    }
    public override bool Init()
    {
        if(base.Init() == false)
            return false;
        BindObject(typeof(Objects));
        GetObject((int)Objects.Skill1).gameObject.SetActive(false);
        GetObject((int)Objects.Skill2).gameObject.SetActive(false);
        return true;
    }

    public IEnumerator Skill1()
    {
        for(int i = 0; i < 3;  i++)
        {
            GetObject((int)Objects.Skill1).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            GetObject((int)Objects.Skill1).gameObject.SetActive(false);
        }
        
    }
    public IEnumerator Skill2()
    {
        for (int i = 0; i < 3; i++)
        {
            GetObject((int)Objects.Skill2).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            GetObject((int)Objects.Skill2).gameObject.SetActive(false);
        }

    }
}
