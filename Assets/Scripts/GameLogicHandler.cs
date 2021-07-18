using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameLogicHandler : MonoBehaviour
{
    public GameObject gameEndUi;
    public GameObject hudUi;
    public GameObject pauseUi;
    public GameObject player;
    public Text gameEndUiText;

    public int totalNotes;
    public float minGpaToPass;
    private PlayerLogicHandler playerLogic;
    
    // Start is called before the first frame update
    void Start()
    {
 
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
    }


    // player needs to call this
    public void PlayerReachedCheckpoint()
    {
        
        hudUi.SetActive(false);
        // calculate final score
        float gpa = GetCurrentGpa();

        if (gpa > minGpaToPass)
        {
            gameEndUiText.text = "Level Complete! \n Your GPA is " + gpa.ToString("F2") + ", You passed";

        }
        else
        {
            gameEndUiText.text = "Level Failed! \n Your GPA is " + gpa.ToString("F2") + ", You failed";
        }
        

        gameEndUi.SetActive(true);
    }

    public float GetCurrentGpa()
    {
        return (float)playerLogic.note_count / (float)totalNotes * 4f;
    }
}
