using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage_Base : BaseController
{
    public Dictionary<Define.MonsterType, bool> _monCheckDic = new Dictionary<Define.MonsterType, bool>();
    public int[] monValues;

    public override bool Init()
    {
        if(base.Init() == false)
            return false;

        MainCanvas main = Manager.UI.SceneUI as MainCanvas;
        main.MissionDeSet();

        if(monValues.Length > 0)
            Manager.Game.SetMission(this, monValues);

        foreach(var player in Manager.Creature._playerList)
            player.transform.position = transform.position + Vector3.one * 0.5f;

        return true;
    }

    private void Update()
    {
        bool allTrue = true;
        foreach (var item in _monCheckDic.Values)
        {
            if (!item)
            {
                allTrue = false;
                break;
            }
        }

        if (allTrue)
        {
            Complete();
            allTrue = false;
        }
    }

    protected virtual void Complete() { }
}
