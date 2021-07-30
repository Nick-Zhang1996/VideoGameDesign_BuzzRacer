using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTutorialAI : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) {
            GameObject enemy = GameObject.Find("TutorialOpponent");
            PurePursuitAi script = enemy.GetComponent<PurePursuitAi>();
            script.startChase = true;
        }
    }
}
