using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayGame()
    {
        TutorialLevel();
    }

    public void TutorialLevel()
    {
        SceneManager.LoadScene("TutorialLevel");
        Debug.Log("load Tutorial Level");
    }

    public void SelectLevel()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("quitting");
    }
}
