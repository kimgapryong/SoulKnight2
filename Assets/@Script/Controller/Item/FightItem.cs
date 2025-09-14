using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightItem : Item_Base
{
    public override void ItemAblity()
    {
        PlayerController player = Manager.Player;

        if(player._fightCor != null)
            StopCoroutine(player._fightCor);

        player.fight = true;
        player._fightCor = StartCoroutine(WaitCool(_data.CoolTime, () => { player.fight = false; player._fightCor = null; }));
    }
  
}
