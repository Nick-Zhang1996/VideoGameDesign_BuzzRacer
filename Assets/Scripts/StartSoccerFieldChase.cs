using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSoccerFieldChase : MonoBehaviour
{
    GameObject playerObject;
    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("We here");
        if (other.gameObject.name.Equals("Player")) {
            GameObject[] pursuers = GameObject.FindGameObjectsWithTag("Pursuiter");
            Debug.Log("Length of pursuers is | " + pursuers.Length);
            foreach (GameObject x in pursuers) {
                PurePursuitAi script = x.GetComponent<PurePursuitAi>();
                script.startChase = true;
            }
        }
    }
}
