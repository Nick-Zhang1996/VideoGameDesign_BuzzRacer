using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogicHandler : MonoBehaviour
{
    public GameObject gameEndUi;
    public GameObject hudUi;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // player needs to call this
    public void PlayerReachedCheckpoint()
    {
        gameEndUi.SetActive(true);
        hudUi.SetActive(false);
    }
}
