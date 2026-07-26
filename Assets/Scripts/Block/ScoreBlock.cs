using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBlock : Block
{
    [SerializeField]
    private float score;
    [SerializeField]
    private SpriteRenderer childSprite;

    public override void BeDestoryed()
    {
        GameManager.Instance.score += score;
        childSprite.enabled = false;
        base.BeDestoryed();
    }
}
