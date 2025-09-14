using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxItem : Item_Base
{
    public override void ItemAblity()
    {
        PlayerController player = Manager.Creature.SearchNonDeathPlayer(Manager.Player);

        if(player == null)
            return;

        player._status.CurHp = player._status.Hp;
        player._die = false;
    }
}
