using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MalWartCoinCollector : MonoBehaviour{

    //the coin object
    public GameObject coin;

    public playMenuHandler menu;

    // Start is called before the first frame update
    void Start(){

       
    }

    // Update is called once per frame
    void Update(){
        
    }

    void OnMouseDown(){

        menu.AddCoins(1);

        //destroying this instance of the coin upon click
        Destroy(this.coin);

    }
}
