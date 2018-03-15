using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class ActionMasterTest : MonoBehaviour
{
    private List<int> bowlResults;

    private ActionMaster.Action tidy = ActionMaster.Action.Tidy;
    private ActionMaster.Action reset = ActionMaster.Action.Reset;
    private ActionMaster.Action endTurn = ActionMaster.Action.EndTurn;
    private ActionMaster.Action endGame = ActionMaster.Action.EndGame;


    // Happens everytime a test runs.
    [SetUp]
    public void Setup()
    {
        bowlResults = new List<int>();
    }

    // A game consists of 10 frames:
    // 9 frames with max. 2 balls rolled each.
    // + 1 frame with max. 3 balls rolled.
    // The 19th ball rolled is the first ball of the 10th frame.

    [Test]
    public void T00PassingTest()
    {
        Assert.AreEqual(1, 1);
    }

    [Test]
    public void T01oneStrikeReturnsEndTurn()
    {
        bowlResults.Add(10);
        Assert.AreEqual(endTurn, ActionMaster.NextAction(bowlResults));
    }

    [Test]
    public void T02Bowl8ReturnsTidy()
    {
        bowlResults.Add(8);
        Assert.AreEqual(tidy, ActionMaster.NextAction(bowlResults));
    }

    [Test]
    public void T03Bowl28SpareReturnsEndTurn()
    {
        int[] bowls = { 2, 8 };
        Assert.AreEqual(endTurn, ActionMaster.NextAction(bowls.ToList()));
    }

    [Test]
    public void T04TwelveStrikesReturnEndGame()
    {
        RollBalls(12, 10);

        Assert.AreEqual(endGame, ActionMaster.NextAction(bowlResults));
    }

    [Test]
    public void T0520ballsReturnEndGame()
    {
        RollBalls(20, 0);

        Assert.AreEqual(endGame, ActionMaster.NextAction(bowlResults));
    }

    [Test]
    public void T06CheckResetAtStrikeOnBall19()
    {
        RollBalls(18, 0);
        RollBalls(1, 10);

        Assert.AreEqual(reset, ActionMaster.NextAction(bowlResults));
    }

    [Test]
    public void T07CheckResetAtSpareInLastFrame()
    {
        RollBalls(18, 0);
        RollBalls(1, 1);
        RollBalls(1, 9);

        Assert.AreEqual(reset, ActionMaster.NextAction(bowlResults));
    }

    [Test]
    public void T08EndGameAtBall20IfNoBall21Awarded()
    {
        RollBalls(20, 1);

        Assert.AreEqual(endGame, ActionMaster.NextAction(bowlResults));
    }

    [Test]
    public void T09StrikeOnBall19AndFailOnBall20ReturnsTidy()
    {
        RollBalls(18, 1);
        RollBalls(1, 10);
        RollBalls(1, 1);

        Assert.AreEqual(tidy, ActionMaster.NextAction(bowlResults));
    }

    [Test]
    public void T10StrikeOnBall19AndZeroOnBall20ReturnsTidy()
    {
        RollBalls(18, 1);
        RollBalls(1, 10);
        RollBalls(1, 0);

        Assert.AreEqual(tidy, ActionMaster.NextAction(bowlResults));
    }

    [Test]
    public void T11StrikeOnBall19AndStrikeOnBall20ReturnsReset()
    {
        RollBalls(18, 1);
        RollBalls(2, 10);

        Assert.AreEqual(reset, ActionMaster.NextAction(bowlResults));
    }

    [Test]
    public void T13Knocking10PinsOnSecondBallOfFrameIncreasesBallOnlyBy1TestB()
    {
        RollBalls(1, 0);
        RollBalls(1, 10);
        RollBalls(1, 5);
        RollBalls(1, 1);
        Assert.AreEqual(endTurn, ActionMaster.NextAction(bowlResults));
    }

    [Test]
    public void T14Dondi10thFrameTurkey()
    {
        RollBalls(18, 1);
        RollBalls(3, 10);
        Assert.AreEqual(endGame, ActionMaster.NextAction(bowlResults));
    }

    [Test]
    public void T15Knocking0ThenKnocking1ReturnsEndTurn()
    {
        RollBalls(1, 0);
        RollBalls(1, 1);

        Assert.AreEqual(endTurn, ActionMaster.NextAction(bowlResults));
    }

    private void RollBalls(int ballsToRoll, int pinsHit)
    {
        for (int i = 1; i <= ballsToRoll; i++)
        {
            bowlResults.Add(pinsHit);
        }
    }
}
