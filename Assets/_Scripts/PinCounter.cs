using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Needed for accessing Text type. 

public class PinCounter : MonoBehaviour {

    private bool ballLeftLaneBox = false;
    private bool bowlIsScored = false;
    private int lastStandingCount;
    private int pinsToBowl = 10;

    private GameManager gameManager;
    private Text pinCounterDisplay;
    private Ball ball;

    public int pinsHaveSettledThresholdSeconds = 3;

    // Use this for initialization
    void Start ()
    {
        ball = GameObject.FindObjectOfType<Ball>();
        gameManager = GameObject.FindObjectOfType<GameManager>();
        pinCounterDisplay = GameObject.Find("PinCounter").GetComponent<Text>();

        pinCounterDisplay.text = "0";
    }

    // Update is called once per frame
    void Update ()
    {
        if (ballLeftLaneBox)
        {
            bowlIsScored = false;
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

        yield return new WaitForSecondsRealtime(pinsHaveSettledThresholdSeconds);

        // Check for any difference after pinsHaveSettledThresholdSeconds.
        if (lastStandingCount == CountStandingPins())
        {
            if (!bowlIsScored)
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
        return standingPinCount;
    }

    void PinsHaveSettled ()
    {
        ballLeftLaneBox = false;
        SubmitBowl();
    }

    void SubmitBowl ()
    {
        int fallenPins = pinsToBowl - lastStandingCount;

        gameManager.HandleBowlResult(fallenPins);
        pinsToBowl = lastStandingCount;

        // Update display.
        pinCounterDisplay.text = fallenPins.ToString();
        pinCounterDisplay.color = Color.green;
        bowlIsScored = true;
    }

    public void Reset ()
    {
        pinsToBowl = 10;
    }
}
