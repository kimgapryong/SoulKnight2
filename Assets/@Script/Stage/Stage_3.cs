using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage_3 : Stage_Base
{
    public Transform pos;
    public BossController boss;
    protected override void Complete()
    {
        Debug.Log("컴플리트");
        foreach (var player in Manager.Creature._playerList)
            player.transform.position = pos.position + Vector3.right * 0.5f;

        StartCoroutine(StartBoss());
    }

    private IEnumerator StartBoss()
    {
        yield return new WaitForSeconds(3f);
        boss.startStage = true;
    }
}
