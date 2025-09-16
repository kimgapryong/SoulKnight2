using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private static Manager _instance;
    public static Manager Instance { get { Init(); return _instance; } }

    private ResourcesManager _resources = new ResourcesManager();
    public static ResourcesManager Resources { get { return Instance._resources; } }
    private UIManager _ui = new UIManager();
    public static UIManager UI { get { return Instance._ui; } }
    private CreatureManager _creature = new CreatureManager();
    public static CreatureManager Creature { get { return Instance._creature; } }
    private SkillManager _skill = new SkillManager();
    public static SkillManager Skill { get { return Instance._skill; } }
    private RandomManager _random = new RandomManager();
    public static RandomManager Random { get { return Instance._random; } }
    private BagManager _bag = new BagManager();
    public static BagManager Bag { get { return Instance._bag; } }  
    private GameManager _game = new GameManager();
    public static GameManager Game { get { return Instance._game; } }   
    public static PlayerController Player { get; private set; }
    public static InputController Input { get;  set; }

    public Action ChangeAction;
    public void ChangePlayer(PlayerController player)
    {
        Player = player;
        foreach(var pla in Creature._playerList)
        {
            if(pla == player)
            {
                pla.ChangeMode(Define.AI.Player);
                continue;
            }
            pla.ChangeMode(Define.AI.Auto);
        }

        Debug.Log(ChangeAction);
        ChangeAction?.Invoke();
    }
    private static void Init()
    {
        if (_instance != null) return;

        GameObject go = GameObject.Find("@Manager");
        if(go == null)
        {
            go = new GameObject("@Manager");
            go.AddComponent<Manager>();
        }
        _instance = go.GetComponent<Manager>();
        DontDestroyOnLoad(go);

        Bag.Init();
    }
    public void Clear()
    {
        Destroy(Input._team.gameObject);
        Destroy(Input.gameObject);
        Input = null;

        Destroy(Camera.main.gameObject);
        foreach(var pla in Creature._playerList)
            Destroy(pla.gameObject);

        Creature._playerList.Clear();

        UI.CloseAllPopupUI();
        Destroy(UI.Root.gameObject);
       
    }
}
