using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// handles all spawning logic for the zombie spawner found in campaign mode
public class CampaignZombieSpawner : ZombieSpawner
{
    
    // contains zombie type, time, and position information 
    private CampaignSpawnObject[] spawnObjects;

    // index of next zombie to spawn in spawnObjects
    private int nextSpawnIndex;

    // time of next zombie to spawn in spawnObjects
    private float nextSpawnTime;

    // the type, position, and time information for the next zombie
    private CampaignSpawnObject nextSpawnObject;

    // script that handles game over / wave completion logic
    // TODO make *this* script handle zombie count / spawn logic, 
    // TODO determine game over / wave complete here
    public CampaignLogicHandler campaignLogicScript;

    // Start is called before the first frame update
    void Start()
    {
        // TODO extend this to a chosen campaign wave, rather than preset
        // get the wave contents for the current wave number
        spawnObjects = GetWaveContents(SaveObject.maxCampaignWave-1);

        // first zombie index is 0
        nextSpawnIndex = 0;
        nextSpawnObject = spawnObjects[0];
        nextSpawnTime = nextSpawnObject.spawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        // if there are more zombies to spawn
        if(!campaignLogicScript.doneSpawning){

            // increment timer
            timeSinceWaveStart += Time.deltaTime;

            // if it is time to spawn the next zombie
            if(timeSinceWaveStart > nextSpawnTime){

                // if the last zombie is not being spawned
                if(nextSpawnIndex <= spawnObjects.Length - 1){
                    
                    // spawn the next zombie
                    Spawn();

                    // increment number of zombies that are alive
                    // TODO move this logic to this script, not campaign
                    campaignLogicScript.numAliveZombies++;

                    // if the last zombie was just spawned
                    if(++nextSpawnIndex == spawnObjects.Length){
                        
                        // we are now done spawning
                        campaignLogicScript.doneSpawning = true;
                        
                        // no more zombies to spawn
                        return;
                    }
                    // otherwise, set next spawn attributes
                    nextSpawnObject = spawnObjects[nextSpawnIndex];
                    nextSpawnTime = nextSpawnObject.spawnTime; 
                }
            }
        }
    }

    public override void Spawn(){
        // spawn                            next zombie's type,                 at the indicated location,              standing up straight
        Instantiate(zombieSelectionArray[nextSpawnObject.zombieIndex], spawningLocations[nextSpawnObject.positionIndex], Quaternion.identity);
    }


    // index - the wave number whose wave contents will be returned
    // This is going to be HUGE, but it follows a simple format
    // The returned object represents an array of zombie types, the position to spawn the zombie at, and the time to spawn said zombie, in that order.
    
    // Ex.              new CampaignSpawnObject(0, 1, 0.0f)
    // Ex. spawn the first zombie in this spawner's array of zombies to spawn,
    // Ex. at the second position in this spawner's array of positions,
    // Ex. at time = 0.0f.

