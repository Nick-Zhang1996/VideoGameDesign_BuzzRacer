using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpeedDisplayHandler : MonoBehaviour
{
    private TMP_Text speedText;
    private GameObject playerObject;
    private PlayerLogicHandler player;
    private Rigidbody playerRb;
    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("Player");
        player = playerObject.GetComponent<PlayerLogicHandler>();
        playerRb = playerObject.GetComponent<Rigidbody>();
        speedText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 localVelocity = player.transform.InverseTransformVector(playerRb.velocity);
        float speedMs = localVelocity.z;
        float speedMph = speedMs * 2.2369362912f;
        //speedText.text = "Speed: " + speedMs.ToString("F2") + " m/s";
        speedText.text = "Speed: " + speedMph.ToString("F2") + " MPH";
        //2.2369362912 m/s to mph
    }
}
