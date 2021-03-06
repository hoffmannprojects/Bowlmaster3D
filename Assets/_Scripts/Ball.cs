﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private bool inPlay = false;
    private Rigidbody rb;
    private AudioSource audioSource;
    private Vector3 startPosition;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        startPosition = transform.position;

        rb.useGravity = false;
    }

    public void Launch(Vector3 force)
    {
        if (!inPlay)
        {
            rb.useGravity = true;
            rb.AddForce(force, ForceMode.Impulse);

            audioSource.Play();

            inPlay = true;
        }
    }

    void MoveStart(float xNudgeAmount) {
        if (! inPlay) {
            // Nudge position by xNudgeAmount x-wise, but clamp min/max.
            Vector3 newPosition = rb.position + new Vector3 (xNudgeAmount, 0, 0);
            newPosition.x = Mathf.Clamp(newPosition.x , - 50f, 50f);

            rb.position = newPosition;
            Debug.Log("startPosition: " + transform.position.x);
        }
    }

    public void Reset ()
    {
        inPlay = false;
        // Rigidbody.position is faster than transform.position.
        rb.position = startPosition;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.rotation = Quaternion.identity;
        
    }

    // Update is called once per frame
    void Update () {
		
	}
}
