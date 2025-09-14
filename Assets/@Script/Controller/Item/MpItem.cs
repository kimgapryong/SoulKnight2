using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MpItem : Item_Base
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
        player._status.CurMp += player._status.Mp * (_data.Precent / 100);
        StartCoroutine(WaitCool(2f, () => player.potion = false));
    }
}
