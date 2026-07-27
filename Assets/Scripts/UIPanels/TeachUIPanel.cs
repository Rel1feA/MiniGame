using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RECode.REFramework;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TeachUIPanel : BasePanel
{
    private void Start()
    {
        UIManager.AddCustomEventListener(GetControl<Image>("BG"), EventTriggerType.PointerClick, (panel) =>
        {
            UIManager.Instance.HidePanel("TeachUIPanel");
        });
    }
}
