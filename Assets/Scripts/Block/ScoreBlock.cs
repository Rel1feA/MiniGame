using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBlock : Block
{
    [SerializeField]
    private float score;

    public override void BeDestoryed()
    {
        GameManager.Instance.score += score;
        base.BeDestoryed();
    }
}
