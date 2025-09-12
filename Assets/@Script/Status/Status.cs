using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Status : MonoBehaviour
{
    public Action<float, float> expAction;
    public Action<float, float> hpAction;
    public Action<float, float> mpAction;

    public string CreatureName {  get; private set; }   
    public Sprite Image { get; private set; }
    public int Level { get; set; }
    public float Exp {  get; set; }
    private float _curExp;
    public float CurExp
    {
        get {  return _curExp; }
        set
        {
            Debug.Log($"ÇöÀç Exp: {Exp}");
            _curExp = value;
            expAction?.Invoke(value, Exp);
        }
    }
    public float Hp { get; set; }
    private float _curHp;
    public float CurHp
    {
        get { return _curHp; }
        set
        {
            _curHp = Mathf.Clamp(value, 0, Hp);
            hpAction?.Invoke(_curHp, Hp);
        }
    }
    public float Mp { get; set; }
    private float _curMp;
    public float CurMp
    {
        get { return _curMp; }
        set
        {
            _curMp = Mathf.Clamp(value, 0, Mp);
            mpAction?.Invoke(_curMp, Mp);   
        }
    }
    public float Damage { get; set; }
    public float Defence { get; set; }
    public float AtkSpeed { get; set; }
    public float Speed { get; set; }
    public float Critical { get; set; }
    public float Arange { get; set; }
    public float MoveArange { get; set; }

    public virtual void SetData(CreatureData data)
    {
        CreatureName = data.CreatureName;
        Image = data.Image;
        Level = data.Level;
        Exp = data.Exp;
        Hp = data.Hp;
        CurHp = Hp;
        Mp = data.Mp;
        CurMp = Mp;
        Damage = data.Damage;
        Defence = data.Defence;
        AtkSpeed = data.AtkSpeed;
        Speed = data.Speed;
        Critical = data.Critical;
        Arange = data.Arange;
        MoveArange = data.MoveArange;
    }

}
