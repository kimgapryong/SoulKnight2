using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpFragment : UI_Base
{
    enum Images
    {
        Hp
    }
    enum Texts
    {
        Hp_Txt,
    }

    Status status;
    Transform p;
    public override bool Init()
    {
        if(base.Init() == false)
            return false;

        BindImage(typeof(Images));
        BindText(typeof(Texts));
        p = transform.parent;

        HpAction(status.CurHp, status.Hp);
        status.hpAction = HpAction;

        return true;
    }

    public void SetInfo(Status status)
    {
        this.status = status;
    }
    private void HpAction(float cur, float max)
    {
        GetText((int)Texts.Hp_Txt).text = $"{cur}/{max}";
        GetImage((int)Images.Hp).fillAmount = cur / max;
    }
    void LateUpdate()
    {
        if (p == null) return;

        var local = p.eulerAngles;
        transform.localEulerAngles = local;
    }
}
