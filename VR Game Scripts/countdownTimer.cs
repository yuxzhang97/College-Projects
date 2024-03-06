using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class countdownTimer : MonoBehaviour {

    // Use this for initialization
    public Text text;
    string textToPrint;
    float timeRemaining; //In Seconds
	void Start () {
        textToPrint = "10:00";
        text.text = textToPrint;
        timeRemaining = 10 * 60;
	}
	
	// Update is called once per frame
	void Update () {
        timeRemaining = timeRemaining - Time.deltaTime; //number of seconds left in the game
        float min = timeRemaining / 60;
        int minInt = (int)(Mathf.Floor(min));

        float sec = timeRemaining - (minInt * 60);
        int secInt = (int)sec;
        string zero;
        if (secInt < 10)
        {
            zero = "0";
        }
        else
        {
            zero = "";
        }
        textToPrint = minInt.ToString() + ":" + zero + secInt.ToString();
        text.text = "Time Remaining: "+textToPrint;
	}
}
