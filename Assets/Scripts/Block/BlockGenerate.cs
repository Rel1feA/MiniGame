using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGenerate : MonoBehaviour
{
    [SerializeField]
    private int width;
    [SerializeField]
    private int height;
    [SerializeField]
    private int interval;//格子间隔
    [SerializeField]
    private Vector2 startPos;
    [SerializeField]
    private int dirFloorHeight;
    [SerializeField]
    private int rockFloorHeight;
    [SerializeField]
    private GameData gameData;

    private FloorPercentData floorPercentData;
    

    private void Start()
    {
        floorPercentData = gameData.dirFloorData;
    }

    public void GenerateBlock()
    {
        for(int i=0;i<height;i++)
        {
            for(int j=0;j<width;j++)
            {
                Vector3 pos=startPos+new Vector2(j*interval,i*interval);
                if(i==dirFloorHeight+1)
                {
                    floorPercentData = gameData.rockFloorData;
                }
                int randNum = Random.Range(0, 101);
            }
        }
    }
}
