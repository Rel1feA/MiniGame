using RECode.REFramework;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;


public class GameUIPanel : BasePanel
{
    private void Start()
    {
        UIManager.AddCustomEventListener(GetControl<Image>("TeachIcon"), EventTriggerType.PointerClick, (panel) =>
        {
            UIManager.Instance.ShowPanel<TeachUIPanel>("TeachUIPanel",E_UI_Canvas.Dynamic,E_UI_Layer.System);
        });
    }

    private void Update()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        GetControl<Text>("RemainTime").text =$"剩余时间:{GameManager.Instance.RemainTime}";
        GetControl<Text>("CurrentScore").text=$"当前分数:{GameManager.Instance.score}";
        GetControl<Text>("TargetScore").text = $"目标分数:{GameManager.Instance.currentLevelData.targetScore}";
        GetControl<Text>("DigSpeedMul").text = $"挖掘速度加成:{GameManager.Instance.player.DigSpeedMul * 100:F0}%";
        GetControl<Text>("FlySpeedMul").text = $"飞行速度加成:{GameManager.Instance.player.Movement.FlySpeedMul * 100:F0}%";
    }
}
