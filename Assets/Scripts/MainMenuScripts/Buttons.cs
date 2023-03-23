using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public string nextSceneName;
    public void PlayButton()
    {
        SceneManager.LoadScene(nextSceneName);
    }
    public void QuitButton()
    {
        Application.Quit();
    }
}
