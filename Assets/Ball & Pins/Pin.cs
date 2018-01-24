using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour {
    public float standingThreshold = 3f;

	// Use this for initialization
	void Start () {
        // Debug.Log (name + " is standing = " + IsStanding () + " delta tilt in x = " + Mathf.DeltaAngle (transform.eulerAngles.x, 0) + " delta tilt in z = " + Mathf.DeltaAngle (transform.eulerAngles.z, 0));
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
	// Update is called once per frame
	void Update () {

    }
}
