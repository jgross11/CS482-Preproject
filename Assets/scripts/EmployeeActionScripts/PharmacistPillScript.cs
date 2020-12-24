using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PharmacistPillScript : MonoBehaviour
{
    
    // healing value of this pill
    public int value;

    // the target tower to move towards when active
    public GameObject target;

    // indicates whether this pill is moving towards target or not
    public bool isActive;

    // position to move towards when created
    private Vector3 randDirection;

    // time to move from tower origin to randDirection
    public float maxMoveTime = 1.0f;

    // amount of time this pill has been alive for
    private float currentLifeTime = 0.0f;

    // amount of time to wait until destroying this pill
    public float lifeTime = 7.0f;

    // speed at which pill moves towards target when active
    public float moveSpeed = 15.0f;

    // the tower which created this pill
    public PharmacistActionScript parentScript;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // set the desired position of this pill
    public void SetDesiredPosition(Vector3 dir){
        randDirection = dir;
    }

    // Update is called once per frame
    void Update()
    {
        // add to pill's current lifetime
        currentLifeTime += Time.deltaTime;

        // if pill is active
        if(isActive){
            
            // destroy this pill if its target dies while it is moving to it
            if(target == null){
                Destroy(this.gameObject);
            } else{
                // move towards target and rotate for neat effect
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
                transform.Rotate(Vector3.forward);
            }
        } else{
            // destroy this pill if the pharmacist who created it dies
            // only wish to destroy it when not already in transit
            if(parentScript == null){
                Destroy(this.gameObject);
            }
        }

        // otherwise if pill still needs to move towards desired position
        else if(currentLifeTime < maxMoveTime){
            
            // move pill towards desired position
            transform.position = Vector3.MoveTowards(transform.position, randDirection, moveSpeed * Time.deltaTime);
        }

        // otherwise if pill has overstayed its welcome
        else if(currentLifeTime >= lifeTime){

            // destroy pill
            Destroy(this.gameObject);
        }
    }

    // set healing value 
    public void SetValue(int val){
        value = val;
    }

    // set target tower
    public void SetTarget(GameObject newTarget){
        target = newTarget;
    }

    // set parent tower's script reference 
    public void SetParentScript(PharmacistActionScript ps){
        parentScript = ps;
    }

    // when the pill is clicked on 
    void OnMouseDown(){

        // get tower's current target
        target = parentScript.GetTarget();

        // if target exists
        if(target != null){

            // indicate pill should move towards tower
            isActive = true;
        }
    }

    // when pill collides with something
    void OnTriggerEnter2D(Collider2D col){
        
        // if the pill collides with its target
        if(isActive && target != null && col == target.GetComponent<Collider2D>()){
            
            // heal target by this pill's amount
            target.GetComponent<Employee>().Heal(value);

            // destroy pill
            Destroy(this.gameObject);
        }
    }

    // if pill happens to already have been colliding with target while active
    // same thing as OnTriggerEnter2D
    void OnTriggerStay2D(Collider2D col){
        if(isActive && target != null && col == target.GetComponent<Collider2D>()){
            target.GetComponent<Employee>().Heal(value);
            Destroy(this.gameObject);
        }
    }
}
