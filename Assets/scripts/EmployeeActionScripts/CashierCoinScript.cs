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
    public float lifeTime = 5.0f;

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

    public void setValue(int val){
        value = val;
    }

    void OnMouseDown(){
        // absolutely awful
        // add coin value to coin count
        GameObject.Find("tower-menu-background").GetComponent<playMenuHandler>().AddCoins(value);

        // destroy coin 
        Destroy(this.gameObject);
    }
}
