using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpwaner : BaseController
{
    public GameObject monsters;
    public List<MonsterController> m_Spwaner = new List<MonsterController>();
    public float timer;
    private Coroutine mCor;
    public override bool Init()
    {
        if(base.Init() == false)
            return false;

        return true;
    }
    private void Update()
    {
        if (m_Spwaner.Count <= 0 && mCor == null)
            mCor = StartCoroutine(StartSpwan());
    }
    private void MonsterSpwan()
    {
        GameObject clone = Instantiate(monsters, transform.position, Quaternion.identity);
        clone.name = monsters.name;
        foreach(MonsterController m in clone.transform.GetComponentsInChildren<MonsterController>(true))
        {
            m_Spwaner.Add(m);
            m.SetSpwanList(this);
        }
            
    }

    private IEnumerator StartSpwan()
    {
        yield return new WaitForSeconds(timer); 
        MonsterSpwan();
        mCor = null;
    }
}
