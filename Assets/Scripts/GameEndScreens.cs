using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndScreens : MonoBehaviour
{
    public void RestartLevel()
    {
        SceneManager.LoadScene("alphaLevel");
    }

    public void Home()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void NextLevel()
    {
        SceneManager.LoadScene("LevelSelect");
    }
}
