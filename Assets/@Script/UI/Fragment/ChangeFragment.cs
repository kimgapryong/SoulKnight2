using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeFragment : UI_Base
{
    enum Texts
    {
        Level_Txt,
        Hp_Txt,
        Mp_Txt,
        Exp_Txt
    }
    enum Images
    {
        CharacterImage,
        Hp,
        Mp,
        Exp
    }
    enum Buttons
    {
        Skill_Btn,
    }
    public Define.HeroType hero;
    private Image selectImage;
    public override bool Init()
    {
        if(base.Init() == false)
            return false;

        BindText(typeof(Texts));
        BindImage(typeof(Images));
        BindButton(typeof(Buttons));
        selectImage = GetComponent<Image>();

        Refresh();

        return true;
    }
    public void ChangeCharacter(Define.HeroType type)
    {
        hero = type;
        Refresh();
    }
    public void Refresh()
    {
        PlayerController player = Manager.Skill.GetSkill(hero).creature as PlayerController;
        PlayerStatus status = player.plaStatus;

        HpAction(status.CurHp, status.Hp);
        MpAction(status.CurMp, status.Mp);
        ExpAction(status.CurExp, status.Exp);
        LevelAction(status.Level);

        status.hpAction = HpAction;
        status.mpAction = MpAction;
        status.expAction = ExpAction;
        status.levelAction = LevelAction;

        GetImage((int)Images.CharacterImage).sprite = status.Image;
    }
    private void HpAction(float cur, float max)
    {
        GetText((int)Texts.Hp_Txt).text = $"{cur}/{max}";
        GetImage((int)Images.Hp).fillAmount = cur/max;
    }
    private void MpAction(float cur, float max)
    {
        GetText((int)Texts.Mp_Txt).text = $"{cur}/{max}";
        GetImage((int)Images.Mp).fillAmount = cur / max;
    }
    private void ExpAction(float cur, float max)
    {
        GetText((int)Texts.Exp_Txt).text = $"{cur}/{max}";
        GetImage((int)Images.Exp).fillAmount = cur / max;
    }
    private void LevelAction(int level)
    {
        GetText((int)Texts.Level_Txt).text = $"LEVEL: {level}";
    }
}
