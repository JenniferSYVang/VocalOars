using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayCorrect : MonoBehaviour
{
    [SerializeField]
    private Text correctDisplay;

    [SerializeField]
    private Text noteDisplay;

    [SerializeField]
    private Record recordScript;

    public void ChangeText(int count)
    {
            if (count == 0)
            {
                correctDisplay.text = "Row 2x";
                noteDisplay.text = "C4 - 262 Hz";
            }
            else if (count == 1)
            {
                correctDisplay.text = "Row 1x";
                noteDisplay.text = "C4 - 262 Hz";
            }
            else if (count == 2)
            {
                correctDisplay.text = "Row";
                noteDisplay.text = "C4 - 262 Hz";
            }
            else if (count == 3)
            {
                correctDisplay.text = "your";
                noteDisplay.text = "D4 - 294 Hz";
            }
            else if (count == 4)
            {
                correctDisplay.text = "boat";
                noteDisplay.text = "E4 - 330 Hz";
            }
            else if (count == 5)
            {
                correctDisplay.text = "gent-";
                noteDisplay.text = "E4 - 330 Hz";
            }
            else if (count == 6)
            {
                correctDisplay.text = "-ly";
                noteDisplay.text = "D4 - 294 Hz";
            }
            else if (count == 7)
            {
                correctDisplay.text = "down";
                noteDisplay.text = "E4 - 330 Hz";
            }
            else if (count == 8)
            {
                correctDisplay.text = "the";
                noteDisplay.text = "F4 - 349 Hz";
            }
            else if (count == 9)
            {
                correctDisplay.text = "stream";
                noteDisplay.text = "G4 - 392 Hz";
            }
            else if (count == 10)
            {
                correctDisplay.text = "Merrily 3x";
                noteDisplay.text = "C5 - 525 Hz";
            }
            else if (count == 11)
            {
                correctDisplay.text = "merrily 2x";
                noteDisplay.text = "G4 - 392 Hz";
            }
            else if (count == 12)
            {
                correctDisplay.text = "merrily 1x";
                noteDisplay.text = "E4 - 330 Hz";
            }
            else if (count == 13)
            {
                correctDisplay.text = "merrily";
                noteDisplay.text = "C4 - 262 Hz";
            }
            else if (count == 14)
            {
                correctDisplay.text = "life";
                noteDisplay.text = "G4 - 392 Hz";
            }
            else if (count == 15)
            {
                correctDisplay.text = "is";
                noteDisplay.text = "F4 - 349 Hz";
            }
            else if (count == 16)
            {
                correctDisplay.text = "but";
                noteDisplay.text = "E4 - 330 Hz";
            }
            else if (count == 17)
            {
                correctDisplay.text = "a";
                noteDisplay.text = "D4 - 294 Hz";
            }
            else if (count == 18)
            {
                correctDisplay.text = "dream";
                noteDisplay.text = "C4 - 262 Hz";
            }
        
    }
}
