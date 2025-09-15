using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    private Dictionary<Define.MonsterType, MissionData?> _missionDic = new Dictionary<Define.MonsterType, MissionData?>();
    public void SetMission(Stage_Base stage, int[] monValues)
    {
        int index = 0;

        foreach (Define.MonsterType mon in System.Enum.GetValues(typeof(Define.MonsterType)))
        {
            if(monValues.Length <= index)
                break;

            if (monValues[index] > 0)
            {
                stage._monCheckDic.Add(mon, false);
                _missionDic[mon]= new MissionData() { Type = mon, CurMonster = 0,MaxMonster = monValues[index], CurStage = stage };
            }
                
            index++;
        }

        MainCanvas main = Manager.UI.SceneUI as MainCanvas;
        main.MissionRefresh();
    }
    public void AddMissionvalue(Define.MonsterType type)
    {
        MissionData? data = GetMission(type);
        if (data == null) 
            return;

        MissionData mission = data.Value;
        mission.CurMonster++;

        if(mission.CurMonster >= mission.MaxMonster)
            mission.CurStage._monCheckDic[type] = true;

        _missionDic[type] = mission;

        MainCanvas main = Manager.UI.SceneUI as MainCanvas;
        main.MissionFragemntRefresh();
    }
    public MissionData? GetMission(Define.MonsterType type)
    {
        MissionData? missionData = null;
        if(_missionDic.TryGetValue(type, out missionData))
            return missionData;

        return null;
    }
}
public struct MissionData
{
    public Define.MonsterType Type;
    public int CurMonster;
    public int MaxMonster;
    public Stage_Base CurStage;
}

