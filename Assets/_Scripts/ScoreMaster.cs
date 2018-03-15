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
        // Takes in a list of min 2 and max 21 rolls.
        // Returns a list of min 1 and max 10 frame scores.
    public static List<int> ScoreIndividualFrames (List<int> rolls)
    {
        List<int> frameScores = new List<int>();

        for (int currentRoll = 0; currentRoll <= rolls.Count - 1; currentRoll++)
        {
            // First roll in frame (even currentRoll).
            if (currentRoll % 2 == 0)
            {
                // Strike.
                if ((rolls[currentRoll] == 10))
                {
                    // Check if next 2 rolls exist.
                    if (rolls.Count >= currentRoll + 3)
                    {
                        // Strike frame score = 10 + next 2 rolls.
                        frameScores.Add(10 + rolls[currentRoll + 1] + rolls[currentRoll + 2]);
                        
                        // Remove Strike from the list.
                        rolls.RemoveAt(currentRoll);
                        currentRoll--;
                    }
                    else
                    {
                        return frameScores;
                    }
                }
            }
            // Second roll in frame (uneven currentRoll).
            else if (currentRoll % 2 != 0)
            {
                // Spare (10 pins after second roll of frame).
                if (rolls[currentRoll] + rolls[currentRoll - 1] == 10)
                {
                    // Check if next roll exists.
                    if (rolls.Count >= currentRoll + 2)
                    {
                        // Spare frame score = 10 + next roll.
                        frameScores.Add(10 + rolls[currentRoll + 1]);
                        //currentRoll++;
                    }
                    else
                    {
                        return frameScores;
                    }
                }
                // Regular frame 
                else
                {
                    // regular frame score = 2nd roll + 1st roll.
                    frameScores.Add(rolls[currentRoll] + rolls[currentRoll - 1]);
                }
            }
        }
        return frameScores;
    }
}
