using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CreatureController : BaseController
{
    public Status _status;
    public Vector2 dir;
    public Vector2 endPoint;
    public CreatureController target;
    public Skill_Base _skill;

    protected bool _damage;
    public bool _die;

    public SpriteRenderer sp;
    public Animator anim;
    public Rigidbody2D rb;

    public float Speed { get; protected set; }
    private Coroutine _poison;

    private Define.State _state;
    public Define.State State
    {
        get { return _state; }
        set
        {
            _state = value;
            ChangeAnim(value);
        }
    }
    public override bool Init()
    {
        if(base.Init() ==false)
            return false;

        sp = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        _skill = GetComponent<Skill_Base>();

        return true;
    }

    private void Update()
    {
        UpdateMethod();
    }
    public virtual void UpdateMethod()
    {
        float localY = (dir.x > 0) ? 0 : (dir.x < 0) ? -180 : transform.eulerAngles.y;
        Vector3 newPos = transform.eulerAngles;
        newPos.y = localY;  
        transform.eulerAngles =  newPos;

        switch (State)
        {
            case(Define.State.Idle):
                Idle();
                break;
            case(Define.State.Move):
                Move();
                break;
            case(Define.State.Attack):
                Attack();
                break;
        }
    }

    protected virtual void ChangeAnim(Define.State type) { }
    protected virtual void Move() { }
    protected virtual void Attack() { }
    protected virtual void Idle() { }
    public virtual void NormalAtk() { }

    public virtual void OnDamage(CreatureController attker, float damage)
    {
        if(_die || _damage)
            return;

        _damage = true;
        sp.color = Color.red;
        _status.CurHp -= damage;

        if(_poison != null)
            StartCoroutine(WaitCool(0.2f, () => { _damage = false; sp.color = Define.GetColor(Define.ColorType.Poison); }));
        else
            StartCoroutine(WaitCool(0.2f, () => { _damage = false; sp.color = Color.white; }));

        if(_status.CurHp <=0)
            OnDie(attker);
    }
    protected virtual void OnDie(CreatureController attker) { _die = true; }
    public IEnumerator WaitCool(float time, Action callback)
    {
        yield return new WaitForSeconds(time);
        callback?.Invoke();
    }

    public void Heal(float heal)
    {
        sp.color = Color.green;
        _status.CurHp += heal;

        StartCoroutine(WaitCool(0.2f, () => { sp.color = Color.white; }));
    }
    public void StartPoison(float time, float damage, float speed)
    {
        if (_poison != null)
            StopCoroutine(_poison);

        _poison = StartCoroutine(Poison(time, damage, speed));
    }
    private IEnumerator Poison(float time, float damage, float speed)
    {
        float timer = 0f;
        float poisonTimer = 0f;

        sp.color = Define.GetColor(Define.ColorType.Poison);
        _status.Speed = Speed * (speed / 100f);
        
        while(timer < time)
        {
            poisonTimer += Time.deltaTime;
            timer += Time.deltaTime;    

            if(poisonTimer >= 1f)
            {
                _status.CurHp -= damage;
                poisonTimer = 0f;
            }
            yield return null;
        }

        sp.color = Color.white;
        _status.Speed = Speed;
        _poison = null;
    }
}