    public CampaignSpawnObject[] GetWaveContents(int index){
        switch(index){
            case 0:
                return new CampaignSpawnObject[]{
                    new CampaignSpawnObject(0, 1, 0.0f),
                    new CampaignSpawnObject(0, 1, 2.0f),
                    new CampaignSpawnObject(0, 1, 4.0f),
                    new CampaignSpawnObject(0, 1, 5.0f)
                    };
            // secret mystery hell wave to punish your coding errors
            default:
                return new CampaignSpawnObject[]{
                    new CampaignSpawnObject(0, 0, 0.0f),
                    new CampaignSpawnObject(0, 1, 0.0f),
                    new CampaignSpawnObject(0, 2, 0.0f),
                    new CampaignSpawnObject(0, 3, 0.0f),
                    new CampaignSpawnObject(0, 0, 0.2f),
                    new CampaignSpawnObject(0, 1, 0.2f),
                    new CampaignSpawnObject(0, 2, 0.2f),
                    new CampaignSpawnObject(0, 3, 0.2f),
                    new CampaignSpawnObject(0, 0, 0.4f),
                    new CampaignSpawnObject(0, 1, 0.4f),
                    new CampaignSpawnObject(0, 2, 0.4f),
                    new CampaignSpawnObject(0, 3, 0.4f),
                    new CampaignSpawnObject(0, 0, 0.6f),
                    new CampaignSpawnObject(0, 1, 0.6f),
                    new CampaignSpawnObject(0, 2, 0.6f),
                    new CampaignSpawnObject(0, 3, 0.6f),
                    new CampaignSpawnObject(0, 0, 0.8f),
                    new CampaignSpawnObject(0, 1, 0.8f),
                    new CampaignSpawnObject(0, 2, 0.8f),
                    new CampaignSpawnObject(0, 3, 0.8f),
                    new CampaignSpawnObject(0, 0, 1.0f),
                    new CampaignSpawnObject(0, 1, 1.0f),
                    new CampaignSpawnObject(0, 2, 1.0f),
                    new CampaignSpawnObject(0, 3, 1.0f),
                    new CampaignSpawnObject(0, 0, 1.2f),
                    new CampaignSpawnObject(0, 1, 1.2f),
                    new CampaignSpawnObject(0, 2, 1.2f),
                    new CampaignSpawnObject(0, 3, 1.2f),
                    new CampaignSpawnObject(0, 0, 1.4f),
                    new CampaignSpawnObject(0, 1, 1.4f),
                    new CampaignSpawnObject(0, 2, 1.4f),
                    new CampaignSpawnObject(0, 3, 1.4f),
                    new CampaignSpawnObject(0, 0, 1.6f),
                    new CampaignSpawnObject(0, 1, 1.6f),
                    new CampaignSpawnObject(0, 2, 1.6f),
                    new CampaignSpawnObject(0, 3, 1.6f),
                    new CampaignSpawnObject(0, 0, 1.8f),
                    new CampaignSpawnObject(0, 1, 1.8f),
                    new CampaignSpawnObject(0, 2, 1.8f),
                    new CampaignSpawnObject(0, 3, 1.8f),
                    new CampaignSpawnObject(0, 0, 2.0f),
                    new CampaignSpawnObject(0, 1, 2.0f),
                    new CampaignSpawnObject(0, 2, 2.0f),
                    new CampaignSpawnObject(0, 3, 2.0f),
                    new CampaignSpawnObject(0, 0, 2.2f),
                    new CampaignSpawnObject(0, 1, 2.2f),
                    new CampaignSpawnObject(0, 2, 2.2f),
                    new CampaignSpawnObject(0, 3, 2.2f),
                    new CampaignSpawnObject(0, 0, 3.0f),
                    new CampaignSpawnObject(0, 1, 3.0f),
                    new CampaignSpawnObject(0, 2, 3.0f),
                    new CampaignSpawnObject(0, 3, 3.0f),
                    new CampaignSpawnObject(0, 0, 3.0f),
                    new CampaignSpawnObject(0, 1, 3.0f),
                    new CampaignSpawnObject(0, 2, 3.0f),
                    new CampaignSpawnObject(0, 3, 3.0f),
                    new CampaignSpawnObject(0, 0, 3.0f),
                    new CampaignSpawnObject(0, 1, 3.0f),
                    new CampaignSpawnObject(0, 2, 3.0f),
                    new CampaignSpawnObject(0, 3, 3.0f),
                    new CampaignSpawnObject(0, 0, 3.0f),
                    new CampaignSpawnObject(0, 1, 3.0f),
                    new CampaignSpawnObject(0, 2, 3.0f),
                    new CampaignSpawnObject(0, 3, 3.0f),
                    new CampaignSpawnObject(0, 0, 3.0f),
                    new CampaignSpawnObject(0, 1, 3.0f),
                    new CampaignSpawnObject(0, 2, 3.0f),
                    new CampaignSpawnObject(0, 3, 3.0f),
                    new CampaignSpawnObject(0, 0, 3.0f),
                    new CampaignSpawnObject(0, 1, 3.0f),
                    new CampaignSpawnObject(0, 2, 3.0f),
                    new CampaignSpawnObject(0, 3, 3.0f),
                    new CampaignSpawnObject(0, 0, 3.0f),
                    new CampaignSpawnObject(0, 1, 3.0f),
                    new CampaignSpawnObject(0, 2, 3.0f),
                    new CampaignSpawnObject(0, 3, 3.0f),
                    new CampaignSpawnObject(0, 0, 3.0f),
                    new CampaignSpawnObject(0, 1, 3.0f),
                    new CampaignSpawnObject(0, 2, 3.0f),
                    new CampaignSpawnObject(0, 3, 3.0f),
                    new CampaignSpawnObject(0, 0, 3.0f),
                    new CampaignSpawnObject(0, 1, 3.0f),
                    new CampaignSpawnObject(0, 2, 3.0f),
                    new CampaignSpawnObject(0, 3, 3.0f),
                    new CampaignSpawnObject(0, 0, 3.0f),
                    new CampaignSpawnObject(0, 1, 3.0f),
                    new CampaignSpawnObject(0, 2, 3.0f),
                    new CampaignSpawnObject(0, 3, 3.0f),
                    new CampaignSpawnObject(0, 0, 3.0f),
                    new CampaignSpawnObject(0, 1, 3.0f),
                    new CampaignSpawnObject(0, 2, 3.0f),
                    new CampaignSpawnObject(0, 3, 3.0f),
                    new CampaignSpawnObject(0, 0, 3.0f),
                    new CampaignSpawnObject(0, 1, 3.0f),
                    new CampaignSpawnObject(0, 2, 3.0f),
                    new CampaignSpawnObject(0, 3, 3.0f) 
                };
        }
    }
}
