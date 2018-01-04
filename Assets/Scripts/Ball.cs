using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    public float launchSpeed;
    public Vector3 velocity;

    private Rigidbody rb;
    private AudioSource audioSource;
    private Vector3 force;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        Launch();
    }

    public void Launch()
    {
        force.x = velocity.x * launchSpeed;
        force.y = velocity.y * launchSpeed;
        force.z = velocity.z * launchSpeed;

        rb.AddForce(force, ForceMode.Impulse);
        audioSource.Play();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
