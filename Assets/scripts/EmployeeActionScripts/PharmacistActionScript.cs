using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PharmacistActionScript : ActionScript
{
    // tower to target when healing
    public GameObject target;

    // target tower's employee script
    private Employee targetScript;

    // this tower's employee script
    private Employee thisScript;

    // prefab for pill to create
    public GameObject pillPrefab;

    // indicates whether or not this tower is currently selecting a target
    public bool isSelecting = false;

    // the employee tower's collider
    public PolygonCollider2D parentCollider;

    // the employee tower's sprite
    public SpriteRenderer parentSpriteRenderer;

    // the target tower's sprite
    public SpriteRenderer targetSpriteRenderer;

    // number of pills created in total
    public int pillsOut;

    // maximum amount of pills able to be out at any given time
    public int maxAmountOfPills;
    
    public override void Start()
    {
        // reset isSelecting to false after a short period of time to circumvent
        // bug wherein placing this tower automatically selects it
        StartCoroutine(EnableSelection());

        // used for sprite updating
        thisScript = transform.parent.GetComponent<Employee>();

        // at start, no pills have been created
        pillsOut = 0;

        // employee reference
        employeeScript = transform.parent.GetComponent<Employee>();

        // max amount of pills = pill lifetime / tower firing speed
        maxAmountOfPills = (int) (pillPrefab.GetComponent<PharmacistPillScript>().lifeTime / parentCollider.GetComponent<Employee>().timeBetweenActions);
    }

    // reset isSelecting after a short period of time
    private IEnumerator EnableSelection(){
        yield return new WaitForSeconds(0.001f);
        isSelecting = false;

        // update tower / target sprite colors
        UpdateColors(false);
    }

    // Update is called once per frame
    void Update()
    {
        // if the left mouse button is clicked
        if(Input.GetMouseButtonDown(0)){

            // get world space from mouse position on screen and collect information regarding possible collision
            RaycastHit2D rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));

            // if tower is not selecting and itself was clicked
            if (!isSelecting && rayHit.collider == parentCollider){
                
                // tower is now selecting
                isSelecting = true;

                // indicate sprite is selected
                UpdateColors(true);

            // otherwise if tower is selecting and nothing or current target is selected
            } else if(isSelecting && rayHit.collider == null || (target != null && rayHit.collider == target.GetComponent<Collider>())){
                
                // cancel selecting
                isSelecting = false;
                
                // indicate sprite is not selected
                UpdateColors(false);

            // otherwise if tower is selecting and an employee was selected
            } else if(rayHit.collider != null && rayHit.collider.tag == "employee" && isSelecting){
                
                // if another tower was already selected 
                if(target != null){

                    // undo graphical representation of selection on old tower
                    targetScript.UpdateSprite();
                }

                // target becomes the newly selected tower
                target = rayHit.collider.gameObject;

                // get new target sprite
                targetSpriteRenderer = target.GetComponent<SpriteRenderer>();

                // stop selecting
                isSelecting = false;

                // get target employee script
                targetScript = target.GetComponent<Employee>();

                // if the target is a pharmacist
                if(targetScript.type == "pharmacist"){
                    // wait a short amount of time and reset target's selection boolean
                    // this prevents double selection bug
                    StartCoroutine(target.GetComponentInChildren<PharmacistActionScript>().EnableSelection());
                }

                // update this and target's sprites to indicate no more selecting
                UpdateColors(false);
            }
        }
    }

    // updates the sprite colors of both this tower and its target
    // isSelected: true to indicate this tower and its target (if applicable) are selected
    // false otherwise
    private void UpdateColors(bool isSelected){
        // if towers are selected
        if(isSelected){

            // if target exists and is not self, indicate both towers are selected
            if(target != null && target.gameObject != parentSpriteRenderer.gameObject){
                targetSpriteRenderer.color = Color.yellow;
                parentSpriteRenderer.color = Color.cyan;

            // otherwise if target is self, indicate both selections simultaneously
            } else if(target.gameObject == parentSpriteRenderer.gameObject){
                parentSpriteRenderer.color = Color.green;
            }
            
            // otherwise no target exists, just indicate this tower is selected
             else{
                parentSpriteRenderer.color = Color.cyan;
             }

        // otherwise, reset this and target tower's sprite to default
        } else{
            thisScript.UpdateSprite();
            if(target != null){
                targetScript.UpdateSprite();
            }
        }
    }

    // returns this tower's target
    public GameObject GetTarget(){
        return target;
    }

    // pharmacists can act ASAP, just return true
    public override bool CanAct(){
        return true;
    }

    // pharmacist action: the action of a pharmacist is to produce a pill that heals
    // create pill in front of this tower with this tower's attack as its healing power
    public override void Act(int attack)
    {
        // create instance of pill here, obtain reference to its script
        PharmacistPillScript pillScript = Instantiate(pillPrefab, new Vector3(transform.position.x, transform.position.y, -9), transform.rotation).GetComponent<PharmacistPillScript>();

        // set healing power relative to this tower's attack
        // TODO balancing
        pillScript.SetValues(employeeScript, attack/3 + 1);

        // set pill's initial target to be the currently selected tower 
        pillScript.SetTarget(target);

        // attach reference to this script to use when finding new target
        pillScript.SetParentScript(this);

        // generate correct position for pill to move to
        // spreads in a semi- half circle around tower in a cyclic fasion
        // do not worry about this math, it is totally legit
        float xPos = 1.5f * (float) Math.Sin(-(Math.PI / 4.0) + (((pillsOut % maxAmountOfPills) + 2) * Math.PI / (float) (maxAmountOfPills+1)));
        float yPos = 1.5f * (float) Math.Cos(-(Math.PI / 4.0) + (((pillsOut % maxAmountOfPills) + 2) * Math.PI / (float) (maxAmountOfPills+1)));

        // increment number of pills that have been created
        pillsOut++;

        // set pill's target direction to the generated x, y pos
        pillScript.SetDesiredPosition(new Vector3(transform.position.x + xPos, transform.position.y + yPos, -9));

    }
}
