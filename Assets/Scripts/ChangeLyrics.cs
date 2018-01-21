using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeLyrics : MonoBehaviour
{
    [SerializeField]
    private Text lyricDisplay;

    public void ChangeTheLyrics(int count)
    {
        if (count == 0 || count == 1 || count == 2 || count == 3 || count == 4)
        {
            lyricDisplay.text = "Row, row, row your boat";
        }
        else if (count == 5 || count == 6 || count == 7 || count == 8 || count == 9)
        {
            lyricDisplay.text = "gent-ly down the stream.";
        }
        else if (count == 10 || count == 11 || count == 12 || count == 13)
        {
            lyricDisplay.text = "Merri-ly, merrily, merrily, merrily,";
        }
        else if (count == 14 || count == 15 || count == 16 || count == 17 || count == 18)
        {
            lyricDisplay.text = "life is but a dream.";
        }
    }
}
