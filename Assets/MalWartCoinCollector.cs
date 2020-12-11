using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MalWartCoinCollector : MonoBehaviour{

    //Counter to hold how many Coins "collected"
    public int MalWartCoinNum = 0;
    public GameObject coin;

    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        
    }

    void OnMouseDown(){
        //printing out the number of coins collected based on every time you click the object
        Destroy(this.coin);
        Debug.Log(MalWartCoinNum++);
    }
}
