using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinSetter : MonoBehaviour
{
    public GameObject pins;
    public float distanceToRaise = 40f;
    private Animator animator;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
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

    public void Tidy ()
    {
        print("Tidy");
        animator.SetTrigger("tidyTrigger");
    }

    public void Reset ()
    {
        print("Reset");
        animator.SetTrigger("resetTrigger");
    }
}
