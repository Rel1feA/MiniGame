using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RECode.REFramework;

public class GameManager : MonoSingleton<GameManager>
{
    public int currentLevel;
    public float score;
    public float remainingTime;
    [SerializeField]
    private GameData gameData;

    private Dictionary<int, LevelData> levelDataDic=new Dictionary<int, LevelData>();

    private void Start()
    {
        CacheLevelData();
        UIManager.Instance.ShowPanel<StartGamePanel>("StartGamePanel",E_UI_Canvas.Static);
    }

    private void Update()
    {
        remainingTime= Time.deltaTime;
        if(remainingTime < 0)
        {
            GameOver();
        }
    }

    public void StartGame()
    {
        LoadSceneManager.Instance.LoadSceneAsync(1, null, (value) =>
        {
            UIManager.Instance.HidePanel("StartGamePanel");
        });
    }

    public void GameOver()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ResetTimer()
    {
        remainingTime = gameData.levelTime;
    }

    private void CacheLevelData()
    {
        foreach(var levelData in gameData.levelDatas)
        {
            levelDataDic[levelData.levelIndex] = levelData;
        }
    }
}
