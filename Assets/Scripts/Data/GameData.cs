using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName ="GameData",menuName ="Data/Game")]
public class GameData : ScriptableObject
{
    public FloorPercentData dirFloorData;
    public FloorPercentData rockFloorData;
}

[Serializable]
public class FloorPercentData
{
    public int dirPercent;
    public int stonePercent;
    public int bedRockPercent;
    public int treasureBoxPercent;
    public int orePercent;
}
