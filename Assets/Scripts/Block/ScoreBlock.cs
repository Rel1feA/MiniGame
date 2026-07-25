using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBlock : Block
{
    [SerializeField]
    private int score;

    public override void BeDestoryed()
    {
        //得分增加
        base.BeDestoryed();
    }
}
