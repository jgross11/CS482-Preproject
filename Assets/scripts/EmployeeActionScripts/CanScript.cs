using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanScript : MonoBehaviour
{
    private int value;
    private Vector3 direction;
    // movement speed 
    public float moveSpeed;

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

    public void SetValue(int val)
    {
        value = val;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // if collided with zombie
        if (col.tag == "zombie")
        {
            // TODO implement zombie please

            // damage the zombie by this can's damage value
            // col.GetComponent<Zombie>().damage(value);

            // destroy the carrot
            Destroy(this.gameObject);
        }
    }
}
