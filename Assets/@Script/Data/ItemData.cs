using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New ItemData", menuName ="ItemData")]
public class ItemData : ScriptableObject
{
    public Define.Item Type;
    public string ItemName;
    public Sprite Image;

    public float CoolTime;
    public float Precent;
    public int MaxCount;

}
