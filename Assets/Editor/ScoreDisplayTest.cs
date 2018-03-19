using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class ScoreDisplayTest
{

    [Test]
    public void T00PassingTest ()
    {
        Assert.AreEqual(1, 1);
    }

    [Test]
    public void T01bowl1 ()
    {
        int[] rolls = { 1 };
        string rollsString = "1";
        Assert.AreEqual(rollsString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }
}
