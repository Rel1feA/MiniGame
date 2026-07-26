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
    public float levelTime;
    public List<LevelData> levelDatas;
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

[Serializable]
public class LevelData
{
    public int levelIndex;
    public int treasureScore;
    public int oreScore;
    public int targetScore;
    public int monsterScore;
}
