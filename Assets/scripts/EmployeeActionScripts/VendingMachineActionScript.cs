using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachineActionScript : ActionScript
{
    public GameObject can;
    // cashiers can generate a coin as soon as possible,
    // so this method just returns true
    public override bool CanAct()
    {
        return true;
    }

    public override void Act(int attack)
    {
        Instantiate(can, transform.position, transform.rotation).GetComponent<CanScript>().setValue(attack / 3 + 1);
    }
}
