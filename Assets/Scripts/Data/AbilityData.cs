using UnityEngine;

[CreateAssetMenu(menuName = "Data/Ability")]
public class AbilityData : ScriptableObject
{
    public string abilityName;               
    public string abilityClassName;          
    public Sprite icon;
    public float cooldown;
    public float duration;
}