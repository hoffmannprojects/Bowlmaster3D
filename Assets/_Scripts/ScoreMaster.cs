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
        List<int> frameScores = new List<int>();

        // Look at every 2nd roll (1st roll is index of 0).
        for (int currentRoll = 1; currentRoll < rolls.Count; currentRoll+=2)
        {
            // Prevent calculation of more than 10 frames.
            if (frameScores.Count == 10)
            {
                break;
            }

            // Normal "open" frame.
            if (rolls[currentRoll - 1] + rolls[currentRoll] < 10)
            {
                frameScores.Add(rolls[currentRoll - 1] + rolls[currentRoll]);
            }

            // Proceed only if 2+ adjecent rolls are present.
            if (rolls.Count - currentRoll < 2)
            {
                break;
            }

            // Strike.
            if (rolls[currentRoll - 1] == 10)
            {
                // Strike frame has just one roll.
                currentRoll--;

                frameScores.Add(10 + rolls[currentRoll + 1] + rolls[currentRoll + 2]);
            }
            // Spare.
            else if (rolls[currentRoll - 1] + rolls[currentRoll] == 10)
            {
                frameScores.Add(10 + rolls[currentRoll + 1]);
            }  
        }
        return frameScores;
    }
}
