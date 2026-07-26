using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName ="GameData",menuName ="Data/Game")]
public class GameData : ScriptableObject
{
    public FloorPercentData surfaceFloorData;
    public FloorPercentData dirtFloorData;
    public FloorPercentData rockFloorData;
    public float startLevelTime;
}

[Serializable]
public class FloorPercentData
{
    public List<BlockData> blockDatas;
}

[Serializable]
public class BlockData
{
    public string prefabName;
    public int weight;
}
