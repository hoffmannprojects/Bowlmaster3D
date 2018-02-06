﻿using System;
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

        // Checking for special cases first, more general cases later.

        if (currentBall == 21)
        {
            return Action.EndGame;
        }

        // Strike on ball 19 but fail on ball 20.
        if ((currentBall == 20) && !BallWasStrike(currentBall) && BallWasStrike(19))
        {
            return Action.Tidy;
        }

        // Last frame: Strike or Spare.
        if (currentBall >= 19 && Ball21Awarded())
        {
            currentBall++;
            return Action.Reset;
        }

        // Strike before last frame.
        if (BallWasStrike(currentBall))
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
        return (GetScoreOfBall(19) + GetScoreOfBall(20) >= 10);
    }

    private bool BallWasStrike(int ballToCheck)
    {
        return (scoreOfBall[ballToCheck - 1] == 10);
    }

    private int GetScoreOfBall(int ballToGetScoreOf)
    {
        return scoreOfBall[ballToGetScoreOf - 1];
    }
}
