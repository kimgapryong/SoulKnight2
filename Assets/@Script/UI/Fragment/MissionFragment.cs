using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class MissionFragment : UI_Base
{
    enum Texts
    {
        Mission_Txt
    }
    private Define.MonsterType _type;
    private string missionName;
    public override bool Init()
    {
        if(base.Init() == false )
            return false;

        BindText(typeof(Texts));

        switch (_type)
        {
            case Define.MonsterType.Stage1:
                missionName = "1단계";
                break;
            case Define.MonsterType.Stage2:
                missionName = "2단계";
                break;
            case Define.MonsterType.Stage3:
                missionName = "3단계";
                break;
        }

        Refresh();
        return true;
    }
    public void SetInfo(Define.MonsterType type)
    {
        _type = type;
    }
    public void Refresh()
    {
        MissionData? data = Manager.Game.GetMission(_type);

        if( data == null)
        {
            Debug.LogError("왜여요ㅙ");
            return;
        }
            
        GetText((int)Texts.Mission_Txt).text = $"{missionName}: {data.Value.CurMonster}/{data.Value.MaxMonster}";        
    }

}
