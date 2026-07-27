using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RECode.REFramework;

public class GameManager : MonoSingleton<GameManager>
{
    public int currentLevelIndex;
    public float score;
    private float remainingTime;
    public LevelData currentLevelData;
    [SerializeField]
    private GameData gameData;
    public Player player;

    public int RemainTime { get { return (int)remainingTime; } }

    private Dictionary<int, LevelData> levelDataDic=new Dictionary<int, LevelData>();


    private void Start()
    {
        CacheLevelData();
        currentLevelIndex = 1;
        currentLevelData = GetLevelData(1);
        UIManager.Instance.ShowPanel<StartGamePanel>("StartGamePanel",E_UI_Canvas.Static);
        remainingTime = gameData.levelTime;
    }

    private void Update()
    {
        remainingTime-= Time.deltaTime;
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
            UIManager.Instance.ShowPanel<GameUIPanel>("GameUIPanel",E_UI_Canvas.Static);
        });
    }

    private LevelData GetLevelData(int index)
    {
        if(levelDataDic.TryGetValue(index,out LevelData levelData)) return levelData;
        return null;
    }

    public void NextLevel()
    {
        currentLevelIndex++;
        score -= currentLevelData.targetScore;
        currentLevelData = GetLevelData(currentLevelIndex);
        EventCenter.Instance.EventTrigger("NextLevel", currentLevelData);
        remainingTime = gameData.levelTime;
    }

    public void SettleGame()
    {
        if (score < currentLevelData.targetScore) return;
        AbilitySystem.Instance.GenerateStore(3);
    }

    public void PauseGame()
    {

    }

    public void ResumeGame()
    {

    }

    public void GameOver()
    {
        Time.timeScale = 0;
        UIManager.Instance.ShowPanel<GameOverPanel>("GameOverPanel",E_UI_Canvas.Dynamic);
    }

    public void ReStartGame()
    {
        Time.timeScale = 1;
        UIManager.Instance.HidePanel("GameOverPanel");
        remainingTime = gameData.levelTime;
        EventCenter.Instance.EventTrigger("RestartGame");
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
