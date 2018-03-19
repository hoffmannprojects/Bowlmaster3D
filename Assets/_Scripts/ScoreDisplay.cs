using System.Collections;
using System.Collections.Generic;
using System.Text;
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
        // 1st attempt Tim.
        //for (int roll = 0; roll < rolls.Count; roll++)
        //{
        //    rollTexts[roll].text = rolls[roll].ToString();
        //}
        string rollsString = FormatRolls(rolls);
        for (int roll = 0; roll < rollsString.Length; roll++)
        {
            rollTexts[roll].text = rollsString[roll].ToString();
        }
    }

    public void FillFrameScores (List <int> frames)
    {
        for (int i = 0; i < frames.Count; i++)
        {
            frameTexts[i].text = frames[i].ToString();
        }
    }

    public static string FormatRolls (List <int> rolls)
    {
        string output = "";

        for (int i = 0; i < rolls.Count; i++)
        {
            int roll = output.Length + 1;

            // Miss.
            if (rolls[i] == 0)
            {
                output += "-";
            }
            // Spare.
            else if ((roll % 2 == 0) && (rolls[i] + rolls[i - 1] == 10))
            {
                output += "/";
            }
            // Strike in last frame.
            else if ((rolls[i] == 10) && (output.Length >= 18))
            {
                output += "X";
            }
            // Strike in frame 1-9.
            else if (rolls[i] == 10)
            {
                output += "X ";
            }
            // Normal roll.
            else
            {
                output += rolls[i].ToString();
            }
        }
        return output;
    }
}
