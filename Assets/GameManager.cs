using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private List<int> bowls = new List<int>();
    private Ball ball;
    private PinSetter pinSetter;
    private PinCounter pinCounter;

    // Needs to stay here (to be persistent).
    //private ActionMaster actionMaster = new ActionMaster();

    // Use this for initialization
    void Start ()
    {
        ball = GameObject.FindObjectOfType<Ball>();
        pinSetter = GameObject.FindObjectOfType<PinSetter>();
        pinCounter = GameObject.FindObjectOfType<PinCounter>();
    }

    public void HandleBowl (int fallenPins)
    {
        ball.Reset();
        bowls.Add(fallenPins);

        // Print bowls list.
        System.Text.StringBuilder builder = new System.Text.StringBuilder();
        foreach (int bowl in bowls)
        {
            builder.Append(bowl).Append(", ");
        }
        string bowlsList = builder.ToString();
        print ("Bowls: " + bowlsList);

        // Let actionMaster decide what action to do.
        //ActionMaster.Action nextAction = ActionMaster.NextAction(bowls);

        if (ActionMaster.NextAction(bowls) == ActionMaster.Action.Tidy)
        {
            pinSetter.Tidy();
        }
        else if (ActionMaster.NextAction(bowls) == ActionMaster.Action.Reset)
        {
            pinSetter.Reset();
            pinCounter.Reset();
        }
        else if (ActionMaster.NextAction(bowls) == ActionMaster.Action.EndTurn)
        {
            pinSetter.Reset();
            pinCounter.Reset();
        }
        else if (ActionMaster.NextAction(bowls) == ActionMaster.Action.EndGame)
        {
            throw new UnityException("EndGame handling not defined!");
        }
    }
}
