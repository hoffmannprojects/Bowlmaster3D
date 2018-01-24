using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        CountStanding();
    }

    int CountStanding()
    {
        int standingPinCount = 0;

        // Find and loop through all pins.
        foreach (Pin currentPin in GameObject.FindObjectsOfType<Pin>())
        {
            if (currentPin.IsStanding())
            {
                standingPinCount++;
            }

        }
        // Update pin count UI text.
        Text pinCounterText = GameObject.Find("PinCounter").GetComponent<Text>();
        pinCounterText.text = standingPinCount.ToString();

        // return count of standing pins
        return standingPinCount;
    }
}
