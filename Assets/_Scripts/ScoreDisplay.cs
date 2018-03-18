using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

    public Text[] rollTexts = new Text[21];
    public Text[] frameTexts = new Text[10];

	// Use this for initialization
	void Start () {
		foreach (Text text in rollTexts)
        {
            text.text = "";
        }

        foreach (Text text in frameTexts)
        {
            text.text = "";
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
