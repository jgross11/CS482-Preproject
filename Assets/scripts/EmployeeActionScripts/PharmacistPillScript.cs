using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PharmacistPillScript : MonoBehaviour
{
    
    public int value;
    public GameObject target;
    public bool isActive;
    private Vector3 randDirection;
    public float maxMoveTime = 1.0f;
    private float currentLifeTime = 0.0f;
    public float lifeTime = 7.0f;
    public float moveSpeed = 15.0f;
    public PharmacistActionScript parentScript;
    
    // Start is called before the first frame update
    void Start()
    {
        // randDirection = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0);
    }

    public void SetDirection(Vector3 dir){
        randDirection = dir;
    }

    // Update is called once per frame
    void Update()
    {
        currentLifeTime += Time.deltaTime;
        if(isActive){
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
            transform.Rotate(Vector3.forward);
        }
        else if(currentLifeTime < maxMoveTime){
            transform.position = Vector2.MoveTowards(transform.position, randDirection, moveSpeed * Time.deltaTime);
        }
        else if(currentLifeTime >= lifeTime){
            Destroy(this.gameObject);
        }
    }

    public void SetValue(int val){
        value = val;
    }

    public void SetTarget(GameObject newTarget){
        target = newTarget;
    }

    public void SetParentScript(PharmacistActionScript ps){
        parentScript = ps;
    }

    void OnMouseDown(){
        target = parentScript.GetTarget();
        if(target != null){
            isActive = true;
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        if(isActive && target != null && col == target.GetComponent<Collider2D>()){
            target.GetComponent<Employee>().Heal(value);
            Destroy(this.gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D col){
        if(isActive && target != null && col == target.GetComponent<Collider2D>()){
            target.GetComponent<Employee>().Heal(value);
            Destroy(this.gameObject);
        }
    }
}
