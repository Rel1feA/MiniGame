using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RECode.REFramework;

public class GameManager : MonoSingleton<GameManager>
{
    public int currentLevel;
    public float score;
    public float remainingTime;
    public int monsterPercent;

    private float targetScore;

    public void CalculateValue()
    {

    }
}
