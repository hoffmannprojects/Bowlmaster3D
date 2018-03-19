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
        // 1st attempt Tim.
        //StringBuilder builder = new StringBuilder();
        //foreach (int roll in rolls)
        //{
        //    builder.Append(roll).Append(", ");
        //}
        //string output = builder.ToString();

        //string output = "";

        StringBuilder stringBuilder = new StringBuilder();

        for (int i = 0; i < rolls.Count; i++)
        {
            string rollConverted = rolls[i].ToString();

            // Miss.
            if (rolls[i] == 0)
            {
                rollConverted = "-";
            }
            // Spare.
            if (
                (i > 0) && 
                (i % 2 != 0) && 
                (rolls[i] + rolls[i - 1] == 10)
                )
            {
                rollConverted = "/";
            }
            // Strike.
            else if ((rolls[i] == 10))
            {
                rollConverted = "X ";
            }
            stringBuilder.Append(rollConverted);
        }
        string output = stringBuilder.ToString();
        return output;
    }
}
