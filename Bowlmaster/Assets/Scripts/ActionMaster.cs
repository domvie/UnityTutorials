using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMaster
{
    public enum Action { Tidy, Reset, EndTurn, EndGame };

    private int[] bowls = new int[21];
    private int bowl = 0; 

    public Action Bowl(int pins)
    {
        if (pins < 0 || pins > 10) { throw new UnityException("Invalid pins"); }
        
        if (pins == 10)
        {
            return Action.EndTurn;
        }

        if (bowl % 2 != 0)
        {
            // mid frame
            bowl += 1;
            return Action.Tidy;
        }



        throw new UnityException("Not sure what action to return");
    }
}
