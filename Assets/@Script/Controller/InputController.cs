using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputController : BaseController
{
    public Transform _team;
    private Vector3 worldPos;
    public bool _canSkill = true;
    public override bool Init()
    {
        if(base.Init() == false)    
            return false;

        _team = GameObject.Find("Team").transform;
        Manager.Input = this;
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(_team);
        return true;
    }
    private void Update()
    {
        MoveMouse();
        SetTarget();

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Manager.Instance.ChangePlayer(Manager.Creature._playerList[0]);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Manager.Instance.ChangePlayer(Manager.Creature._playerList[1]);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Manager.Instance.ChangePlayer(Manager.Creature._playerList[2]);
        }

        if (_canSkill)
        {
            if (Input.GetKeyDown(KeyCode.Q))
                Manager.Skill.GetSkill(Manager.Player._type)._skillDic[Define.Skill.Skill1]?.Invoke();
            else if (Input.GetKeyDown(KeyCode.W))
                Manager.Skill.GetSkill(Manager.Player._type)._skillDic[Define.Skill.Skill2]?.Invoke();
            else if (Input.GetKeyDown(KeyCode.E))
                Manager.Skill.GetSkill(Manager.Player._type)._skillDic[Define.Skill.Skill3]?.Invoke();
            else if (Input.GetKeyDown(KeyCode.R))
                Manager.Skill.GetSkill(Manager.Player._type)._skillDic[Define.Skill.Skill4]?.Invoke();
        }
        
    }
    private void SetTarget()
    {
        if(Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
        {
            if(EventSystem.current && EventSystem.current.IsPointerOverGameObject())
                return;

            Collider2D col = Physics2D.OverlapPoint(worldPos, LayerMask.GetMask("Monster"));

            if(col == null) 
                return;
            
            MonsterController monster = col.GetComponent<MonsterController>();
            if(monster == null) return;
            Manager.Player.SetTarget(monster);
        }
    }
  
    private void MoveMouse()
    {
        worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(1) || Input.GetMouseButton(1))
        {
            if (worldPos.x > _team.transform.position.x)
                _team.transform.eulerAngles = Vector3.zero;
            else if (worldPos.x < _team.transform.position.x)
                _team.transform.eulerAngles = new Vector3(0, -180, 0);

            _team.transform.position = worldPos;
            SetMove();
        }
    }
    private void SetMove()
    {
        Vector3 p1 = _team.Find("Pos1").position;
        Vector3 p2 = _team.Find("Pos2").position;
        Vector3 p3 = _team.Find("Pos3").position;

        Manager.Player.SetPoint(p1);

        int order = 0;
        foreach(PlayerController pla in Manager.Creature._playerList)
        {
            if(pla == Manager.Player)
                continue;

            pla.SetPoint(order == 0 ? p2 : p3);
            order++;
        }
    }
}
