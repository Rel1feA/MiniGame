using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RECode.REFramework;
using UnityEngine.UI;
using TMPro;

public class GameUIPanel : BasePanel
{
    private void Update()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        GetControl<Text>("RemainTime").text =$"剩余时间{GameManager.Instance.RemainTime}";
        GetControl<Text>("CurrentScore").text=$"当前分数{GameManager.Instance.score}";
        GetControl<Text>("TargetScore").text = $"目标分数{GameManager.Instance.currentLevelData.targetScore}";
    }
}
