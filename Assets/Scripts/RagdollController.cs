using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    public Collider MainCollider;
    public Collider[] AllColliders;
    public Rigidbody[] AllRigigidbodies;
    private bool currentRagdollEnabled;
    public bool forceRagdoll = false;
    public float impulseForce;
    

    public Rigidbody pelvis;

    private void Awake()
    {
        MainCollider = GetComponent<Collider>();
        AllColliders = GetComponentsInChildren<Collider>(true);
        AllRigigidbodies = GetComponentsInChildren<Rigidbody>(true);
        DoRagdoll(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (forceRagdoll)
        {
            DoRagdoll(true);
            //pelvis.AddForce(new Vector3(0f, impulseForce, 0f), ForceMode.Impulse);
            forceRagdoll = false;
            Debug.Log("Forced ragdoll mode");
        }
    }

    public void DoRagdoll(bool isRagdoll)
    {
        foreach (var col in AllColliders)
            col.enabled = isRagdoll;
        MainCollider.enabled = !isRagdoll;
        foreach (var rb in AllRigigidbodies)
            rb.useGravity = isRagdoll;
        GetComponent<Animator>().enabled = !isRagdoll;
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;
        Debug.Log("enable rugdoll");
        DoRagdoll(true);
        pelvis.AddForce(new Vector3(0f, impulseForce, 0f), ForceMode.Impulse);
    }
}
