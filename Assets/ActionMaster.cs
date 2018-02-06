using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMaster
{
    public enum Action
    {
        Tidy, Reset, EndTurn, EndGame
    }

    private int currentBall = 1;
    private int[] scoreOfBall = new int[21];

    public Action Bowl (int pins)
    {
        if (pins < 0 || pins > 10)
        {
            throw new UnityException("Invalid pin count!");
        }

        scoreOfBall[currentBall - 1] = pins;

        if (currentBall == 21)
        {
            return Action.EndGame;
        }

        // Last frame: Strike or Spare.
        if (currentBall >= 19 && Ball21Awarded())
        {
            currentBall++;
            return Action.Reset;
        }

        // Strike before last frame.
        if ( pins == 10)
        {
            currentBall += 2;

            return Action.EndTurn;
        }

        // First ball in frame.
        if (currentBall % 2 != 0)
        {
            currentBall ++;
            return Action.Tidy;
        }
        // Second ball in frame.
        else if (currentBall % 2 == 0)
        {
            currentBall++;

            if (currentBall < 20)
            {
            return Action.EndTurn;
            }

            // 20th+ ball.
            return Action.EndGame;
        }

        throw new UnityException("Not sure what action to return!");
    }

    private bool Ball21Awarded ()
    {
        return ((scoreOfBall[19 - 1] + scoreOfBall[20 - 1]) >= 10);
    }
}
