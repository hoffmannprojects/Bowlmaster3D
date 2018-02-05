using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour {
    public float standingThreshold = 3f;
    public float distanceToRaise = 40f;

    private Rigidbody myRigidbody;

    // Use this for initialization
    void Start () {
        myRigidbody = GetComponent<Rigidbody>();
    }
	
    public bool IsStanding () {
        float tiltInX = Mathf.Abs (Mathf.DeltaAngle (transform.eulerAngles.x, 0)); // Absolute (positive) delta between transform rotation in degrees and 0.
        float tiltInZ = Mathf.Abs (Mathf.DeltaAngle (transform.eulerAngles.z, 0));

        if (tiltInX >= standingThreshold  || tiltInZ >= standingThreshold) {
            return false;
        }
        else {
            return true; 
        }
    }

    public void RaiseIfStanding ()
    {
        if (IsStanding())
        {
            myRigidbody.useGravity = false;
            myRigidbody.isKinematic = true;
            myRigidbody.MovePosition(transform.position + Vector3.up * distanceToRaise);
        }
    }

    public void Lower ()
    {
        myRigidbody.MovePosition(transform.position + Vector3.down * distanceToRaise);
        myRigidbody.useGravity = true;
        myRigidbody.isKinematic = false;
    }
}
