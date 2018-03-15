using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMaster {

    // Returns a list of cumulative scores, like a normal score card.
    public static List<int> ScoreCumulative (List<int> rolls)
    {
        List<int> cumulativeScores = new List<int>();
        int cumulativeTotal = 0;

        foreach (int frameScore in ScoreIndividualFrames(rolls)) 
        {
            cumulativeTotal += frameScore;
            cumulativeScores.Add(cumulativeTotal);
        }
        return cumulativeScores;
    }

    // Returns a list of individual frame scores, NOT cumulative.
    public static List<int> ScoreIndividualFrames (List<int> rolls)
    {
        List<int> frameList = new List<int>();

        // Takes in a list of min 1 and max 21 rolls.
        // Returns a list of min 1 and max 10 frames.
        // Frame score = 1st roll + 2nd roll.
            // Strike: 10 pins in the first roll of a frame.
                // Strike frame score = 10 + next 2 rolls.
            // Spare: 10 pins after second roll of frame (1st roll in a frame <= 9; 1st + 2nd roll = 10).
                // Spare frame score = 10 + next roll.

        return frameList;
    }
}
