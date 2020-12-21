using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillowActionScript : ActionScript
{

    //this needed to be added for there not to be errors, or the way Zombie/Employee interaction would have needed to be completely rewritten

    public override bool CanAct()
    {
        return true;
    }

    public override void Act(int attack)
    {
        
    }
}
