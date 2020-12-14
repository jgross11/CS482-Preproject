using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PharmacistActionScript : ActionScript
{

    public GameObject target;
    public GameObject pillPrefab;
    public bool isSelecting = false;
    public PolygonCollider2D parentCollider;
    public SpriteRenderer parentSpriteRenderer;
    public SpriteRenderer targetSpriteRenderer;
    public int pillsOut;
    public int maxAmountOfPills;
    
    void Start()
    {
        // reset isSelecting to false after a short period of time to circumvent
        // bug wherein placing this tower automatically selects it
        StartCoroutine(EnableSelection());
        pillsOut = 0;
        maxAmountOfPills = (int) (pillPrefab.GetComponent<PharmacistPillScript>().lifeTime / parentCollider.GetComponent<Employee>().timeBetweenActions);
    }

    private IEnumerator EnableSelection(){
        yield return new WaitForSeconds(0.001f);
        isSelecting = false;
        UpdateColors(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            RaycastHit2D rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
            if (!isSelecting && rayHit.collider == parentCollider){
                isSelecting = true;
                UpdateColors(true);
                Debug.Log("now selecting target");
            } else if(isSelecting && rayHit.collider == null || (target != null && rayHit.collider == target.GetComponent<Collider>())){
                isSelecting = false;
                Debug.Log("canceling selection 1");
                UpdateColors(false);
            } else if(rayHit.collider != null && rayHit.collider.tag == "employee" && isSelecting){
                if(target != null){
                    targetSpriteRenderer.color = Color.white;
                }
                target = rayHit.collider.gameObject;
                targetSpriteRenderer = target.GetComponent<SpriteRenderer>();
                Debug.Log("selected target");
                isSelecting = false;
                Employee targetScript = target.GetComponent<Employee>();
                Debug.Log(targetScript.type);
                if(targetScript.type == "pharmacist"){
                    target.GetComponentInChildren<PharmacistActionScript>().SetSelected(false);
                }
                UpdateColors(false);
            }
        }


        // TODO make this more apparent
        if(isSelecting){
            
        } else{

        }

    }

    private void UpdateColors(bool isSelected){
        if(isSelected){
            if(target != null && target.gameObject != parentSpriteRenderer.gameObject){
                targetSpriteRenderer.color = Color.red;
                parentSpriteRenderer.color = Color.cyan;
            } else if(target.gameObject == parentSpriteRenderer.gameObject){
                parentSpriteRenderer.color = Color.magenta;
            }
             else{
                parentSpriteRenderer.color = Color.cyan;
             }
        } else{
            parentSpriteRenderer.color = Color.white;
            if(target != null){
                targetSpriteRenderer.color = Color.white;
            }
        }
    }

    public GameObject GetTarget(){
        return target;
    }

    public override bool CanAct(){
        return true;
    }

    public override void Act(int attack)
    {
        PharmacistPillScript pillScript = Instantiate(pillPrefab, transform.position, transform.rotation).GetComponent<PharmacistPillScript>();
        pillScript.SetValue(attack/3 + 1);
        pillScript.SetTarget(target);
        pillScript.SetParentScript(this);

        // do not worry about this math, it is totally legit
        float xPos = 1.5f * (float) Math.Sin(-(Math.PI / 4.0) + (((pillsOut % maxAmountOfPills) + 2) * Math.PI / (float) (maxAmountOfPills+1)));
        float yPos = 1.5f * (float) Math.Cos(-(Math.PI / 4.0) + (((pillsOut % maxAmountOfPills) + 2) * Math.PI / (float) (maxAmountOfPills+1)));

        pillsOut++;

        pillScript.SetDirection(new Vector3(transform.position.x + xPos, transform.position.y + yPos, 0));

    }

    public void SetSelected(bool state){
        StartCoroutine(EnableSelection());
    }
}
