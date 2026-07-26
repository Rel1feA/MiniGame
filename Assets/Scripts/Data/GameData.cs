using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName ="GameData",menuName ="Data/Game")]
public class GameData : ScriptableObject
{
    public FloorPercentData dirtFloorData;
    public FloorPercentData rockFloorData;
    public Sprite dirtUp;
    public Sprite dirtDown;
    public Sprite dirtMid;
    public Sprite dirtLeftUp;
    public Sprite dirtRightUp;
    public Sprite dirtLeftMid;
    public Sprite dirtRightMid;
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
