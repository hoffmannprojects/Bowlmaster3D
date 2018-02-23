using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour {
    public float standingThreshold = 3f;

    private Rigidbody myRigidbody;

    // Use this for initialization
    void Start () {
        myRigidbody = GetComponent<Rigidbody>();
    }
	
    public bool IsStanding () {
        // Get absolute (positive) delta between transform rotation in degrees and 0.
        float tiltInX = Mathf.Abs (Mathf.DeltaAngle (transform.eulerAngles.x, 0)); 
        float tiltInZ = Mathf.Abs (Mathf.DeltaAngle (transform.eulerAngles.z, 0));

        if (tiltInX >= standingThreshold  || tiltInZ >= standingThreshold) {
            return false;
        }
        else {
            return true; 
        }
    }

    public void RaiseIfStanding (float distance)
    {
        if (IsStanding())
        {
            myRigidbody.useGravity = false;
            myRigidbody.MovePosition(transform.position + Vector3.up * distance);
            myRigidbody.rotation = Quaternion.identity;
        }
    }

    public void Lower (float distance)
    {
        myRigidbody.useGravity = true;
    }
}
