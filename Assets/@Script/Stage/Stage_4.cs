using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEditor.PlayerSettings;

public class Stage_4 : Stage_Base
{
    public Tilemap tile;
    public Transform bossPos;
    public BossController boss;
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        StartCoroutine(LavaTrue());

        return true;
    }
    protected override void Complete()
    {
        Debug.Log("컴플리트");
        foreach (var player in Manager.Creature._playerList)
            player.transform.position = bossPos.position + Vector3.right * 0.5f;

        StartCoroutine(StartBoss());
    }

    private IEnumerator StartBoss()
    {
        yield return new WaitForSeconds(3f);
        boss.startStage = true;
    }

    private IEnumerator LavaTrue()
    {
        while (true)
        {
            tile.gameObject.SetActive(true);
            yield return new WaitForSeconds(4f);
            tile.gameObject.SetActive(false);
            yield return new WaitForSeconds(4f);
        }
    }
}
