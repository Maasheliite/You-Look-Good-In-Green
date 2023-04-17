using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenuUI;

    public GameObject usualCanvas;

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameStateManager.gameState == GameStateManager.GameState.Paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }

        }


        void Resume()
        {
            pauseMenuUI.SetActive(false);
            usualCanvas.SetActive(true);
            GameStateManager.gameState = GameStateManager.GameState.Running;
        }

        void Pause()
        {
            pauseMenuUI.SetActive(true);
            usualCanvas.SetActive(false);
            GameStateManager.gameState = GameStateManager.GameState.Paused;
        }


    }

}
