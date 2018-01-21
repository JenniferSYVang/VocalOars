using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeNote : MonoBehaviour
{
    [SerializeField]
    private Record recordScript;

    [SerializeField]
    private Text noteDisplay;

    // Use this for initialization
    void Start()
    {
        noteDisplay.text = "C4 - 262 Hz";
    }

    // Update is called once per frame
    void Update()
    {
        if(recordScript.correct == true && recordScript.count == 0)
        {
            noteDisplay.text = "C4 - 262 Hz (1)";
        }
        else if (recordScript.correct == true && recordScript.count == 1)
        {
            noteDisplay.text = "C4 - 262 Hz (2)";
        }
        else if (recordScript.correct == true && recordScript.count == 2)
        {
            noteDisplay.text = "C4 - 262 Hz (3)";
        }
    }
}
