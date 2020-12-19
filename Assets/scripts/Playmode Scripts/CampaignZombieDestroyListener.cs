using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// this script is attached upon the creation of a zombie in campaign mode
// in order to accurately track the number of zombies still alive in that wave
public class CampaignZombieDestroyListener : MonoBehaviour
{
    // spawner whose numAliveZombies will be decremented upon this being destroyed
    private CampaignZombieSpawner spawnerScript;

    void OnDestroy(){
        
        // if reference is valid
        if(spawnerScript != null){
            
            // decrement the number of alive zombies from this spawner
            spawnerScript.numAliveZombies--;
        }       
    }

    public void SetSpawner(CampaignZombieSpawner s){
        spawnerScript = s;
    }
}
