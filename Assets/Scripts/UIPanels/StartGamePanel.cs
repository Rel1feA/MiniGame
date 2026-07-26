using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RECode.REFramework;
using UnityEngine.UI;

public class StartGamePanel : BasePanel
{
    private void Start()
    {
        GetControl<Button>("StartGameBTN").onClick.AddListener(()=> {
            GameManager.Instance.StartGame();
            UIManager.Instance.HidePanel("StartGamePanel");
        });
        GetControl<Button>("QuitGameBTN").onClick.AddListener(GameManager.Instance.QuitGame);
    }
}
