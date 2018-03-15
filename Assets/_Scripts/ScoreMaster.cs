﻿using System.Collections;
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

        //int currentFrame = 0;
        int currentRoll = 1;

        foreach (int roll in rolls)
        {
            // First roll of a frame (odd currentRoll).
            if (currentRoll % 2 != 0)
            {
                // Is Strike.
                if ((roll == 10))
                {
                    // Check if next 2 rolls exist.
                    if (rolls.Count >= currentRoll + 2)
                    {
                        // Strike frame score = 10 + next 2 rolls.
                        frameScores.Add(10 + rolls[currentRoll] + rolls[currentRoll + 1]);
                    }
                }
                else
                {
                    currentRoll++;
                }
            }
            // Second roll of a frame (even currentRoll).
            else if (currentRoll % 2 == 0)
            {
                // Spare (10 pins after second roll of frame).
                if (rolls[currentRoll - 1] + rolls[currentRoll - 2] == 10)
                {
                    // Check if next roll exists.
                    if (rolls.Count >= currentRoll + 1)
                    {
                        // Spare frame score = 10 + next roll.
                        frameScores.Add(10 + rolls[currentRoll]);
                        currentRoll++;
                    }
                    else
                    {
                        currentRoll++;
                        return frameScores;
                    }
                }
                else
                {
                    // Regular frame score = 2nd roll + 1st roll.
                    frameScores.Add(roll + rolls[currentRoll - 2]);
                    currentRoll++;
                }
            }
            else
            {
                currentRoll++;
            }
        }
        return frameScores;
    }
}
