using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RECode.REFramework;

public class AbilitySystem : MonoSingleton<AbilitySystem>
{
    public List<AbilityData> abilityDatas;
    public List<AbilityData> currentAbility;
    private Dictionary<int, AbilityData> abilityDataDic=new Dictionary<int, AbilityData>();

    private void Start()
    {
        CacheAbilityData();
    }

    public AbilityData GetAbilityData(int id)
    {
        if(abilityDataDic.TryGetValue(id, out AbilityData abilityData))
        {
            return abilityData;
        }
        return null;
    }

    private void CacheAbilityData()
    {
        foreach(var data in abilityDatas)
        {
            abilityDataDic[data.id]= data;
        }
    }

    public void HandleAbility(int id,Player player)
    {
        switch (id)
        {
            case 1:
                player.Movement.AddFlySpeedMul(0.5f);
                break;
            case 2:
                player.AddDigSpeedMul(0.5f);
                break;
            case 3:
                player._Boom.count++;
                break;
            case 4:
                player._Digpotion.count++;
                break;
            case 5:
                player._Translate.count++;
                break;
        }
    }

    public void GenerateStore(int count)
    {
        currentAbility.Clear();
        if (count > abilityDatas.Count)
        {
            Debug.LogWarning("输入的Count值范围太大");
        }
        HashSet<int> selected = new HashSet<int>();
        while (selected.Count < count)
        {
            int num = Random.Range(1,abilityDatas.Count+1);
            selected.Add(num); // 如果重复，Add 返回 false，但不影响
        }
        int[] selecedAbilityID=new List<int>(selected).ToArray();
        for(int i=0;i<selecedAbilityID.Length;i++)
        {
            currentAbility.Add(GetAbilityData(selecedAbilityID[i]));
        }
        UIManager.Instance.ShowPanel<StoreUIPanel>("StoreUIPanel", E_UI_Canvas.Dynamic, E_UI_Layer.Top, (panel) =>
        {
            panel.UpdateUI(currentAbility);
        });
    }
}
