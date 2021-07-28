using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameLogicHandler : MonoBehaviour
{
    public GameObject gameEndUi;
    public GameObject hudUi;
    public GameObject pauseUi;
    public GameObject player;
    public Text gameEndUiText;

    [HideInInspector] public bool pendingReset = false;
    [HideInInspector] public float resetTimestamp;
    [HideInInspector] public string resetScene;

    public int totalNotes;
    public float minGpaToPass;
    private PlayerLogicHandler playerLogic;
    // level count, for unlocking next level
    public int level_id;

    // Start is called before the first frame update
    void Awake()
    {
        Time.timeScale = 1.0f;
        playerLogic = player.GetComponent<PlayerLogicHandler>();
        hudUi.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (pauseUi.activeSelf)
            {
                pauseUi.SetActive(false);
                Time.timeScale = 1f;
            }
            else
            {
                pauseUi.SetActive(true);
                Time.timeScale = 0f;
            }
        }

        if (pendingReset)
        {
            if (Time.realtimeSinceStartup > resetTimestamp)
            {
                SceneManager.LoadScene(resetScene);
            }
        }
    }


    // player needs to call this
    public void PlayerReachedCheckpoint()
    {
        
        hudUi.SetActive(false);
        // calculate final score
        float gpa = GetCurrentGpa();

        if (gpa > minGpaToPass)
        {
            gameEndUiText.text = "Level Complete! \n Your GPA is " + gpa.ToString("F2") + ", You passed \n New Level Unlocked!";
            PlayerPrefs.SetInt("levelReached", level_id+1);
            PlayerPrefs.Save();
            Debug.Log("New level Unlocked" + (level_id + 1).ToString());
            Time.timeScale = 0.1f;

        }
        else
        {
            gameEndUiText.text = "Level Failed! \n Your GPA is " + gpa.ToString("F2") + ", You failed";
            Time.timeScale = 0.1f;
        }
        LoadSceneWithDelay(3.0f, "LevelSelect");

        gameEndUi.SetActive(true);
    }

    public float GetCurrentGpa()
    {
        return (float)playerLogic.note_count / (float)totalNotes * 4f;
    }

    public void LoadSceneWithDelay(float delay, string scene)
    {
        pendingReset = true;
        resetTimestamp = Time.realtimeSinceStartup + delay;
        resetScene = scene;
    }
}
