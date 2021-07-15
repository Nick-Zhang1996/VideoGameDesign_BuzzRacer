using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevatorTrigger : MonoBehaviour
{
    private Animator elevatorAnim;
    // Start is called before the first frame update
    void Start()
    {
        GameObject elevator = GameObject.Find("elevatorPlatform");
        elevatorAnim = elevator.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("We are here. The animator should start.");
        elevatorAnim.SetBool("playerNear", true);
    }
    private void OnTriggerExit(Collider other)
    {
        elevatorAnim.SetBool("playerNear", false);
    }
}