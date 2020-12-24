using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bama : MonoBehaviour
{
    
    private bool isActive;
    public float movementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive){
            transform.position += Vector3.right * Time.deltaTime * movementSpeed;
        }
    }

    void OnTriggerEnter2D(Collider2D coll){
        if(coll.tag == "zombie"){
            isActive = true;
            // TODO update the damage amount accordingly
            coll.gameObject.GetComponent<Zombie>().Damage(20);
        }
    }
}
