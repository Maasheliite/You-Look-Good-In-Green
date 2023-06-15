using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenuUI;
    public GameObject skillTree;
    public GameObject usualCanvas;
    private bool isSkillUp;

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

        if (Input.GetKeyDown(KeyCode.X))
        {
            if (!isSkillUp)
            {
                SkillTree();
                isSkillUp = true;
            }
            else
            {
                EndSkill();
                isSkillUp = false;
            }
        }
        


    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        usualCanvas.SetActive(true);
        GameStateManager.gameState = GameStateManager.GameState.Running;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        usualCanvas.SetActive(false);
        GameStateManager.gameState = GameStateManager.GameState.Paused;
    }


    public void SkillTree()
    {
        skillTree.SetActive(true);
        usualCanvas.SetActive(false);
        GameStateManager.gameState = GameStateManager.GameState.Paused;

    }

    public void EndSkill()
    {
        skillTree.SetActive(false);
        usualCanvas.SetActive(true);
        GameStateManager.gameState = GameStateManager.GameState.Running;

    }
}
