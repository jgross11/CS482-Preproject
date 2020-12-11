using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// cashier action logic
public class CashierActionScript : ActionScript
{

    public GameObject coin;
    // cashiers can generate a coin as soon as possible,
    // so this method just returns true
    public override bool CanAct()
    {
        return true;
    }

    public override void Act(int attack)
    {
        Instantiate(coin, transform.position, transform.rotation).GetComponent<CashierCoinScript>().setValue(attack/3 + 1);
    }
}
