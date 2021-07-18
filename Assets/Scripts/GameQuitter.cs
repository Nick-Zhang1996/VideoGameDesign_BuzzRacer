using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameQuitter : MonoBehaviour
{
    public void QuitGame()
    {
        SceneManager.LoadScene("LevelSelect");
    }
}
