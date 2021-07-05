using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogicHandler : MonoBehaviour
{

    public GameObject gameLogicObject;
    private GameLogicHandler gameLogic;
    // determines grade in the end
    public int note_count = 0;
    // for speed boost
    public int buzz_count = 0;
    // for invincibility against pursur and pedestrians
    public int t_count = 0;

    // full health 100, 0 health explosion
    public int damagePerCollision = 20;
    public int health = 100;

    private AudioPlayer horn;

    // Start is called before the first frame update
    void Start()
    {
        horn = this.GetComponent<AudioPlayer>();
        gameLogic = gameLogicObject.GetComponent<GameLogicHandler>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Note"))
        {
            Debug.Log("Hit Note");
            other.gameObject.SetActive(false);
            note_count += 1;
        }

        if (other.gameObject.CompareTag("Buzz"))
        {
            Debug.Log("Hit Buzz");
            other.gameObject.SetActive(false);
            buzz_count += 1;
        }

        if (other.gameObject.CompareTag("T"))
        {
            Debug.Log("Hit T");
            other.gameObject.SetActive(false);
            horn.m_ToggleChange = true;
            t_count += 1;
        }

        if (other.gameObject.CompareTag("CheckPoint"))
        {
            gameLogic.PlayerReachedCheckpoint();
            Debug.Log("game end");
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        health -= damagePerCollision;
        Debug.Log("Player collision");
    }

}
