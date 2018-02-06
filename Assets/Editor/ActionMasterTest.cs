using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class ActionMasterTest : MonoBehaviour
{
    private ActionMaster actionMaster;
    private ActionMaster.Action tidy = ActionMaster.Action.Tidy;
    private ActionMaster.Action reset = ActionMaster.Action.Reset;
    private ActionMaster.Action endTurn = ActionMaster.Action.EndTurn;
    private ActionMaster.Action endGame = ActionMaster.Action.EndGame;

    // Happens everytime a test runs.
    [SetUp]
    public void Setup ()
    {
        actionMaster = new ActionMaster();
    }

    // A game consists of 10 frames:
    // 9 frames with max. 2 balls rolled each.
    // + 1 frame with max. 3 balls rolled.
    // The 19th ball rolled is the first ball of the 10th frame.

    [Test]
    public void T00PassingTest ()
    {
        Assert.AreEqual(1, 1);
    }

    [Test]
    public void T01oneStrikeReturnsEndTurn()
    {
        Assert.AreEqual(endTurn, actionMaster.Bowl(10));
    }

    [Test]
    public void T02Bowl8ReturnsTidy()
    {
        Assert.AreEqual(tidy, actionMaster.Bowl(8));
    }

    [Test]
    public void T03Bowl28SpareReturnsEndTurn()
    {
        actionMaster.Bowl(8);
        Assert.AreEqual(endTurn, actionMaster.Bowl(2));
    }

    [Test]
    public void T04TwelveStrikesReturnEndGame()
    {
        RollBalls(11, 10);

        Assert.AreEqual(endGame, actionMaster.Bowl(10));
    }

    [Test]
    public void T0520ballsReturnEndGame ()
    {
        RollBalls(19, 0);

        Assert.AreEqual(endGame, actionMaster.Bowl(0));
    }

    [Test]
    public void T06CheckResetAtStrikeOnBall19 ()
    {
        RollBalls(18, 0);

        Assert.AreEqual(reset, actionMaster.Bowl(10));
    }

    [Test]
    public void T07CheckResetAtSpareInLastFrame ()
    {
        RollBalls(18, 0);

        RollBalls(1, 1);
        Assert.AreEqual(reset, actionMaster.Bowl(9));
    }

    [Test]
    public void T08EndGameAtBall20IfNoBall21Awarded ()
    {
        RollBalls(19, 1);

        Assert.AreEqual(endGame, actionMaster.Bowl(1));
    }

    [Test]
    public void T09StrikeOnBall19AndFailOnBall20ReturnsTidy ()
    {
        RollBalls(18, 1);
        RollBalls(1, 10);

        Assert.AreEqual(tidy, actionMaster.Bowl(1));
    }

    [Test]
    public void T10StrikeOnBall19AndZeroOnBall20ReturnsTidy ()
    {
        RollBalls(18, 1);
        RollBalls(1, 10);

        Assert.AreEqual(tidy, actionMaster.Bowl(0));
    }

    [Test]
    public void T11StrikeOnBall19AndStrikeOnBall20ReturnsReset ()
    {
        RollBalls(18, 1);
        RollBalls(1, 10);

        Assert.AreEqual(reset, actionMaster.Bowl(10));
    }

    [Test]
    public void T12Knocking10PinsOnSecondBallOfFrameIncreasesBallOnlyBy1 ()
    {
        RollBalls(1, 0);
        int lastBallCount = actionMaster.GetCurrentBall();

        RollBalls(1, 10);
        Assert.IsTrue(actionMaster.GetCurrentBall() == lastBallCount + 1);
    }

    [Test]
    public void T13Knocking10PinsOnSecondBallOfFrameIncreasesBallOnlyBy1TestB ()
    {
        RollBalls(1, 0);
        RollBalls(1, 10);
        RollBalls(1, 5);
        Assert.AreEqual(endTurn, actionMaster.Bowl(1));
    }

    [Test]
    public void T14Dondi10thFrameTurkey ()
    {
        RollBalls(18, 1);

        Assert.AreEqual(reset, actionMaster.Bowl(10));
        Assert.AreEqual(reset, actionMaster.Bowl(10));
        Assert.AreEqual(endGame, actionMaster.Bowl(10));
    }

    private void RollBalls (int ballsToRoll, int pinsHit)
    {
        for (int i = 1; i <= ballsToRoll; i++)
        {
            actionMaster.Bowl(pinsHit);
        }
    }
}
