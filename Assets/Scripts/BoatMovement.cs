using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoatMovement : MonoBehaviour
{
    public float zPos;
    public bool stop;
    private float unitsToMove;
    private bool started;
    private float timer;

    [SerializeField]
    private Record recordScript;

    [SerializeField]
    private Text middleText;


    // Use this for initialization
    void Start()
    {
        unitsToMove = 0;
        stop = true;
        timer = 5;
        started = true;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        zPos = this.transform.position.z;

        if (zPos < 88.8 && timer <= 0)
        {
            if (stop)
            {
                unitsToMove = 0;
                if (started)
                {
                    started = false;
                    stop = false;
                }
            }
            else if (recordScript.correct && unitsToMove != 0)
            {
                unitsToMove = -.1f;
            }
            else
            {
                unitsToMove = .15f;
            }
            zPos += unitsToMove;
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, zPos);
        }
    }

    public void StopMovement()
    {
        stop = true;
    }
}
