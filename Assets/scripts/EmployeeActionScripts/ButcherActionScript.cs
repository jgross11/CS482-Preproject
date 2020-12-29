using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButcherActionScript : ActionScript
{
    // indicates whether a zombie is in attack range
    public bool zombieInRange = false;

    // indicates whether the attack animation is playing
    public bool isActing = false;

    // the collider of the zombie in attack range
    public Collider2D zombieCollider;

    // duration to play 'animation'
    public float animationDuration;
    
    // butchers can act when there is a zombie in its melee range
    public override bool CanAct(){
        return zombieInRange;
    }

    // handles attack 'animation' reset
    public IEnumerator ActionAnimationReset(){
        // wait for duration of animation 
        yield return new WaitForSeconds(animationDuration);

        // reset parent rotation
        transform.parent.rotation = Quaternion.identity;

        // reset animation state variable
        isActing = false;
    }

    void Update(){
        // if the tower is attacking, 'animate'
        if(isActing){
            // rotate tower itself 
            transform.parent.Rotate(0, 0, -2);
        }
    }

    // butchers act by swinging their cleaver at a nearby zombie
    public override void Act(int value){
        // if a zombie is in range
        if(zombieCollider != null){

            // start animation
            isActing = true;

            // start animation stop timer
            StartCoroutine(ActionAnimationReset());

            // deal damage to zombie
            zombieCollider.GetComponent<Zombie>().Damage(value);

            // add experience to the tower
            employeeScript.AddExperience(value);
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        // if new zombie enters attack range and one isn't already targeted
        if(zombieCollider == null && col.tag == "zombie"){
            
            // indicate zombie is in range
            zombieInRange = true;

            // set zombie collider appropriately
            zombieCollider = col;
        }
    }

    void OnTriggerStay2D(Collider2D col){
        // if another zombie is in range after targeted zombie leaves (dies)
        if(zombieCollider == null && col.tag == "zombie"){
            
            // indicate new zombie is in range
            zombieInRange = true;

            // set new zombie collider appropriately
            zombieCollider = col;
        }
    }

    void OnTriggerExit2D(Collider2D col){
        // if targeted zombie has left attack range
        if(col == zombieCollider){

            // reset range variables to stop targeting
            // this allows OnTriggerEnter2D and OnTriggerStay2D to target next zombie when appropriate
            zombieInRange = false;
            zombieCollider = null;
        }
    }
}
