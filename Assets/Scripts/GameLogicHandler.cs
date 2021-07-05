using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameLogicHandler : MonoBehaviour
{
    public GameObject gameEndUi;
    public GameObject hudUi;

    public GameObject mainTutorialUi;
    public GameObject notesTutorialUi;
    public GameObject buzzTutorialUi;
    public GameObject tTutorialUi;
    public GameObject opponentTutorialUi;
    public GameObject checkPointTutorialUi;

    private GameObject currentTutorialUi;
    public bool mainTutorialHasTriggered = false;
    public bool notesTutorialHasTriggered = false;
    public bool buzzTutorialHasTriggered = false;
    public bool tTutorialHasTriggered = false;
    public bool opponentTutorialHasTriggered = false;
    public bool checkpointTutorialHasTriggered = false;
    public void TriggerMainTutorial()
    {
        Time.timeScale = 0f;
        mainTutorialUi.SetActive(true);
        currentTutorialUi = mainTutorialUi;
        Debug.Log("main tutorial");
        mainTutorialHasTriggered = true;
    }

    public void TriggerNotesTutorial()
    {
        Time.timeScale = 0f;
        notesTutorialUi.SetActive(true);
        currentTutorialUi = notesTutorialUi;
        Debug.Log("notes tutorial");
        notesTutorialHasTriggered = true;
    }

    public void TriggerBuzzTutorial()
    {
        Time.timeScale = 0f;
        buzzTutorialUi.SetActive(true);
        currentTutorialUi = buzzTutorialUi;
        Debug.Log("buzz tutorial");
        buzzTutorialHasTriggered = true;
    }

    public void TriggerTTutorial()
    {
        Time.timeScale = 0f;
        tTutorialUi.SetActive(true);
        currentTutorialUi = tTutorialUi;
        Debug.Log("T tutorial");
        tTutorialHasTriggered = true;
    }

    public void TriggerOpponentTutorial()
    {
        Time.timeScale = 0f;
        opponentTutorialUi.SetActive(true);
        currentTutorialUi = opponentTutorialUi;
        Debug.Log("opponent tutorial");
        opponentTutorialHasTriggered = true;
    }

    public void TriggerCheckpointTutorial()
    {
        Time.timeScale = 0f;
        checkPointTutorialUi.SetActive(true);
        currentTutorialUi = checkPointTutorialUi;
        Debug.Log("check point tutorial");
        checkpointTutorialHasTriggered = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        hudUi.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // handbrake (space bar) is used to unpause tutorials
    void OnHandbrake(InputValue movementValue)
    {
        float val = movementValue.Get<float>();
        bool released = val < 0.5f;
        if (released)
        {
            currentTutorialUi.SetActive(false);
            Time.timeScale = 1f;
            Debug.Log("resume game");
        }
    }

    // player needs to call this
    public void PlayerReachedCheckpoint()
    {
        gameEndUi.SetActive(true);
        hudUi.SetActive(false);
    }
}
