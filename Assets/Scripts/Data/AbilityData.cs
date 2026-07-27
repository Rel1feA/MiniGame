using UnityEngine;

[CreateAssetMenu(menuName = "Data/Ability")]
public class AbilityData : ScriptableObject
{
    public int id;
    public string abilityTitleName;                 
    public Sprite icon;
    [TextArea]
    public string describtion;
}