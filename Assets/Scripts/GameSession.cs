using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField] int scorePerKill = 100;
    int score = 0;

    private void Awake()
    {
        SetUpSingleton();
    }

    public void SetUpSingleton()
    {
        int numberOfGameSessions = FindObjectsOfType<GameSession>().Length;
        if(numberOfGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ScoreCounter()
    {
        score += scorePerKill;
    }

    public int GetScore()
    {
        return score;
    }

    public void ResetScore()
    {
        Destroy(gameObject);
    }
}
