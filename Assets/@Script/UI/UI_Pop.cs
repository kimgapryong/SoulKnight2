using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Pop : UI_Base
{
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Manager.UI.SetCanvas(gameObject, true);
        return true;
    }
    public virtual void ClosePopupUI()
    {
        Manager.UI.ClosePopUI(this);
    }
}
