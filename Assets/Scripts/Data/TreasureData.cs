using UnityEngine;

[CreateAssetMenu(menuName = "Data/Treasure")]
public class TreasureData : ScriptableObject
{
    public string treasureName;              
    public string description;               
    public Sprite icon;                      
    public GameObject dropPrefab;            
    public AbilityData unlockAbility;                            
    public int minDepth;                     // 最小生成深度
    public int maxDepth;                     // 最大生成深度
}
