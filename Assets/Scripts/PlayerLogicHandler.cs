using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogicHandler : MonoBehaviour
{

    public GameObject gameLogicObject;
    public GameObject visualShield;
    public float shieldDuration = 10f;

    private GameLogicHandler gameLogic;
    public bool shieldIsActive;
    private float shieldActiveTime;
    

    // determines grade in the end
    public int note_count = 0;
    // for speed boost
    public int buzz_count = 0;
    // for invincibility against pursur and pedestrians
    public int t_count = 0;

    // full health 100, 0 health explosion
    public int damagePerCollision = 20;
    public int maxHealth = 100;
    public int health;

    private AudioPlayer horn;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        horn = this.GetComponent<AudioPlayer>();
        gameLogic = gameLogicObject.GetComponent<GameLogicHandler>();
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
    }

    void OnTriggerEnter(Collider other)
    {
        // notes are only used at the end to calculate GPA (level score)
        if (other.gameObject.CompareTag("Note"))
        {
            Debug.Log("Collect Note");
            other.gameObject.SetActive(false);
            note_count += 1;
        }

        // buzz fix the car
        if (other.gameObject.CompareTag("Buzz"))
        {
            Debug.Log("Collect Buzz, health return to max");
            other.gameObject.SetActive(false);
            health = maxHealth;
            buzz_count += 1;
        }

        // T gives invulnerability for 10 sec
        if (other.gameObject.CompareTag("T"))
        {
            Debug.Log("Collect T, invincibility");
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
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (! shieldIsActive)
        {
            health -= damagePerCollision;
            Debug.Log("Player damege: collision");
        }
        else
        {
            Debug.Log("Player collision: no damage");
        }
        
    }

}
