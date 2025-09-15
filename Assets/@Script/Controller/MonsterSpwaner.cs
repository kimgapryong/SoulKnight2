using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpwaner : BaseController
{
    public List<CreatureData> _monData = new List<CreatureData>();
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
        {
            Debug.LogWarning("½ÇÇà");
            mCor = StartCoroutine(StartSpwan());
        }
            
            
    }
    private void MonsterSpwan()
    {
        foreach (CreatureData monData in _monData)
        {
            MonsterController mon = Manager.Creature.CreateMonster(monData);
            mon.gameObject.transform.position = transform.position + new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f));
            m_Spwaner.Add(mon);
            mon.SetSpwanList(this);
        }
    }

    private IEnumerator StartSpwan()
    {
        yield return new WaitForSeconds(timer); 
        MonsterSpwan();
        mCor = null;
    }
}
