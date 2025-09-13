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

    [Header("���� ���� �߻�ü ��")]
    public int Again;

    [Header("Ȯ���� ��ų")]
    public float Persent;
    public float PersentTime;

    [Header("������ ��ų")]
    public float SkillTime;
    public float DotDamage;
    public float DotSpeed;

    [Header("�÷��̾� ���� ��ų")]
    public float Hp;
    public float AtkSpeed;
    public float Pdamage;
    public float Speed;

}
