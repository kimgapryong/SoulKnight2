using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Stage_Base : BaseController
{
    public Dictionary<Define.MonsterType, bool> _monCheckDic = new Dictionary<Define.MonsterType, bool>();
    public int[] monValues;
    private bool _stop;
    private bool checkDie;
    public override bool Init()
    {
        if(base.Init() == false)
            return false;

        Manager.Creature.Clear();

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
        if (Manager.Creature._playerList.All(player => player._die))
        {
            if(checkDie)
                return;

            checkDie = true;
            Manager.UI.ShowPopUI<DiePop>();
        }


        if (_stop)
            return;

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
            _stop = true;
        }
    }

    protected virtual void Complete() { }
}
