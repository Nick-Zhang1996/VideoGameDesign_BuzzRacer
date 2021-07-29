using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walkingScript : MonoBehaviour
{
    private Animator profAnim;
    public float velocity;
    // Start is called before the first frame update
    void Start()
    {
        GameObject prof = GameObject.Find("professor");
        profAnim = prof.GetComponent<Animator>();
        profAnim.SetBool("startWalking", true);
        velocity = 0f;
        Debug.Log("Is this null? | "+ velocity);
    }

    private void Update()
    {
        //profAnim.SetFloat("vely", velocity);
        //Debug.Log("Velocity reported" + profAnim.GetFloat("vely"));
    }
}
