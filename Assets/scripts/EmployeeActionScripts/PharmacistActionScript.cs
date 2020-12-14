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
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            RaycastHit2D rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
            if (!isSelecting && rayHit.collider == parentCollider){
                isSelecting = true;
                Debug.Log("now selecting target");
            } else if(isSelecting && (rayHit.collider == null || rayHit.collider.tag != "employee")){
                isSelecting = false;
                Debug.Log("canceling selection 1");
            } else if(rayHit.collider != null && rayHit.collider.tag == "employee" && isSelecting){
                if(target != null){
                    targetSpriteRenderer.color = Color.white;
                }
                target = rayHit.collider.gameObject;
                targetSpriteRenderer = target.GetComponent<SpriteRenderer>();
                Debug.Log("selected target");
                isSelecting = false;
            }
        }


        // TODO make this more apparent
        if(isSelecting){
            parentSpriteRenderer.color = Color.grey;
            if(target != null){
                targetSpriteRenderer.color = Color.grey;
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
        pillScript.setValue(attack/3 + 1);
        pillScript.setTarget(target);
        pillScript.setParentScript(this);
    }
}
