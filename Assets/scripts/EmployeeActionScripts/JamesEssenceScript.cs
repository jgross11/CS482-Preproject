using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JamesEssenceScript : MonoBehaviour
{
    private int value;
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
        // add essence value to essence count
        SaveObject.numEssence += value;

        // add experience to james tower
        employeeScript.AddExperience(value);   

        // destroy after a small period of time
        Destroy(this.gameObject, 0.01f);
    }
}
