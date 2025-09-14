using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicItem : Item_Base
{
    public override void ItemAblity()
    {
        PlayerController player = Manager.Player;

        if (player._magicCor != null)
            StopCoroutine(player._magicCor);

        player.magic = true;
        player._magicCor = StartCoroutine(WaitCool(_data.CoolTime, () => { player.magic = false; player._magicCor = null; }));
    }
}
