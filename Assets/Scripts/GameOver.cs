using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Game Over
public class GameOver : MonoBehaviour 
{
    // Command Update When Press Esc Key
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OverBackGame();
        }
    }

    // Command Load Gameplay Scene
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    // Command Load Menu Scene
    public void OverBackGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }
}
