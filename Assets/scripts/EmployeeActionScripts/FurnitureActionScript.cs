using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureActionScript : ActionScript
{
   

    public override bool CanAct()
    {
        return true;
    }

    public override void Act(int attack)
    {

        //not sure if we want to implement some form of attack, PvZ's wall does no damage.
        //We could possibly do something similar to the pharmacist where, rather than the tower itself being the wall
        //he throws 2 or 3 pillows with x amount of health on them out in front, and thats what the zombies need to get through...
        //this seems to present itself with some....issues though....we can discuss it later

    }
}
