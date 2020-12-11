using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// abstract class representing an action taken by a tower (and zombie?)
public abstract class ActionScript : MonoBehaviour
{
    // must return true if the tower can act, false otherwise
    public abstract bool CanAct();

    // executes the actual action the tower does
    // the results of this action are dependent on 
    // one of the employee's stats
    public abstract void Act(int scalingStat);
}
