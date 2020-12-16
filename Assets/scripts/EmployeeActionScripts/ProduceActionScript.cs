using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProduceActionScript : ActionScript
{

    // prefab reference for carrot projectile
    public GameObject carrotPrefab;

    // distance from tower to shoot at
    public float detectionRange;

    // produce tower attacks by creating a carrot projectile to hurl at zombies
    public override void Act(int attack){

        // create carrot at this location and obtain reference to its script 
        ProduceCarrotScript carrotScript = Instantiate(carrotPrefab, transform.position, transform.rotation).GetComponent<ProduceCarrotScript>();
        
        // set carrot damage value
        carrotScript.SetValue(attack);
    }

    // produce towers can act whenever a zombie is within their range
    public override bool CanAct(){
        
        // uncomment for debugging range
        // Debug.DrawRay(transform.position, Vector3.right * detectionRange, Color.white, 2.0f, false);

        // collect information about potential zombie in view range
        // want to only find zombies, which reside on layer 'zombie', layermasking goes by activated bit, not integer value
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.right, detectionRange, 1 << LayerMask.NameToLayer("zombie"));

        // return validity of target existing and being zombie
        return (hit.collider != null && hit.collider.tag == "zombie");
    }
}
