using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RECode.REFramework;

public class AbilitySystem : MonoSingleton<AbilitySystem>
{
    public List<AbilityData> abilityDatas;

    public void GenerateSkill(Player player,int id)
    {
        switch(id)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                player.gameObject.AddComponent<Translate>();
                break;
        }
    }
}
