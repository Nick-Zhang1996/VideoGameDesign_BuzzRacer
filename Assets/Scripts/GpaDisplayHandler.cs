using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GpaDisplayHandler : MonoBehaviour
{
    private TMP_Text gpaText;
    [SerializeField] private GameLogicHandler game;
    // Start is called before the first frame update
    void Start()
    {
        gpaText = GetComponent<TMP_Text>();   
    }

    // Update is called once per frame
    void Update()
    {
        float gpa = game.GetCurrentGpa();
        gpaText.text = "GPA: " + gpa.ToString("F2");
        //gpaText.color = new Color(1f, 1f, 1f);
    }
}
