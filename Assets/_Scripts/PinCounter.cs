using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Needed for accessing Text type. 

public class PinCounter : MonoBehaviour {

    private bool ballLeftLaneBox = false;
    private bool bowlIsLocked = false;
    private int pinsLeftStanding;
    private int pinsToBowl;

    private GameManager gameManager;
    private Text pinCounterDisplay;

    public int pinsHaveSettledThresholdSeconds = 3;

    // Use this for initialization
    void Start ()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        pinCounterDisplay = GameObject.Find("PinCounter").GetComponent<Text>();

        Reset();
        ResetDisplay();
    }

    // Update is called once per frame
    void Update ()
    {
        if (ballLeftLaneBox)
        {
            bowlIsLocked = false;
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
        pinsLeftStanding = CountStandingPins();

        pinCounterDisplay.text = "scoring";

        yield return new WaitForSecondsRealtime(pinsHaveSettledThresholdSeconds);

        // Check for any difference after pinsHaveSettledThresholdSeconds.
        if ((pinsLeftStanding == CountStandingPins()) && !bowlIsLocked)
        {            
            LockBowl();
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

    void LockBowl ()
    {
        ballLeftLaneBox = false;
        SubmitBowl();
        bowlIsLocked = true;
    }

    void SubmitBowl ()
    {
        // Two lines need to stay together, to make sure pinsTobowl gets reset.
        int pinsBowled = pinsToBowl - pinsLeftStanding;
        pinsToBowl = pinsLeftStanding;

        ResetDisplay();

        gameManager.HandleRoll(pinsBowled);

        print("pinsToBowl: " + pinsToBowl);
    }

    private void ResetDisplay ()
    {
        pinCounterDisplay.text = "";
    }

    public void Reset ()
    {
        pinsToBowl = 10;
    }
}
