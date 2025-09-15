using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New CreatureData", menuName = "CreatureData")]
public class CreatureData : ScriptableObject 
{
    [Header("���")]
    public string CreatureName;
    public Sprite Image;
    public string Path;

    [Header("�������ͽ�")]
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

    [Header("�÷��̾�")]
    public Define.HeroType HeroType;

    [Header("����")]
    public Define.MonsterType MonsterType;
    public float Amount;
}
