using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    //public float launchSpeed;
    //public Vector3 launchVelocity;

    private Rigidbody rb;
    private AudioSource audioSource;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        rb.useGravity = false;
        //Launch(launchVelocity);
    }

    public void Launch(Vector3 force)
    {
        rb.useGravity = true;
        rb.AddForce(force, ForceMode.Impulse);

        audioSource.Play();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
