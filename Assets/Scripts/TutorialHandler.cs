using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TutorialHandler: MonoBehaviour
{

    public GameObject mainTutorialUi;
    public GameObject notesTutorialUi;
    public GameObject buzzTutorialUi;
    public GameObject tTutorialUi;
    public GameObject opponentTutorialUi;
    public GameObject checkPointTutorialUi;
    public GameObject opponentPrepTutorialUi;

    private GameObject currentTutorialUi;
    public bool enableTutorials;
    [HideInInspector] public bool mainTutorialHasTriggered = false;
    [HideInInspector] public bool notesTutorialHasTriggered = false;
    [HideInInspector] public bool buzzTutorialHasTriggered = false;
    [HideInInspector] public bool tTutorialHasTriggered = false;
    [HideInInspector] public bool opponentTutorialHasTriggered = false;
    [HideInInspector] public bool checkpointTutorialHasTriggered = false;
    [HideInInspector] public bool opponentPrepTutorialHasTriggered = false;

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

    public void TriggerOpponentPrepTutorial()
    {
        Time.timeScale = 0f;
        opponentPrepTutorialUi.SetActive(true);
        currentTutorialUi = opponentPrepTutorialUi;
        Debug.Log("opponent prep tutorial");
        opponentPrepTutorialHasTriggered = true;
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

        if (!enableTutorials)
        {
            mainTutorialHasTriggered = true;
            notesTutorialHasTriggered = true;
            buzzTutorialHasTriggered = true;
            tTutorialHasTriggered = true;
            opponentTutorialHasTriggered = true;
            checkpointTutorialHasTriggered = true;
        }
        

        if (!mainTutorialHasTriggered)
        {
            TriggerMainTutorial();
        }
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
            if (currentTutorialUi != null)
            {
                currentTutorialUi.SetActive(false);
            }

            Time.timeScale = 1f;
            Debug.Log("resume game");
        }
    }

    public void PlayerReachedPreCheckpoint()
    {
        if (!checkpointTutorialHasTriggered)
        {
            TriggerCheckpointTutorial();
        }
    }
    // player needs to call this
}
