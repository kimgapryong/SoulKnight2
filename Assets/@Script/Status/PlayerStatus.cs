using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : Status
{
    private const int MAXLEVEL = 20;
    private Define.HeroType _type;

    public Action<int> levelAction;
    public Action<int> skillAction;

    private int _point;
    public int Point
    {
        get { return _point; }  
        set
        {
            _point = value;
            skillAction?.Invoke(value);
        }
    }
    public override void SetData(CreatureData data)
    {
        base.SetData(data);
        _type = data.HeroType;
    }

    public void LevelUp()
    {
        switch (_type)
        {
            case Define.HeroType.Archer:
                StatusUp(15, 8, 1.3f, 1.2f);
                break;
            case Define.HeroType.Magician:
                StatusUp(10, 10, 1.15f, 1.6f);
                break;
            case Define.HeroType.Warrior:
                StatusUp(20, 10, 1.5f, 1.2f);
                break;
        }
        Level++;
        Speed *= 1.2f;
        Exp *= 1.6f;
        Point++;

        Debug.Log(Exp);
        levelAction?.Invoke(Level);
    }

     private void StatusUp(float hp, float damage, float defence, float mp)
    {
        Hp += hp;
        CurHp = Hp;
        Damage += damage;
        Defence *= defence;
        Mp *= mp;
        CurMp = Mp;
    }
    public void AddExp(float amount)
    {
        if (Level >= MAXLEVEL)
        {
            CurExp = 0;
            return;
        }

        CurExp += amount;
        while (CurExp >= Exp)
        {
            CurExp -= Exp;
            LevelUp();
        }
    }
}
