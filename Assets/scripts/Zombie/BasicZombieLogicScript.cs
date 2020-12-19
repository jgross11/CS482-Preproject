using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicZombieLogicScript : ZombieLogicScript
{
    
    private Employee employeeScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

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
        if(employeeScript != null){
            employeeScript.TakeDamage(value);
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
