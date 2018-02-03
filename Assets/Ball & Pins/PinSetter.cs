using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour
{
    private int lastStandingCount;
    private bool ballEnteredBox = false;
    private Text pinCounterDisplay;
    private Ball ball;

    // Use this for initialization
    void Start()
    {
        pinCounterDisplay = GameObject.Find("PinCounter").GetComponent<Text>();
        lastStandingCount = CountStanding();
        ball = GameObject.FindObjectOfType<Ball>();
    }


    // Update is called once per frame
    void Update()
    {
        if (ballEnteredBox) 
        {
            StartCoroutine(CheckStandingCount());
        }
    }

    IEnumerator CheckStandingCount()
    {
        // Update lastStandingCount
        lastStandingCount = CountStanding();

        // Call PinsHaveSettled() when they have after 3 seconds.
        yield return new WaitForSecondsRealtime (3);
        if (lastStandingCount == CountStanding())
        {
            PinsHaveSettled ();
        }
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


    void OnTriggerExit(Collider otherCollider)
    {
        // Check for presence of Pin script on parent, because of Pin setup in hierarchy. 
        if (otherCollider.GetComponentInParent<Pin>())
        {
            Destroy(otherCollider.GetComponentInParent<Pin>().gameObject);
        }
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
        pinCounterDisplay.text = standingPinCount.ToString();

        // return count of standing pins
        return standingPinCount;
    }
}
