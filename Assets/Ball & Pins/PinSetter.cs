using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour
{
    public GameObject pins;
    public float distanceToRaise = 40f;

    private int lastStandingCount;
    private int pinsToBowl = 10;
    private bool ballLeftLaneBox = false;
    private bool scoreIsUpdated = false;

    private Text pinCounterDisplay;
    private Ball ball;
    private Animator animator;

    // Needs to stay here (to be persistent).
    private ActionMaster actionMaster = new ActionMaster();

    // Use this for initialization
    void Start()
    {
        pinCounterDisplay = GameObject.Find("PinCounter").GetComponent<Text>();
        ball = GameObject.FindObjectOfType<Ball>();
        animator = GetComponent<Animator>();

        pinCounterDisplay.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        if (ballLeftLaneBox) 
        {
            scoreIsUpdated = false;
            StartCoroutine(UpdatePinStatus());
        }
    }

    IEnumerator UpdatePinStatus()
    {
        lastStandingCount = CountStandingPins();

        pinCounterDisplay.color = Color.red;
        pinCounterDisplay.text = "scoring";

        yield return new WaitForSecondsRealtime (3);

        // Check for any difference after 3 seconds.
        if (lastStandingCount == CountStandingPins())
        {
            if (!scoreIsUpdated)
            {
                PinsHaveSettled ();
            }
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
        // Return count of standing pins.
        return standingPinCount;
    }

    void PinsHaveSettled()
    {
        ballLeftLaneBox = false;
        ball.Reset();
        UpdateScore();
        Debug.Log("Pins to Bowl: " + pinsToBowl);
    }

    void UpdateScore()
    {
        
        int fallenPins = pinsToBowl - lastStandingCount;

        pinsToBowl = lastStandingCount;

        // Let actionMaster decide what action to do.
        ActionMaster.Action action = actionMaster.Bowl(fallenPins);

        if (action == ActionMaster.Action.Tidy)
        {
            print("Tidy");
            animator.SetTrigger("tidyTrigger");
        }
        else if (action == ActionMaster.Action.Reset)
        {
            print("Reset");
            animator.SetTrigger("resetTrigger");
            pinsToBowl = 10;
        }
        else if (action == ActionMaster.Action.EndTurn)
        {
            print("EndTurn triggering Reset");
            animator.SetTrigger("resetTrigger");
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

    public void SetBallLeftLaneBox()
    {
        ballLeftLaneBox = true;
    }

    public void RaisePins()
    {
        foreach (Pin currentPin in GameObject.FindObjectsOfType<Pin>())
        {
           currentPin.RaiseIfStanding(distanceToRaise);
        }
    }

    public void LowerPins ()
    {
        foreach (Pin currentPin in GameObject.FindObjectsOfType<Pin>())
        {
            currentPin.Lower(distanceToRaise);
        }
    }

    public void RenewPins ()
    {
        Instantiate(pins, new Vector3(0f, distanceToRaise, 1829f), Quaternion.identity);
    }
}
