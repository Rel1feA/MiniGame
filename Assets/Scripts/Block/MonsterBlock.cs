using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBlock : Block
{
    [SerializeField]
    private float monsterPercent;

    public override void BeDestoryed()
    {
        int rand = Random.Range(0, 101);
        if(rand<monsterPercent)
        {
            Debug.Log("出现蝙蝠");
        }
        base.BeDestoryed();
    }
}
