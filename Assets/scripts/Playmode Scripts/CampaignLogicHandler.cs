using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampaignLogicHandler : MonoBehaviour
{
    // used to reveal only the appropriate towers for player to use
    public playMenuHandler menuScript;

    // used to determine... something TODO make number of alive zombies tracked here
    public ZombieSpawner spawnerScript;

    // number of zombies currently alive
    public int numAliveZombies;

    // whether or not the zombie spawner is done spawning
    // TODO transfer last two fields to zombie spawner script
    public bool doneSpawning;
    

    void Start(){
        // TODO determine what the scaling formula for tower unlocks in campaign mode is
        // allow only the appropriate employees to be used, more to be added as campaign progresses
        menuScript.availableEmployees = TrimAvailableEmployees(SaveObject.maxCampaignWave / 5 + 1);

        // zombies still need to spawn
        doneSpawning = false;
    }

    void Update(){
        // if no more zombies are alive and none need to be spawned, wave is complete
        if(doneSpawning && numAliveZombies == 0){
            Debug.Log("Completed wave!");
        }
    }

    // newIndexAmount - the upper index whose employee will be displayed. All higher-indices employees are locked
    private GameObject[] TrimAvailableEmployees(int newIndexAmount){

        // create subarray
        GameObject[] result = new GameObject[newIndexAmount];

        // add unlocked tower to sub array
        for(int i = 0; i < newIndexAmount; i++){
            result[i] = menuScript.availableEmployees[i];
        }

        // return subarray
        return result;
    }
}
