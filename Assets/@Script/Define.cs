using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum AI
    {
        Auto,
        Player,
    }
    public enum HeroType
    {
        Warrior,
        Archer,
        Magician,
    }
    public enum State
    {
        Move,
        Attack,
        Idle,
    }
    public enum Skill
    {
        Skill1,
        Skill2,
        Skill3,
        Skill4,
    }
    public enum Item
    {
        Fight,
        Max,
        Magic,
        Hp1,
        Mp1,
    }

    public enum ColorType
    {
        Poison,
    }
    public static Color GetColor(ColorType type)
    {
        switch (type)
        {
            case ColorType.Poison:
                return new Color(139, 0, 225);
                
        }

        return Color.white;
    }
   
}
