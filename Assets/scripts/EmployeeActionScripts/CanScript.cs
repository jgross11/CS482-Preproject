using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanScript : MonoBehaviour
{
    private int value;
    private Vector3 direction;

    // Start is called before the first frame update

    void Start()
    {
        direction = new Vector3(1, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * 0.003f;


    }

    public void setValue(int val)
    {
        value = val;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(this.gameObject);
    }
}
