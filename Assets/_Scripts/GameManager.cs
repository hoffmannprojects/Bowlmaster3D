using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private List<int> rolls = new List<int>();
    private Ball ball;
    private PinSetter pinSetter;
    private PinCounter pinCounter;
    private ScoreDisplay scoreDisplay;

    // Use this for initialization
    void Start ()
    {
        ball = GameObject.FindObjectOfType<Ball>();
        pinSetter = GameObject.FindObjectOfType<PinSetter>();
        pinCounter = GameObject.FindObjectOfType<PinCounter>();
        scoreDisplay = GameObject.FindObjectOfType<ScoreDisplay>();
    }

    public void HandleRoll (int fallenPins)
    {
        ball.Reset();
        rolls.Add(fallenPins);
        PrintRollsToConsole();

        try
        {
            scoreDisplay.FillRollScores(rolls);
            scoreDisplay.FillFrameScores(ScoreMaster.ScoreCumulative(rolls));
        }
        catch
        {
            Debug.LogWarning("scoreDisplay not functioning.");
        }

        if (ActionMaster.NextAction(rolls) == ActionMaster.Action.Tidy)
        {
            pinSetter.Tidy();
            Debug.Log("Tidy.");
        }
        else if (ActionMaster.NextAction(rolls) == ActionMaster.Action.Reset)
        {
            pinSetter.Reset();
            pinCounter.Reset();
            Debug.Log("Reset.");
        }
        else if (ActionMaster.NextAction(rolls) == ActionMaster.Action.EndTurn)
        {
            pinSetter.Reset();
            pinCounter.Reset();
            Debug.Log("Reset (End Turn).");
        }
        else if (ActionMaster.NextAction(rolls) == ActionMaster.Action.EndGame)
        {
            throw new UnityException("EndGame handling not defined!");
        }
    }

    private void PrintRollsToConsole ()
    {
        StringBuilder builder = new StringBuilder();
        foreach (int roll in rolls)
        {
            builder.Append(roll).Append(", ");
        }
        string rollsList = builder.ToString();
        Debug.Log("Bowls: " + rollsList);
    }
}
