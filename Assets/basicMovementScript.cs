using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicMovementScript : MonoBehaviour
{

    // movement speed multiplier, keep 1 < movementSpeed < 1...
    public float movementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(Time.deltaTime*movementSpeed, 0, 0);
    }

    void OnCollisionEnter2D(Collision2D col){
        if(col.collider.tag == "death"){
            Destroy(this.gameObject);
        }
    }
}
