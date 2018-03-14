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

    public void HandleBowlResult (int fallenPins)
    {
        ball.Reset();
        bowls.Add(fallenPins);

        // Let actionMaster decide what action to do.
        //ActionMaster.NextAction nextAction = ActionMaster.NextAction(bowls);

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
            print("EndTurn triggering Reset");
            pinSetter.Reset();
            pinCounter.Reset();
        }
        else if (ActionMaster.NextAction(bowls) == ActionMaster.Action.EndGame)
        {
            throw new UnityException("EndGame handling not defined!");
        }
    }
}
