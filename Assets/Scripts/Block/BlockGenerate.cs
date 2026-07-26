using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RECode.REFramework;

public class BlockGenerate : MonoBehaviour
{
    [SerializeField]
    private int width;
    [SerializeField]
    private int height;
    [SerializeField]
    private Transform startPos;
    [SerializeField]
    private float interval;
    [SerializeField]
    private string blockPrefabName;
    [SerializeField]
    private GameData gameData;

    private void Start()
    {
        GenerateBlock();
    }

    private void GenerateBlock()
    {
        for(int i=0;i<height;i++)
        {
            for(int j=0;j<width;j++)
            {
                Vector2 pos=startPos.position+new Vector3(j*interval,-i*interval,0);
                ResourcesManager.Instance.LoadAsync<GameObject>($"Prefabs/Blocks/{blockPrefabName}", (o) =>
                {
                    o.transform.position = pos;
                    o.transform.parent = transform;
                    SpriteRenderer spriteRenderer= o.GetComponent<SpriteRenderer>();
                    if(i==0&&j==0)
                    {
                        spriteRenderer.sprite = gameData.dirtLeftUp;
                    }
                    else if(j>0&&j<width&&i==0)
                    {
                        spriteRenderer.sprite = gameData.dirtUp;
                    }
                    else if (i > 0 && i < height&&j==0)
                    {
                        spriteRenderer.sprite = gameData.dirtLeftMid;
                    }
                    else if(i > 0 && i < height && j == height-1)
                    {
                        spriteRenderer.sprite = gameData.dirtRightMid;
                    }
                    else
                    {
                        spriteRenderer.sprite=gameData.dirtMid;
                    }
                });
            }
        }
    }
}
