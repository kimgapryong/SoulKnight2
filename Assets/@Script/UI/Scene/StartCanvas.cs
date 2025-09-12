using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartCanvas : UI_Scene
{
    enum Texts
    {
        Start_Txt,
        Explain_Txt,
        Exit_Txt,
    }
    public override bool Init()
    {
        if(base.Init() == false)
            return false;

        BindText(typeof(Texts));

        GetText((int)Texts.Start_Txt).gameObject.BindEvent(() => SceneManager.LoadScene("StartStage"));

        return true;
    }
}
