using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    void OnTriggerExit (Collider otherCollider)
    {
        // Check for presence of Pin script on parent, because of Pin setup in hierarchy. 
        if (otherCollider.GetComponentInParent<Pin>())
        {
            Destroy(otherCollider.GetComponentInParent<Pin>().gameObject);
        }
    }
}
