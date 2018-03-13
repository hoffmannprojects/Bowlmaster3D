using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneBox : MonoBehaviour {

    private GameManager gameManager;

	// Use this for initialization
	void Start ()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
	}

    private void OnTriggerExit (Collider other)
    {
        if (other.gameObject.GetComponent<Ball>())
        {
            gameManager.SetBallLeftLaneBox();
        }
    }
}
