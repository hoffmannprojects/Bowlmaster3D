﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Needed for accessing Text type. 

public class PinCounter : MonoBehaviour {

    private bool ballLeftLaneBox = false;
    private bool scoreIsUpdated = false;
    private int lastStandingCount;
    private int pinsToBowl = 10;

    private GameManager gameManager;
    private Text pinCounterDisplay;
    private Ball ball;
    private PinSetter pinSetter;

    // Needs to stay here (to be persistent).
    private ActionMaster actionMaster = new ActionMaster();

    // Use this for initialization
    void Start ()
    {
        ball = GameObject.FindObjectOfType<Ball>();
        gameManager = GameObject.FindObjectOfType<GameManager>();
        pinCounterDisplay = GameObject.Find("PinCounter").GetComponent<Text>();
        pinSetter = GameObject.FindObjectOfType<PinSetter>();

        pinCounterDisplay.text = "0";
    }

    // Update is called once per frame
    void Update ()
    {
        if (ballLeftLaneBox)
        {
            scoreIsUpdated = false;
            StartCoroutine(UpdatePinStatus());
        }
    }

    private void OnTriggerExit (Collider other)
    {
        if (other.gameObject.GetComponent<Ball>())
        {
            ballLeftLaneBox = true;
        }
    }

    IEnumerator UpdatePinStatus ()
    {
        lastStandingCount = CountStandingPins();

        pinCounterDisplay.color = Color.red;
        pinCounterDisplay.text = "scoring";

        yield return new WaitForSecondsRealtime(3);

        // Check for any difference after 3 seconds.
        if (lastStandingCount == CountStandingPins())
        {
            if (!scoreIsUpdated)
            {
                PinsHaveSettled();
            }
        }
    }

    int CountStandingPins ()
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
        // Return count of standing pins.
        return standingPinCount;
    }

    void PinsHaveSettled ()
    {
        ballLeftLaneBox = false;
        ball.Reset();
        UpdateScore();
        Debug.Log("Pins to Bowl: " + pinsToBowl);
    }

    void UpdateScore ()
    {

        int fallenPins = pinsToBowl - lastStandingCount;

        pinsToBowl = lastStandingCount;

        // Let actionMaster decide what action to do.
        ActionMaster.Action action = actionMaster.Bowl(fallenPins);

        if (action == ActionMaster.Action.Tidy)
        {
            pinSetter.Tidy();
        }
        else if (action == ActionMaster.Action.Reset)
        {
            pinSetter.Reset();
            pinsToBowl = 10;
        }
        else if (action == ActionMaster.Action.EndTurn)
        {
            print("EndTurn triggering Reset");
            pinSetter.Reset();
            pinsToBowl = 10;
        }
        else if (action == ActionMaster.Action.EndGame)
        {
            throw new UnityException("EndGame handling not defined!");
        }

        // Update display.
        pinCounterDisplay.text = fallenPins.ToString();
        pinCounterDisplay.color = Color.green;
        scoreIsUpdated = true;
    }
}
