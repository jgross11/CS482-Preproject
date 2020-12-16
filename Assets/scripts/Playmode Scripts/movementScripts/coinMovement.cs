using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinMovement : MonoBehaviour
{
    public float movementSpeed;

    //random number generator
    System.Random rand = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(rand.Next(-6, 6), 2, -9);

    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(0, Time.deltaTime * movementSpeed, 0);

    }
}
