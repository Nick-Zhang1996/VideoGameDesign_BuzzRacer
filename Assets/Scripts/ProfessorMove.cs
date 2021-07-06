using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfessorMove : MonoBehaviour
{
    public Animator anim;
    //public Rigidbody rbody;
    float forwardTime = 0;

    private int currentFrameCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("forwardSpeed", 1f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
