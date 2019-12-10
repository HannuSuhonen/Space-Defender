using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] float timeForGameOverScene = 1f;

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGameLevel()
    {
        SceneManager.LoadScene(1);
    }
    
    public void LoadGameOver()
    {
        StartCoroutine(DelayGameOverScene());
    }

    IEnumerator DelayGameOverScene()
    {
        yield return new WaitForSeconds(timeForGameOverScene);
        SceneManager.LoadScene(2);
    }

    public void Quitgame()
    {
        Application.Quit();
    }

    public void ResetScore()
    {
        FindObjectOfType<GameSession>().ResetScore();
    }

    private void Update()
    {
        
        QuickReset();
    }

    //Quick reset for debugging, press spacebar to quickly get back to the game.
    private void QuickReset()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if(sceneName == "Space_Defender_GameOver" && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(1);
            FindObjectOfType<GameSession>().ResetScore();
        }
    }
}
