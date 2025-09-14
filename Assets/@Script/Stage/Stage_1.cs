using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage_1 : Stage_Base
{
    public List<CreatureData> _plaData;
    public List<CreatureData> _monData;
    public int monCount = 100;
    public override bool Init()
    {
        if(base.Init() == false)
            return false;

        GameObject cam = Manager.Resources.Instantiate("Main Camera");
        DontDestroyOnLoad(cam);

        Vector3 pos = Vector3.one;
        foreach(CreatureData data in _plaData)
        {
            PlayerController pla = Manager.Creature.CreatePlayer(data);
            Vector3 newPos = pos + Vector3.one * 1.5f;
            pla.transform.position = newPos;
            pos = newPos;

            DontDestroyOnLoad(pla);
        }
        
        foreach(CreatureData monData in _monData)
        {
            for(int i = 0; i < monCount; i++)
            {
                MonsterController mon = Manager.Creature.CreateMonster(monData);
                mon.gameObject.transform.position = new Vector3(Random.Range(-15f, 20f), Random.Range(-18f, 30f));
            }
        }

        Manager.UI.ShowSceneUI<MainCanvas>();
        Manager.Creature.ChooiseRandomPlayer();
        return true;
    }
}

