using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMaster
{
    public enum Action
    {
        Tidy, EndTurn
    }

    public Action Bowl (int pins)
    {
        if ( pins == 10)
        {
            return Action.EndTurn;
        }
        return Action.Tidy;
    }

}
