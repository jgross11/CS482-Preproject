﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverListener : MonoBehaviour
{
    
    public CampaignLogicHandler callback;
    public Collider2D myCollider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.tag == "zombie"){
            
            // call game over in campaign logic
            callback.GameOver();

            // disable collider temporarily so that more zombies don't trigger game over
            myCollider.enabled = false;
        }
    }
}
