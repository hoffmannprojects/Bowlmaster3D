using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class ScoreDisplayTest
{

    [Test]
    public void T00_PassingTest ()
    {
        Assert.AreEqual(1, 1);
    }

    [Test]
    public void T01_bowl1 ()
    {
        int[] rolls = { 1 };
        string rollsString = "1";
        Assert.AreEqual(rollsString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T02_bowl12 ()
    {
        int[] rolls = { 1, 2 };
        string rollsString = "12";
        Assert.AreEqual(rollsString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T03_bowl1290 ()
    {
        int[] rolls = { 1, 2, 9, 0 };
        string rollsString = "129-";
        Assert.AreEqual(rollsString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T04_bowlStrike ()
    {
        int[] rolls = { 1, 2, 9, 0 , 10, 8 , 4};
        string rollsString = "129-X 84";
        Assert.AreEqual(rollsString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T05_bowl2Strikes ()
    {
        int[] rolls = { 1, 2, 9, 0, 10, 10, 8, 4 };
        string rollsString = "129-X X 84";
        Assert.AreEqual(rollsString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T06_bowlSpare9_1 ()
    {
        int[] rolls = { 1, 2, 9, 1 };
        string rollsString = "129/";
        Assert.AreEqual(rollsString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T07_bowlSpare0_10 ()
    {
        int[] rolls = { 1, 2, 0, 10 };
        string rollsString = "12-/";
        Assert.AreEqual(rollsString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T08_bowlPerfectGame ()
    {
        int[] rolls = { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10};
        string rollsString = "X X X X X X X X X X X X ";
        Assert.AreEqual(rollsString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T09_GutterGame ()
    {
        int[] rolls = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, };
        string rollsString = "--------------------";
        Assert.AreEqual(rollsString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T10_BowlX1 ()
    {
        int[] rolls = { 10, 1 };
        string rollsString = "X 1";
        Assert.AreEqual(rollsString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T11_Bowl2Spares ()
    {
        int[] rolls = { 1, 2, 3, 5, 5, 5, 3, 3, 7, 1, 9, 1, 6 };
        string rollsString = "12355/33719/6";
        Assert.AreEqual(rollsString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T12_BowlAllOnes ()
    {
        int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        string rollsString = "11111111111111111111";
        Assert.AreEqual(rollsString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T13_SpareInLastFrame ()
    {
        int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 9, 7 };
        string rollsString = "1111111111111111111/7";
        Assert.AreEqual(rollsString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }
}
