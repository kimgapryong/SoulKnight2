using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpItem : Item_Base
{
    public override void ItemAblity()
    {
        PlayerController player = Manager.Player;
        if (player.potion)
        {
            Manager.Bag.AddItem(player._type, this);
            return;
        }
        
        player.potion = true;
        player._status.CurHp += player._status.Hp * (_data.Precent / 100);
        StartCoroutine(WaitCool(2f, () => player.potion = false));
    }
}
