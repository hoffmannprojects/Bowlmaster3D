using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    private bool inPlay = false;
    private Rigidbody rb;
    private AudioSource audioSource;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        rb.useGravity = false;
    }

    public void Launch(Vector3 force)
    {
        rb.useGravity = true;
        rb.AddForce(force, ForceMode.Impulse);

        audioSource.Play();

        inPlay = true;
    }

    void MoveStart(float xNudgeAmount) {
        if (! inPlay) {
            transform.Translate (new Vector3 (xNudgeAmount, 0, 0) ); // Nudge position by xNudgeAmount x-wise.
            Debug.Log("startPosition: " + transform.position.x);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
