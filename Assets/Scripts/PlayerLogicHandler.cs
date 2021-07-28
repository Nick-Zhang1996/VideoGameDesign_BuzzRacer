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
    private Rigidbody rb;

    // shield
    public GameObject visualShield;
    public bool shieldIsActive;
    private float shieldActiveTime;
    public float shieldDuration = 10f;
    private bool pendingReset = false;

    // health and damage
    // full health 100, 0 health explosion
    private int damagePerCollision = 10;
    private int damagePerOpponentCollision = 20;
    private int maxHealth = 100;
    public int health;

    // determines grade in the end
    public int note_count = 0;
    // for health repair
    public int buzz_count = 0;
    // for invincibility against pursur and pedestrians
    public int t_count = 0;

    


    // Start is called before the first frame update
    void Awake()
    {
        health = maxHealth;
        horn = GetComponent<AudioPlayer>();
        explosion = GetComponent<ParticleSystem>();
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = rb.centerOfMass + Vector3.down*0.3f;
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

        // death
        if (health < 0 && !pendingReset)
        {
            explosion.Play();
            text.text = "Oh no, you died ! \n be careful not to hit things next time";
            continuedText.text = "Sending you back";
            generalTextObject.SetActive(true);
            gameLogic.LoadSceneWithDelay(3.0f, SceneManager.GetActiveScene().name);
            pendingReset = true;
            Debug.Log("resetting scene in 3 sec");
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

        if (other.gameObject.CompareTag("OpponentTrigger"))
        {
            tutorialHandler.TriggerOpponentPrepTutorial();
            Debug.Log("opponent prep");
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

        if (collision.gameObject.CompareTag("Pursuiter"))
        {
            Debug.Log("Player damege: opponent collision");
            health -= damagePerOpponentCollision;
            if (!tutorialHandler.opponentTutorialHasTriggered)
            {
                tutorialHandler.TriggerOpponentTutorial();
            }
        }
        else if (collision.gameObject.CompareTag("Hittable"))
        {
            health -= damagePerCollision;
            Debug.Log("Player damege: collision");
            Debug.Log(collision.gameObject.name);
        }
        
    }

}
