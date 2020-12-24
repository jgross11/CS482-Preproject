using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnerActionScript : ActionScript
{
    // the zombie to deal damage to when attacking
    public Zombie target;

    // true when attack animation ends, must make position level
    private bool resetPosition;


    // Start is called before the first frame update
    void Start()
    {
        resetPosition = false;
    }

    // Update is called once per frame
    void Update()
    {
        // if just attacked
        if(resetPosition){

            // slowly rotate back to kneeling position over time
            if(transform.parent.rotation.z > 0) transform.parent.Rotate(Vector3.back*0.3f);

            // reset for next attack
            else resetPosition = false;
        } 
    }

    // Gunners target one zombie at a time, focusing on the same one until it dies, 
    // and then move on to the next one. They are not limited by range, so they can act
    // so long as there is at least one zombie on the field.
    public override bool CanAct(){
        // if no current target
        if (target == null){

            // attempt to find a zombie game object
            GameObject go = GameObject.FindWithTag("zombie");

            // if no zombie found, no zombies to target
            if (go == null) return false;

            // otherwise, target becomes first found gameobject
            target = go.GetComponent<Zombie>();
        }
        
        // target either already exists or was just found
        return true;
    }

    // Gunners shoot bullets that move too fast to be seen, so their action is to simply
    // apply the damage to their target and play their attack animation
    public override void Act(int attackValue){
        // if target still exists, deal damage
        if (target != null) target.Damage(attackValue);

        // rotate by 45 degrees to indicate shot was fired
        transform.parent.Rotate(new Vector3(0, 0, 45));

        // indicate position resetting must occur
        resetPosition = true;
    }
}
