using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class HudHandler : MonoBehaviour
{
    public Text textTCount;
    public Text textNotesCount;
    public Text texBuzzCount;
    public Text textHealth;

    public GameObject ramblingWreck;
    private PlayerLogicHandler player;

    // Start is called before the first frame update
    void Start()
    {
        player = ramblingWreck.GetComponent<PlayerLogicHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        textTCount.text = "T count: " + player.t_count.ToString();
        texBuzzCount.text = "Buzz count: " + player.buzz_count.ToString();
        textNotesCount.text = "Notes count: " + player.note_count.ToString();
        textHealth.text = "Health: " + player.health.ToString();
    }
}
