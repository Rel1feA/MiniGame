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
    [SerializeField]
    private int firstFloor;
    [SerializeField]
    private int secondFloor;

    private FloorPercentData floorPercentData;

    private void Start()
    {
        GenerateBlock();
    }

    private void GenerateBlock()
    {
        floorPercentData = gameData.dirtFloorData;
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                Vector3 pos = startPos.position + new Vector3(j * interval, -i * interval);
                if (i == firstFloor + 1)
                {
                    floorPercentData = gameData.surfaceFloorData;
                }
                else if(i == secondFloor + 1)
                {
                    floorPercentData = gameData.rockFloorData;
                }
                int randNum = Random.Range(0, 100);
                int cumulative = 0;
                string blockName = "Dirt";
                foreach (var blockData in floorPercentData.blockDatas)
                {
                    cumulative += blockData.weight;
                    if (randNum <= cumulative)
                    {
                        blockName = blockData.prefabName;
                        break;
                    }
                }
                ResourcesManager.Instance.LoadAsync<GameObject>($"Prefabs/Blocks/{blockName}", (o) =>
                {
                    o.transform.position=pos;
                    o.transform.parent = transform;
                });
            }
        }
    }
}
