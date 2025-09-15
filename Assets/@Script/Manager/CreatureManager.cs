using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CreatureManager
{
    private const int MAXCOUNT = 3;
    public List<PlayerController> _playerList = new List<PlayerController>();
    public List<MonsterController> _monsterList = new List<MonsterController>();
    public PlayerController CreatePlayer(CreatureData data)
    {
        PlayerController pla = null;
        if (_playerList.Count >= MAXCOUNT)
        {
            //플레이어 교체 UI
            return null;
        }

        Manager.Resources.Instantiate($"Players/{data.Path}", callback: (go) =>
        {
            PlayerController player = go.GetOrAddComponent<PlayerController>();
            PlayerStatus status = go.GetOrAddComponent<PlayerStatus>();

            go.GetOrAddComponent<PlayerState>();
            go.GetOrAddComponent<AutoState>();

            status.SetData(data);
            player.SetStatus(status);

            pla = player;
            _playerList.Add(player);
        });

        return pla;
    }
    public MonsterController CreateMonster(CreatureData data)
    {
        MonsterController mon = null;
        Manager.Resources.Instantiate($"Monsters/{data.Path}", callback: (go) =>
        {
            MonsterController monster = go.GetOrAddComponent<MonsterController>();
            monster._type = data.MonsterType;

            MonsterStatus status = go.GetOrAddComponent<MonsterStatus>();

            Manager.UI.MakeSubItem<HpFragment>(monster.transform, "HpCanvas", (fa) =>
            {
                fa.SetInfo(status);
                fa.GetComponent<Canvas>().sortingOrder = 3;
            });

            status.SetData(data);
            monster.SetStatus(status);

            mon = monster;
            _monsterList.Add(monster);
        });
        return mon;
    }
    public PlayerController SearchPlayer(Transform monster, PlayerController pla = null)
    {
        PlayerController curPlayer = null;
        float minValue = float.MaxValue;
        foreach(PlayerController player in _playerList)
        {
            if (pla == player)
                continue;

            float curValue = Vector2.Distance(monster.transform.position, player.transform.position);
            if (curValue < minValue)
            {
                minValue = curValue;
                curPlayer = player;
            }
        }
        return curPlayer;
    }
    public List<PlayerController> SearchAllPlayer(CreatureController creature)
    {
        List<PlayerController> playerList = new List<PlayerController>();
        foreach(var player in _playerList)
        {
            if(Vector2.Distance(player.transform.position, creature.transform.position) <= creature._status.Arange)
                playerList.Add(player);
        }
        return playerList;
    }
    public PlayerController SearchNonDeathPlayer(PlayerController player)
    {
        foreach(var pla in _playerList)
        {
            if (pla._die == true)
                return pla;
        }

        return null;
    }
    public MonsterController SearchMonster(Transform player, MonsterController mon = null)
    {
        MonsterController curMonster = null;
        float minValue = float.MaxValue;
        foreach(MonsterController monster in _monsterList)
        {
            if (monster == null || mon == monster) continue;
            float curValue = Vector2.Distance(player.position, monster.transform.position);
            if(curValue < minValue)
            {
                minValue = curValue;
                curMonster = monster;
            }
        }
        return curMonster;
    }
    public List<MonsterController> SearchAllMonster(PlayerController player)
    {
        List<MonsterController> _monList = new List<MonsterController>();
        foreach(var mon in _monsterList.ToList())
        {
            if (Vector2.Distance(mon.transform.position, player.transform.position) <= 10f)
                _monList.Add(mon);
        }
        return _monList;
    }

    public void SetState(PlayerController player, Define.State state)
    {
        foreach(PlayerController pla in _playerList)
        {
            if(state == Define.State.Attack)
            {
                pla.isAtk = true;
                if (pla == player)
                    pla.autoState.StopAutoSkill();
                else
                    pla.autoState.StartAutoSkill();

            }
                
            if(state == Define.State.Move)
                pla.isAtk = false;

           
                /*  if(pla == player || pla.State == state || player._ai == Define.AI.Player)
                      continue;*/

                pla.State = state;
        }
    }
    public void ChooiseRandomPlayer()
    {
        PlayerController pla = _playerList[Random.Range(0, _playerList.Count)];
        if(pla == null)
            return;

        Manager.Instance.ChangePlayer(pla);
    }

}
