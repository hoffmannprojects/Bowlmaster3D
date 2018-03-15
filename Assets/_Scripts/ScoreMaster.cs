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

        // code here

        return frameList;
    }
}
