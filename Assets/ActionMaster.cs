using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMaster
{
    public enum Action
    {
        Tidy, EndTurn
    }

    private int bowl = 1;

    public Action Bowl (int pins)
    {
        if (pins < 0 || pins > 10)
        {
            throw new UnityException("Invalid pin count!");
        }

        // Other behaviour here, e.g. last frame.

        // Strike.
        if ( pins == 10)
        {
            bowl += 2;
            return Action.EndTurn;
        }

        // First bowl in frame.
        if (bowl % 2 != 0)
        {
            bowl ++;
            return Action.Tidy;
        }
        // Second bowl in frame.
        else if (bowl % 2 == 0)
        {
            bowl++;
            return Action.EndTurn;
        }


        throw new UnityException("Not sure what action to return!");
    }

}
