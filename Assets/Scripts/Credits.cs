using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Credits
public class Credits : MonoBehaviour
{
    // Update
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CredBackGame();
        }
    }

    // Return to the main scene
    public void CredBackGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 3);
    }

}
