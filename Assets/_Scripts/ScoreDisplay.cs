using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

    public Text[] rollTexts, frameTexts;

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

    public void FillRollScores (List <int> rolls)
    {
        for (int roll = 1; roll <= 21; roll++)
        {
            rollTexts[roll-2].text = rolls.ToString();
        }
    }
}
