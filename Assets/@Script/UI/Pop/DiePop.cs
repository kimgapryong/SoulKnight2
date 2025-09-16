using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DiePop : UI_Pop
{
    enum Buttons
    {
        Lobby_Btn,
        RePlay_Btn,
    }
    public override bool Init()
    {
        if(base.Init() == false)    
            return false;

        BindButton(typeof(Buttons));
        GetButton((int)Buttons.Lobby_Btn).gameObject.BindEvent(() => { Manager.Instance.Clear(); SceneManager.LoadScene("SampleScene"); });
        GetButton((int)Buttons.RePlay_Btn).gameObject.BindEvent(() => { Manager.Instance.Clear(); SceneManager.LoadScene("StartStage"); });

        return true;
    }
    
}
