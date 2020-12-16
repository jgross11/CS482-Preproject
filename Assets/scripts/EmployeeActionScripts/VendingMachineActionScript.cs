using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachineActionScript : ActionScript
{
    public GameObject can;
    // cashiers can generate a coin as soon as possible,

    public float detectionRange;


    // so this method just returns true
    public override bool CanAct()
    {

        // uncomment for debugging range
        //Debug.DrawRay(transform.position, Vector3.right * detectionRange, Color.white, 2.0f, false);

        // collect information about potential zombie in view range
        // want to only find zombies, which reside on layer 'zombie', layermasking goes by activated bit, not integer value
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.right, detectionRange, 1 << LayerMask.NameToLayer("zombie"));

        // return validity of target existing and being zombie
        return (hit.collider != null && hit.collider.tag == "zombie");


    }

    public override void Act(int attack)
    {
        CanScript canScript = Instantiate(can, transform.position, transform.rotation).GetComponent<CanScript>();

        canScript.SetValue(attack);
    }
}
