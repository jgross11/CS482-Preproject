using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicZombieLogicScript : ZombieLogicScript
{
    // employee script of target
    private Employee employeeScript;

    // true when attack animation ends, must make position level
    private bool resetPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if just attacked
        if(resetPosition){

            // rotate back to base position over time
            if(transform.parent.rotation.z > 0) transform.parent.Rotate(Vector3.back*0.3f);

            // reset for next attack
            else resetPosition = false;
        }
    }

    // determines if zombie can move
    public override bool CanMove(){
        return actionTarget == null;
    }

    // determines way in which zombie moves
    public override void Move(){
        transform.parent.position += Vector3.left * movementSpeed * Time.deltaTime;
    }

    // determines if zombie can act
    public override bool CanAct(){
        return actionTarget != null;
    }

    // determines way in which zombie acts
    public override void Act(){

        // if a target exists
        if(employeeScript != null){

            // deal damage to target
            employeeScript.TakeDamage(value);

            // attack animation
            transform.parent.Rotate(new Vector3(0, 0, 30));
            resetPosition = true;
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        if(actionTarget == null && col.tag == "employee"){
            actionTarget = col.gameObject;
            employeeScript = actionTarget.GetComponent<Employee>();
        }
    }

    void OnTriggerExit2D(Collider2D col){
        if(actionTarget == null && col.gameObject == actionTarget){
            actionTarget = null;
            employeeScript = null;
        }
    }
    void OnTriggerStay2D(Collider2D col){
        if(actionTarget == null && col.tag == "employee"){
            actionTarget = col.gameObject;
            employeeScript = actionTarget.GetComponent<Employee>();
        }
    }
}
