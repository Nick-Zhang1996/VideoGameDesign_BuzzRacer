using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("DemoLevel");
    }

    public void SelectLevel()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
}
