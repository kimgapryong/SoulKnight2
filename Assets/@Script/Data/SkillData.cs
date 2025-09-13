using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="SkillData", menuName ="New SkillData")]
public class SkillData : ScriptableObject
{
    public string SkillName;
    public Sprite Image;
    public string Explain; 

    public int Target;
    public int SkillPoint;
    public float Damage;
    public float Mp;
    public float CoolTime;
    public float SkillArange;

    [Header("연속 공격 발사체 수")]
    public int Again;

    [Header("확률형 스킬")]
    public float Persent;
    public float PersentTime;

    [Header("지속형 스킬")]
    public float SkillTime;
    public float DotDamage;
    public float DotSpeed;

    [Header("플레이어 스탯 스킬")]
    public float Hp;
    public float AtkSpeed;
    public float Pdamage;
    public float Speed;

}
