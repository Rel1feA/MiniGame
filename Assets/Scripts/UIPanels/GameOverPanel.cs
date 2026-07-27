using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RECode.REFramework;

public class GameOverPanel : BasePanel
{
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            GameManager.Instance.ReStartGame();
        }

    }
}
