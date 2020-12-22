using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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

    // number of zombies currently alive
    public int numAliveZombies;

    // text that displays when next zombie is spawning
    public Text nextZombieText;

    // whether or not the zombie spawner is done spawning
    public bool doneSpawning;


    // Start is called before the first frame update
    void Start()
    {
        LoadNextWave();
    }

    public void LoadNextWave(){

        // wipe all currently existing zombies, if any exist
        Transform[] children = transform.GetComponentsInChildren<Transform>();
        
        // ensure accidental end-of-wave isn't achieved
        numAliveZombies = -1;
        foreach(Transform child in children)
        {
            // apparently, the parent is itself a child
            // a philosophical statement if ever there was one
            // but we don't want to destroy the parent. 
            if(child == transform){
                continue;
            }
            Destroy(child.gameObject);
        }

        // TODO extend this to a chosen campaign wave, rather than preset
        // get the wave contents for the current wave number
        spawnObjects = GetWaveContents(SaveObject.maxCampaignWave+1);

        // first zombie index is 0
        nextSpawnIndex = 0;
        nextSpawnObject = spawnObjects[0];
        nextSpawnTime = nextSpawnObject.spawnTime;

        // wave just started, so set time to -5 to allow defense building
        timeSinceWaveStart = -5;

        numAliveZombies = 0;

        // start of wave -> move zombies to spawn
        doneSpawning = false;
    }

    // Update is called once per frame
    void Update()
    {

        // if there are more zombies to spawn
        if(!doneSpawning){

            // increment timer
            timeSinceWaveStart += Time.deltaTime;

            // if it is time to spawn the next zombie
            if(timeSinceWaveStart > nextSpawnTime){

                // if the last zombie is not being spawned
                if(nextSpawnIndex <= spawnObjects.Length - 1){
                    
                    // spawn the next zombie
                    Spawn();

                    // increment number of zombies that are alive
                    numAliveZombies++;

                    // if the last zombie was just spawned
                    if(++nextSpawnIndex == spawnObjects.Length){
                        
                        // we are now done spawning
                        doneSpawning = true;
                        
                        // no more zombies to spawn
                        return;
                    }
                    // otherwise, set next spawn attributes
                    nextSpawnObject = spawnObjects[nextSpawnIndex];
                    nextSpawnTime = nextSpawnObject.spawnTime; 
                }
            }
        } else if(numAliveZombies == 0){
            campaignLogicScript.EndCurrentWave();
        }

        // update next zombie text
        nextZombieText.text = doneSpawning ? "Zombies remaining: " + (numAliveZombies < 0 ? 0 : numAliveZombies) : "Next zombie in " + (nextSpawnTime - timeSinceWaveStart < 0 ? 0.0f : Math.Round(nextSpawnTime - timeSinceWaveStart, 1)) + " sec";
    }

    public override void Spawn(){
        // spawn                                            next zombie's type,                 at the indicated location,              standing up straight, as a child of this object
        GameObject go = Instantiate(zombieSelectionArray[nextSpawnObject.zombieIndex], spawningLocations[nextSpawnObject.positionIndex], Quaternion.identity, this.transform);
        
        // attach and obtain reference to destroy listener on spawned zombie
        CampaignZombieDestroyListener listener = go.AddComponent(typeof(CampaignZombieDestroyListener)) as CampaignZombieDestroyListener;

        // set listener's spawner to this one, in order to ensure numAliveZombies decreases when zombie is destroyed.
        listener.SetSpawner(this);
    }


    // index - the wave number whose wave contents will be returned
    // This is going to be HUGE, but it follows a simple format
    // The returned object represents an array of zombie types, the position to spawn the zombie at, and the time to spawn said zombie, in that order.
    
    // Ex.              new CampaignSpawnObject(0, 1, 0.0f)
    // Ex. spawn the first (index 0) zombie in this spawner's array of zombies to spawn,
    // Ex. at the second (index 1) position in this spawner's array of positions,
    // Ex. at time = 0.0.

    public CampaignSpawnObject[] GetWaveContents(int index){
        switch(index){
            case 1:
                return new CampaignSpawnObject[]{
                    new CampaignSpawnObject(0, 1, 0.0f),
                    new CampaignSpawnObject(0, 1, 3.0f),
                    new CampaignSpawnObject(0, 1, 6.0f),
                    new CampaignSpawnObject(0, 1, 9.0f)
                    };
            case 2:
                return new CampaignSpawnObject[]{
                    new CampaignSpawnObject(0, 1, 0.0f),
                    new CampaignSpawnObject(0, 1, 2.0f),
                    new CampaignSpawnObject(0, 1, 4.0f),
                    new CampaignSpawnObject(0, 1, 6.0f),
                    new CampaignSpawnObject(0, 2, 6.0f),
                    new CampaignSpawnObject(0, 2, 8.0f),
                    new CampaignSpawnObject(0, 2, 10.0f),
                    new CampaignSpawnObject(0, 2, 12.0f)
                    };
            case 3:
                return new CampaignSpawnObject[]{
                    new CampaignSpawnObject(0, 1, 0.0f),
                    new CampaignSpawnObject(0, 1, 1.5f),
                    new CampaignSpawnObject(0, 1, 3.0f),
                    new CampaignSpawnObject(0, 1, 4.5f),
                    new CampaignSpawnObject(0, 2, 4.5f),
                    new CampaignSpawnObject(0, 1, 6.0f),
                    new CampaignSpawnObject(0, 2, 6.0f),
                    new CampaignSpawnObject(0, 1, 7.0f),
                    new CampaignSpawnObject(0, 2, 7.0f),
                    new CampaignSpawnObject(0, 1, 8.0f),
                    new CampaignSpawnObject(0, 2, 8.0f)
                    };
            case 4:
                return new CampaignSpawnObject[]{
                    new CampaignSpawnObject(0, 1, 0.0f),
                    new CampaignSpawnObject(0, 2, 0.0f),
                    new CampaignSpawnObject(0, 3, 0.0f),
                    new CampaignSpawnObject(0, 1, 4.0f),
                    new CampaignSpawnObject(0, 2, 4.0f),
                    new CampaignSpawnObject(0, 3, 4.0f),
                    new CampaignSpawnObject(0, 1, 7.0f),
                    new CampaignSpawnObject(0, 2, 7.0f),
                    new CampaignSpawnObject(0, 3, 7.0f),
                    new CampaignSpawnObject(0, 1, 10.0f),
                    new CampaignSpawnObject(0, 2, 10.0f),
                    new CampaignSpawnObject(0, 3, 10.0f),
                    new CampaignSpawnObject(0, 1, 12.0f),
                    new CampaignSpawnObject(0, 2, 12.0f),
                    new CampaignSpawnObject(0, 3, 12.0f),
                    new CampaignSpawnObject(0, 1, 14.0f),
                    new CampaignSpawnObject(0, 2, 14.0f),
                    new CampaignSpawnObject(0, 3, 14.0f)
                };
            case 5:
                return new CampaignSpawnObject[]{
                    new CampaignSpawnObject(1, 3, 0.0f),
                    new CampaignSpawnObject(0, 0, 0.0f),
                    new CampaignSpawnObject(0, 3, 2.0f),
                    new CampaignSpawnObject(0, 0, 4.0f),
                    new CampaignSpawnObject(1, 3, 5.0f),
                    new CampaignSpawnObject(1, 0, 5.0f),
                    new CampaignSpawnObject(0, 1, 6.0f),
                    new CampaignSpawnObject(1, 2, 6.0f),
                    new CampaignSpawnObject(1, 2, 8.0f),
                    new CampaignSpawnObject(1, 2, 9.0f),
                    new CampaignSpawnObject(1, 1, 11.0f),
                    new CampaignSpawnObject(0, 3, 11.0f)
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
                    new CampaignSpawnObject(0, 0, 10.0f),
                    new CampaignSpawnObject(0, 1, 10.0f),
                    new CampaignSpawnObject(0, 2, 10.0f),
                    new CampaignSpawnObject(0, 3, 10.0f)
                };
        }
    }
}
