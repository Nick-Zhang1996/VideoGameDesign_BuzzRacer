using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAi : MonoBehaviour
{
    public GameObject[] ai;
    private PurePursuitAi[] aiPurePursuit;
    // Start is called before the first frame update
    void Start()
    {
        aiPurePursuit = new PurePursuitAi[ai.Length];
        for (int i=0; i<ai.Length; i++)
        {
            aiPurePursuit[i] = ai[i].GetComponent<PurePursuitAi>();
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
                aiPurePursuit[i].startChase = true;

            }
        }
        Debug.Log("ai enabled");
    }
}
