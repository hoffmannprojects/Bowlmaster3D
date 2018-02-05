﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour
{
    public GameObject pins;
    public float distanceToRaise = 40f;

    private int lastStandingCount;
    private bool ballEnteredBox = false;
    private Text pinCounterDisplay;
    private Ball ball;

    // Use this for initialization
    void Start()
    {
        pinCounterDisplay = GameObject.Find("PinCounter").GetComponent<Text>();
        ball = GameObject.FindObjectOfType<Ball>();
    }


    // Update is called once per frame
    void Update()
    {
        if (ballEnteredBox) 
        {
            StartCoroutine(UpdatePinStatus());
        }
    }

    IEnumerator UpdatePinStatus()
    {
        lastStandingCount = CountStandingPins();

        yield return new WaitForSecondsRealtime (3);

        // Check for any difference after 3 seconds.
        if (lastStandingCount == CountStandingPins())
        {
            PinsHaveSettled ();
        }
    }

    int CountStandingPins()
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

        // Update display.
        pinCounterDisplay.text = standingPinCount.ToString();

        // Return count of standing pins.
        return standingPinCount;
    }

    void PinsHaveSettled()
    {
        pinCounterDisplay.color = Color.green;
        ballEnteredBox = false;
        ball.Reset();
    }

    void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.gameObject.GetComponent<Ball>())
        {
            ballEnteredBox = true;
            print("Ball detected in PinSetter");

            pinCounterDisplay.color = Color.red;
        }
    }

    public void RaisePins()
    {
        Debug.Log("Raising Pins.");

        foreach (Pin currentPin in GameObject.FindObjectsOfType<Pin>())
        {
           currentPin.RaiseIfStanding(distanceToRaise);
        }
    }

    public void LowerPins ()
    {
        Debug.Log("Lowering Pins.");

        foreach (Pin currentPin in GameObject.FindObjectsOfType<Pin>())
        {
            currentPin.Lower(distanceToRaise);
        }
    }

    public void RenewPins ()
    {
        Debug.Log("Renewing Pins.");
        Instantiate(pins, new Vector3(0f, distanceToRaise, 1829f), Quaternion.identity);
    }
}
