using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SafetyNetHandler : MonoBehaviour
{
    public GameObject generalTextObject;
    public Text text;
    public Text continuedText;
    private bool pendingReset = false;
    private float resetTimestamp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pendingReset)
        {
            if (Time.realtimeSinceStartup > resetTimestamp)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Time.timeScale = 0f;
        text.text = "Oops, you've discovered the edge of the world. \n This is obviously not supposed to happen so we're sending you back";
        continuedText.text = "Just one moment";
        generalTextObject.SetActive(true);
        resetTimestamp = Time.realtimeSinceStartup + 5f;
        pendingReset = true;
        Debug.Log("resetting scene in 5 sec");


    }
}
