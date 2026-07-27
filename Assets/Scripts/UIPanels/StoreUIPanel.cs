using RECode.REFramework;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class StoreUIPanel : BasePanel
{

    private void Start()
    {
        UIManager.AddCustomEventListener(GetControl<Image>("Card1BG"), EventTriggerType.PointerEnter, (value) =>
        {
            GetControl<Image>("Card1BG").rectTransform.DOScale(1.2f, 0.2f);
        });
        UIManager.AddCustomEventListener(GetControl<Image>("Card1BG"), EventTriggerType.PointerExit, (value) =>
        {
            GetControl<Image>("Card1BG").rectTransform.DOScale(1f, 0.2f);
        });
        UIManager.AddCustomEventListener(GetControl<Image>("Card2BG"), EventTriggerType.PointerEnter, (value) =>
        {
            GetControl<Image>("Card2BG").rectTransform.DOScale(1.2f, 0.2f);
        });
        UIManager.AddCustomEventListener(GetControl<Image>("Card2BG"), EventTriggerType.PointerExit, (value) =>
        {
            GetControl<Image>("Card2BG").rectTransform.DOScale(1f, 0.2f);
        });
        UIManager.AddCustomEventListener(GetControl<Image>("Card3BG"), EventTriggerType.PointerEnter, (value) =>
        {
            GetControl<Image>("Card3BG").rectTransform.DOScale(1.2f, 0.2f);
        });
        UIManager.AddCustomEventListener(GetControl<Image>("Card3BG"), EventTriggerType.PointerExit, (value) =>
        {
            GetControl<Image>("Card3BG").rectTransform.DOScale(1f, 0.2f);
        });
        UIManager.AddCustomEventListener(GetControl<Image>("Card1BG"), EventTriggerType.PointerClick, (value) =>
        {
            AbilitySystem.Instance.HandleAbility(AbilitySystem.Instance.currentAbility[0].id, GameManager.Instance.player);
            GameManager.Instance.NextLevel();
            UIManager.Instance.HidePanel("StoreUIPanel");
        });
        UIManager.AddCustomEventListener(GetControl<Image>("Card2BG"), EventTriggerType.PointerClick, (value) =>
        {
            AbilitySystem.Instance.HandleAbility(AbilitySystem.Instance.currentAbility[1].id, GameManager.Instance.player);
            GameManager.Instance.NextLevel();
            UIManager.Instance.HidePanel("StoreUIPanel");
        });
        UIManager.AddCustomEventListener(GetControl<Image>("Card3BG"), EventTriggerType.PointerClick, (value) =>
        {
            AbilitySystem.Instance.HandleAbility(AbilitySystem.Instance.currentAbility[2].id, GameManager.Instance.player);
            GameManager.Instance.NextLevel();
            UIManager.Instance.HidePanel("StoreUIPanel");
        });
    }

    public void UpdateUI(List<AbilityData> abilityDatas)
    {
        Debug.Log("UpdateUI");
        GetControl<Text>("C1Title").text = abilityDatas[0].abilityTitleName;
        GetControl<Image>("C1Icon").sprite = abilityDatas[0].icon;
        GetControl<Text>("C1Description").text = abilityDatas[0].describtion;
        GetControl<Text>("C2Title").text = abilityDatas[1].abilityTitleName;
        GetControl<Image>("C2Icon").sprite = abilityDatas[1].icon;
        GetControl<Text>("C2Description").text = abilityDatas[1].describtion;
        GetControl<Text>("C3Title").text = abilityDatas[2].abilityTitleName;
        GetControl<Image>("C3Icon").sprite = abilityDatas[2].icon;
        GetControl<Text>("C3Description").text = abilityDatas[2].describtion;
    }
}
