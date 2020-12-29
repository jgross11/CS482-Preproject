using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanScript : MonoBehaviour
{
    public int value;
    private Vector3 direction;
    // movement speed 
    public float moveSpeed;

    // employee reference
    private Employee employeeScript;

    // Start is called before the first frame update

    void Start()
    {
        direction = new Vector3(1, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * Time.deltaTime * moveSpeed;


    }

    public void SetValues(Employee emp, int val)
    {
        value = val;
        employeeScript = emp;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // if collided with zombie
        if (col.tag == "zombie")
        {
            // TODO implement zombie please

            // damage the zombie by this can's damage value
            col.GetComponent<Zombie>().Damage(value);

            // tower gets experience
            employeeScript.AddExperience(value);

            /*
            // fix bug where zombie is destroyed before function is called
            // this may not be necessary
            
            if(zombieScript != null){
                zombieScript.Damage(value);
            }
            */
            // destroy the can
            Destroy(this.gameObject);
        }
    }
}
