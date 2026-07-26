using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RECode.REFramework;

public enum E_SCOREBLOCK
{
    Treasure,
    Ore
}

public class ScoreBlock : Block
{
    [SerializeField]
    private float score;
    [SerializeField]
    private SpriteRenderer childSprite;
    [SerializeField]
    private E_SCOREBLOCK type;

    private void OnEnable()
    {
        EventCenter.Instance.AddListener<LevelData>("NextLevel", UpdateScore);
    }

    private void OnDisable()
    {
        EventCenter.Instance.RemoveListener<LevelData>("NextLevel", UpdateScore);
    }

    public override void BeDestoryed()
    {
        GameManager.Instance.score += score;
        childSprite.enabled = false;
        base.BeDestoryed();
    }

    public void UpdateScore(LevelData levelData)
    {
        switch(type)
        {
            case E_SCOREBLOCK.Treasure:
                score = levelData.treasureScore;
                break;
            case E_SCOREBLOCK.Ore:
                score = levelData.oreScore;
                break;
        }
    }
}
