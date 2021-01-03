using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MProjectile : MonoBehaviour
{
    // damage value 
    public int value;

    // movement speed 
    public float moveSpeed;

    // employee reference
    private Employee employeeScript;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // move projectile forward relative to its move speed
        transform.position += Vector3.right * Time.deltaTime * moveSpeed;
        transform.Rotate(0, 0, -2);
    }

    void OnTriggerEnter2D(Collider2D col){
        
        // if collided with zombie
        if(col.tag == "zombie"){
            // damage the zombie by this projectile's damage value
            col.GetComponent<Zombie>().Damage(value);

            // give experience to agent tower
            employeeScript.AddExperience(value);

            // destroy the projectile
            Destroy(this.gameObject);
        }
    }

    // set this projectile's damage value and tower script reference
    public void SetValues(Employee emp, int val){
        value = val;
        employeeScript = emp;
    }
}
