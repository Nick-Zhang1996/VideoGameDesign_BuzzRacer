using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerLogicHandler : MonoBehaviour
{

    public GameLogicHandler gameLogic;
    public TutorialHandler tutorialHandler;
    private AudioPlayer horn;
    public ParticleSystem explosion;
    public GameObject generalTextObject;
    public Text text;
    public Text continuedText;

    // shield
    public GameObject visualShield;
    public bool shieldIsActive;
    private float shieldActiveTime;
    public float shieldDuration = 10f;

    // health and damage
    // full health 100, 0 health explosion
    public int damagePerCollision = 10;
    public int damagePerOpponentCollision = 20;
    public int maxHealth = 100;
    public int health;

    // determines grade in the end
    public int note_count = 0;
    // for health repair
    public int buzz_count = 0;
    // for invincibility against pursur and pedestrians
    public int t_count = 0;


    private bool pendingReset = false;
    private float resetTimestamp;




    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        horn = GetComponent<AudioPlayer>();
        explosion = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shieldIsActive && Time.timeSinceLevelLoad > shieldActiveTime + shieldDuration)
        {
            shieldIsActive = false;
            visualShield.SetActive(false);
            Debug.Log("shield disabled");
        }

        if (health < 0 && !pendingReset)
        {
            explosion.Play();
            text.text = "Oh no, you died ! \n be careful not to hit things next time";
            continuedText.text = "Sending you back";
            generalTextObject.SetActive(true);
            resetTimestamp = Time.realtimeSinceStartup + 3f;
            pendingReset = true;
            Debug.Log("resetting scene in 3 sec");
        }

        if (pendingReset)
        {
            if (Time.realtimeSinceStartup > resetTimestamp)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

    }

    void OnTriggerEnter(Collider other)
    {
        // notes are only used at the end to calculate GPA (level score)
        if (other.gameObject.CompareTag("Note"))
        {
            Debug.Log("Collect Note");
            if (!tutorialHandler.notesTutorialHasTriggered)
            {
                tutorialHandler.TriggerNotesTutorial();
            }
            other.gameObject.SetActive(false);
            note_count += 1;
        }

        // buzz fix the car
        if (other.gameObject.CompareTag("Buzz"))
        {
            Debug.Log("Collect Buzz, health return to max");
            if (!tutorialHandler.buzzTutorialHasTriggered)
            {
                tutorialHandler.TriggerBuzzTutorial();
            }
            other.gameObject.SetActive(false);
            health = maxHealth;
            buzz_count += 1;
        }

        // T gives invulnerability for 10 sec
        if (other.gameObject.CompareTag("T"))
        {
            Debug.Log("Collect T, invincibility");
            if (!tutorialHandler.tTutorialHasTriggered)
            {
                tutorialHandler.TriggerTTutorial();
            }
            other.gameObject.SetActive(false);
            horn.m_ToggleChange = true;
            t_count += 1;
            shieldIsActive = true;
            visualShield.SetActive(true);
            shieldActiveTime = Time.timeSinceLevelLoad;
            Debug.Log("Shield enabled");
        }

        if (other.gameObject.CompareTag("CheckPoint"))
        { 
            gameLogic.PlayerReachedCheckpoint();
            Debug.Log("game end");
        }

        if (other.gameObject.CompareTag("PreCheckpoint"))
        {
            if (!tutorialHandler.checkpointTutorialHasTriggered)
            {
                tutorialHandler.TriggerCheckpointTutorial();
            }
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (shieldIsActive)
        {
            Debug.Log("Player collision, shield active, no damage");
            return;
        }

        if (collision.gameObject.CompareTag("Boundary"))
        {
            // OK to collide into boundary
            return;
        }

        if (collision.gameObject.CompareTag("Pursuiter"))
        {
            Debug.Log("Player damege: opponent collision");
            health -= damagePerOpponentCollision;
            if (!tutorialHandler.opponentTutorialHasTriggered)
            {
                tutorialHandler.TriggerOpponentTutorial();
            }
        }
        else
        {
            health -= damagePerCollision;
            Debug.Log("Player damege: collision");
        }
        
    }

}
