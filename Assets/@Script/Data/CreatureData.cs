using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New CreatureData", menuName = "CreatureData")]
public class CreatureData : ScriptableObject 
{
    [Header("경로")]
    public string CreatureName;
    public Sprite Image;
    public string Path;

    [Header("스테이터스")]
    public int Level;
    public float Exp;
    public float Hp;
    public float Mp;
    public float Damage;
    public float Defence;
    public float AtkSpeed;
    public float Speed;
    public float Critical;
    public float Arange;
    public float MoveArange;

    [Header("플레이어")]
    public Define.HeroType HeroType;

    [Header("몬스터")]
    public Define.MonsterType MonsterType;
    public float Amount;
}
