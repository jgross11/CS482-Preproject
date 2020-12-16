using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProduceCarrotScript : MonoBehaviour
{
    // damage value 
    public int value;

    // movement speed 
    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        // fix rotation issue
        transform.Rotate(0, 0, 90);
    }

    // Update is called once per frame
    void Update()
    {
        // move carrot forward relative to its move speed
        transform.position += Vector3.right * Time.deltaTime * moveSpeed;
        transform.Rotate(0, 0, -1);
    }

    void OnTriggerEnter2D(Collider2D col){
        
        // if collided with zombie
        if(col.tag == "zombie"){
            // TODO implement zombie please
            
            // damage the zombie by this carrot's damage value
            Zombie zombieScript = col.GetComponent<Zombie>();
            
            zombieScript.Damage(value);
            
            /*
            // fix bug where zombie is destroyed before function is called
            // this may not be necessary
            
            if(zombieScript != null){
                zombieScript.Damage(value);
            }
            */

            // destroy the carrot
            Destroy(this.gameObject);
        }
    }

    // set this carrot's damage value
    public void SetValue(int val){
        value = val;
    }
}
