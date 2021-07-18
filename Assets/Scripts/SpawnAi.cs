using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAi : MonoBehaviour
{
    public GameObject[] ai;
    private AiCarController[] aiController;
    // Start is called before the first frame update
    void Start()
    {
        aiController = new AiCarController[ai.Length];
        for (int i=0; i<ai.Length; i++)
        {
            aiController[i] = ai[i].GetComponent<AiCarController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            for (int i = 0; i < ai.Length; i++)
            {
                aiController[i].enabled = true;
            }
        }
        Debug.Log("ai enabled");
    }
}
