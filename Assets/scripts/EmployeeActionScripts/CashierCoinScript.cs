using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashierCoinScript : MonoBehaviour
{

    private int value;
    // Start is called before the first frame update

    private Vector3 randDirection;
    private float currentLifeTime = 0.0f;
    public float maxMoveTime = 1.0f;
    public float lifeTime = 7.0f;
    private Employee employeeScript;

    void Start()
    {
        randDirection = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0);
    }

    // Update is called once per frame
    void Update()
    {
        currentLifeTime += Time.deltaTime;
        if(currentLifeTime < maxMoveTime){
            transform.position += randDirection * 0.003f;
        }
        if(currentLifeTime >= lifeTime){
            Destroy(this.gameObject);
        }
    }

    public void SetValues(Employee emp, int val){
        value = val;
        employeeScript = emp;
    }

    void OnMouseDown(){
        // absolutely awful
        // add coin value to coin count
        GameObject.Find("tower-menu-background").GetComponent<playMenuHandler>().AddCoins(value);

        // add experience to cashier tower
        employeeScript.AddExperience(value);   

        // destroy coin after a short period of time
        // cannot destroy immediately as, for example, if a pharmacist
        // is selected, destroy immediately means the pharmacist is deselected
        // as this object is destroyed before the pharmacist's ray can collide
        // with this object's collider to preserve selection. 
        Destroy(this.gameObject, 0.01f);
    }

}
