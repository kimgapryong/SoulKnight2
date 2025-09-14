using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item_Base : BaseController
{
    public ItemData _data;
    public abstract void ItemAblity();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if(player == null)
            return;

        if(Manager.Bag.AddItem(player._type, this))
            transform.position += Vector3.one * 500f;
    }
    protected IEnumerator WaitCool(float tiem, Action callback)
    {
        yield return new WaitForSeconds(tiem);
        callback?.Invoke();
    }
}
