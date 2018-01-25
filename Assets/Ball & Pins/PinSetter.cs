using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour
{
    private bool ballEnteredBox = false;
    private Text pinCounterDisplay;

    // Use this for initialization
    void Start()
    {
        pinCounterDisplay = GameObject.Find("PinCounter").GetComponent<Text>();
    }


    // Update is called once per frame
    void Update()
    {
        CountStanding();
    }


    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.gameObject.GetComponent<Ball>())
        {
            ballEnteredBox = true;
            print("Ball detected in PinSetter");

            pinCounterDisplay.color = Color.red;
        }
    }


    private void OnTriggerExit(Collider otherCollider)
    {
        // Check for presence of Pin script on parent, because of Pin setup in hierarchy. 
        if (otherCollider.GetComponentInParent<Pin>() != null)
        {
            print("Pin left Pinsetter");
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
