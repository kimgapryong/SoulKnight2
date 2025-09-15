using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCanvas : UI_Scene
{
    enum Images
    {
        Skill_1,
        Skill_2,
        Skill_3,
        Skill_4,
    }
    enum Objects
    {
        ChangeContent1,
        ChangeContent2,
        ChangeContent3,
        MissionContent,
        Mission
    }
    public List<SkillFragment> _skillList = new List<SkillFragment>();
    public List<ChangeFragment> _changeList = new List<ChangeFragment>();
    public List<SlotFragment> _slotList = new List<SlotFragment>();
    public List<MissionFragment> _missionList = new List<MissionFragment>();

    public bool _skill;
    public bool _change;
    public bool _slot;

    public override bool Init()
    {
        if(base.Init() == false)
            return false;

        BindImage(typeof(Images));
        BindObject(typeof(Objects));
        GetObject((int)Objects.MissionContent).gameObject.SetActive(false);

        foreach(var skill in _skillList)
            skill.SetInfo(this);
        foreach(var change in _changeList)
            change.SetInfo(this);
        foreach(var slot in _slotList)
            slot.SetInfo(this);
        

        Manager.Instance.ChangeAction -= ChangeAction;
        Manager.Instance.ChangeAction += ChangeAction;

        StartCoroutine(WaitAction(ChangeAction));

        return true;
    }

    public void ChangeAction()
    {
        foreach(var skill in _skillList)
        {
            skill.Refresh(Manager.Skill.GetSkill(Manager.Player._type));
        }

        foreach(var change in _changeList)
        {
            if(change.hero == Manager.Player._type)
            {
                change.selectImage.color = Color.blue;
                continue;
            }

            change.selectImage.color = Color.white;
        }
        SlotRefresh();
    }
    public void SlotRefresh()
    {
        PlayerController player = Manager.Player;
        ItemDatas[] datas = Manager.Bag.GetItemDatas(player._type);
        Debug.Log(datas.Length);

        for(int i = 0; i < datas.Length; i++)
        {
            if (datas[i].count == 0)
                _slotList[i].DeSet();
            else
                _slotList[i].Set(datas[i]);
        }
    }
    public void MissionRefresh()
    {
        GetObject((int)Objects.MissionContent).gameObject.SetActive(true);
        foreach (var mission in _missionList)
        {
            Destroy(mission.gameObject);
        }
        _missionList.Clear();

        foreach (Define.MonsterType mon in System.Enum.GetValues(typeof(Define.MonsterType)))
        {
            MissionData? data = Manager.Game.GetMission(mon);

            if(data == null)
                continue;

            Manager.UI.MakeSubItem<MissionFragment>(GetObject((int)Objects.Mission).transform, callback: (fa) =>
            {
                _missionList.Add(fa);
                fa.SetInfo(data.Value.Type);
            });
        }
        
    }
    public void MissionDeSet()
    {
        GetObject((int)Objects.MissionContent).gameObject.SetActive(false);
    }
    public void MissionFragemntRefresh()
    {
        foreach(var mission in _missionList)
        {
            mission.Refresh();
        }
    }
    private IEnumerator WaitAction(Action callback)
    {
        while(!_skill || !_change || !_slot)
        {
            yield return null;
        }
        callback?.Invoke();
    }
}
