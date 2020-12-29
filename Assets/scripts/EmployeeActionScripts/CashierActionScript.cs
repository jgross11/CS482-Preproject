using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// cashier action logic
public class CashierActionScript : ActionScript
{

    public GameObject coin;

    // list of coins created by this tower
    private List<GameObject> coins;

    public override void Start(){
        coins = new List<GameObject>();
        employeeScript = transform.parent.GetComponent<Employee>();
    }

    // cashiers can generate a coin as soon as possible,
    // so this method just returns true
    public override bool CanAct()
    {
        return true;
    }

    // cashiers act by creating a coin to be collected by the player
    public override void Act(int attack)
    {
        // create coin game object
        GameObject createdCoin = Instantiate(coin, transform.position - Vector3.forward, transform.rotation);

        // set created coin employee reference, worth value
        createdCoin.GetComponent<CashierCoinScript>().SetValues(employeeScript, attack/3 + 1);

        // add coin to list of coins this tower created
        coins.Add(createdCoin);
    }

    void OnDestroy(){

        // must destroy any created coins upon destruction of this tower
        foreach(GameObject createdCoin in coins)
        {
            Destroy(createdCoin);
        }
    }
}
