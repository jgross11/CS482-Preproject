using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// handles all spawning logic for the zombie spawner found in campaign mode
public class CampaignZombieSpawner : ZombieSpawner
{
    // zombie type constants
    private const int BASIC_INDEX = 0;
    private const int TANK_INDEX = 1;
    private const int HEALER_INDEX = 2;
    private const int MINI_BOSS_INDEX = 3;
    private const int BOSS_INDEX = 4;

    // spawn position constants
    private const int TOP = 0;
    private const int MIDDLE_TOP = 1;
    private const int MIDDLE_BOTTOM = 2;
    private const int BOTTOM = 3;

    // contains zombie type, time, and position information 
    private CampaignSpawnObject[] spawnObjects;

    // index of next zombie to spawn in spawnObjects
    public int nextSpawnIndex;

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
    
    // Ex.              new CampaignSpawnObject(BASIC_INDEX, MIDDLE_TOP, 0.0f)
    // Ex. spawn the basic (index 0) zombie in this spawner's array of zombies to spawn,
    // Ex. at the middle-top (index 1) position in this spawner's array of positions,
    // Ex. at time = 0.0.

    public CampaignSpawnObject[] GetWaveContents(int index){
        CampaignSpawnObject[] result;
        switch(index){
            case 1:
                result = new CampaignSpawnObject[16];
                for(int i = 0; i < 16; i+=4){
                    result[i] = new CampaignSpawnObject(BASIC_INDEX, TOP, 0.0f + (3 * i));
                    result[i+1] = new CampaignSpawnObject(BASIC_INDEX, MIDDLE_TOP, 3.0f + (3 * i));
                    result[i+2] = new CampaignSpawnObject(BASIC_INDEX, MIDDLE_BOTTOM, 6.0f + (3 * i));
                    result[i+3] = new CampaignSpawnObject(BASIC_INDEX, BOTTOM, 9.0f + (3 * i));
                }
                return result;
            case 2:
                result = new CampaignSpawnObject[32];
                for(int i = 0; i < 32; i+=4){
                    result[i] = new CampaignSpawnObject(BASIC_INDEX, TOP, 0.0f + (2.5f * i));
                    result[i+1] = new CampaignSpawnObject(BASIC_INDEX, MIDDLE_TOP, 2.5f + (2.5f * i));
                    result[i+2] = new CampaignSpawnObject(BASIC_INDEX, MIDDLE_BOTTOM, 5.0f + (2.5f * i));
                    result[i+3] = new CampaignSpawnObject(BASIC_INDEX, BOTTOM, 7.5f + (2.5f * i));
                }
                return result;
            case 3:
                result = new CampaignSpawnObject[50];
                for(int i = 0; i < 20; i += 4){
                    result[i] = new CampaignSpawnObject(BASIC_INDEX, TOP, 0.0f + (2.0f * i));
                    result[i+1] = new CampaignSpawnObject(BASIC_INDEX, MIDDLE_TOP, 2.0f + (2.0f * i));
                    result[i+2] = new CampaignSpawnObject(BASIC_INDEX, MIDDLE_BOTTOM, 4.0f + (2.0f * i));
                    result[i+3] = new CampaignSpawnObject(BASIC_INDEX, BOTTOM, 6.0f + (2.0f * i));
                }
                for(int i = 0; i < 20; i += 4){
                    result[20+i] = new CampaignSpawnObject(BASIC_INDEX, TOP, 34.0f + (1.5f * i));
                    result[20+i+1] = new CampaignSpawnObject(BASIC_INDEX, MIDDLE_TOP, 36.0f + (1.5f * i));
                    result[20+i+2] = new CampaignSpawnObject(BASIC_INDEX, MIDDLE_BOTTOM, 38.0f + (1.5f * i));
                    result[20+i+3] = new CampaignSpawnObject(BASIC_INDEX, BOTTOM, 40.0f + (1.5f * i));
                }
                for(int i = 0; i < 8; i += 4){
                    result[40+i] = new CampaignSpawnObject(BASIC_INDEX, TOP, 65.0f + (1.0f * i));
                    result[40+i+1] = new CampaignSpawnObject(BASIC_INDEX, MIDDLE_TOP, 66.0f + (1.0f * i));
                    result[40+i+2] = new CampaignSpawnObject(BASIC_INDEX, MIDDLE_BOTTOM, 67.0f + (1.0f * i));
                    result[40+i+3] = new CampaignSpawnObject(BASIC_INDEX, BOTTOM, 68.0f + (1.0f * i));
                } 
                result[48] = new CampaignSpawnObject(BASIC_INDEX, MIDDLE_BOTTOM, 105f);
                result[49] = new CampaignSpawnObject(BASIC_INDEX, MIDDLE_TOP, 105f);
                return result; 
            case 4:
                return new CampaignSpawnObject[]{
                    new CampaignSpawnObject(BASIC_INDEX, MIDDLE_TOP, 0.0f),
                    new CampaignSpawnObject(BASIC_INDEX, MIDDLE_BOTTOM, 0.0f),
                    new CampaignSpawnObject(BASIC_INDEX, BOTTOM, 0.0f),
                    new CampaignSpawnObject(BASIC_INDEX, MIDDLE_TOP, 4.0f),
                    new CampaignSpawnObject(BASIC_INDEX, MIDDLE_BOTTOM, 4.0f),
                    new CampaignSpawnObject(BASIC_INDEX, BOTTOM, 4.0f),
                    new CampaignSpawnObject(BASIC_INDEX, MIDDLE_TOP, 7.0f),
                    new CampaignSpawnObject(BASIC_INDEX, MIDDLE_BOTTOM, 7.0f),
                    new CampaignSpawnObject(BASIC_INDEX, BOTTOM, 7.0f),
                    new CampaignSpawnObject(BASIC_INDEX, MIDDLE_TOP, 10.0f),
                    new CampaignSpawnObject(BASIC_INDEX, MIDDLE_BOTTOM, 10.0f),
                    new CampaignSpawnObject(BASIC_INDEX, BOTTOM, 10.0f),
                    new CampaignSpawnObject(BASIC_INDEX, MIDDLE_TOP, 12.0f),
                    new CampaignSpawnObject(BASIC_INDEX, MIDDLE_BOTTOM, 12.0f),
                    new CampaignSpawnObject(BASIC_INDEX, BOTTOM, 12.0f),
                    new CampaignSpawnObject(BASIC_INDEX, MIDDLE_TOP, 14.0f),
                    new CampaignSpawnObject(BASIC_INDEX, MIDDLE_BOTTOM, 14.0f),
                    new CampaignSpawnObject(BASIC_INDEX, BOTTOM, 14.0f)
                };
            case 5:
                return new CampaignSpawnObject[]{
                    new CampaignSpawnObject(MIDDLE_TOP, BOTTOM, 0.0f),
                    new CampaignSpawnObject(BASIC_INDEX, TOP, 0.0f),
                    new CampaignSpawnObject(BASIC_INDEX, BOTTOM, 2.0f),
                    new CampaignSpawnObject(BASIC_INDEX, TOP, 4.0f),
                    new CampaignSpawnObject(MIDDLE_TOP, BOTTOM, 5.0f),
                    new CampaignSpawnObject(MIDDLE_TOP, TOP, 5.0f),
                    new CampaignSpawnObject(BASIC_INDEX, MIDDLE_TOP, 6.0f),
                    new CampaignSpawnObject(MIDDLE_TOP, MIDDLE_BOTTOM, 6.0f),
                    new CampaignSpawnObject(MIDDLE_TOP, MIDDLE_BOTTOM, 8.0f),
                    new CampaignSpawnObject(MIDDLE_TOP, MIDDLE_BOTTOM, 9.0f),
                    new CampaignSpawnObject(MIDDLE_TOP, MIDDLE_TOP, 11.0f),
                    new CampaignSpawnObject(BASIC_INDEX, BOTTOM, 11.0f)
                };
            // secret mystery hell wave to punish your coding errors
            default:
                return new CampaignSpawnObject[]{
                    new CampaignSpawnObject(BOSS_INDEX, TOP, 0.0f),
                    new CampaignSpawnObject(BOSS_INDEX, MIDDLE_TOP, 0.0f),
                    new CampaignSpawnObject(BOSS_INDEX, MIDDLE_BOTTOM, 0.0f),
                    new CampaignSpawnObject(BOSS_INDEX, BOTTOM, 0.0f),
                    new CampaignSpawnObject(BOSS_INDEX, TOP, 0.2f),
                    new CampaignSpawnObject(BOSS_INDEX, MIDDLE_TOP, 0.2f),
                    new CampaignSpawnObject(BOSS_INDEX, MIDDLE_BOTTOM, 0.2f),
                    new CampaignSpawnObject(BOSS_INDEX, BOTTOM, 0.2f),
                    new CampaignSpawnObject(BOSS_INDEX, TOP, 0.4f),
                    new CampaignSpawnObject(BOSS_INDEX, MIDDLE_TOP, 0.4f),
                    new CampaignSpawnObject(BOSS_INDEX, MIDDLE_BOTTOM, 0.4f),
                    new CampaignSpawnObject(BOSS_INDEX, BOTTOM, 0.4f),
                    new CampaignSpawnObject(BOSS_INDEX, TOP, 0.6f),
                    new CampaignSpawnObject(BOSS_INDEX, MIDDLE_TOP, 0.6f),
                    new CampaignSpawnObject(BOSS_INDEX, MIDDLE_BOTTOM, 0.6f),
                    new CampaignSpawnObject(BOSS_INDEX, BOTTOM, 0.6f),
                    new CampaignSpawnObject(BOSS_INDEX, TOP, 0.8f),
                    new CampaignSpawnObject(BOSS_INDEX, MIDDLE_TOP, 0.8f),
                    new CampaignSpawnObject(BOSS_INDEX, MIDDLE_BOTTOM, 0.8f),
                    new CampaignSpawnObject(BOSS_INDEX, BOTTOM, 0.8f),
                    new CampaignSpawnObject(BOSS_INDEX, TOP, 1.0f),
                    new CampaignSpawnObject(BOSS_INDEX, MIDDLE_TOP, 1.0f),
                    new CampaignSpawnObject(BOSS_INDEX, MIDDLE_BOTTOM, 1.0f),
                    new CampaignSpawnObject(BOSS_INDEX, BOTTOM, 1.0f),
                    new CampaignSpawnObject(BOSS_INDEX, TOP, 1.2f),
                    new CampaignSpawnObject(BOSS_INDEX, MIDDLE_TOP, 1.2f),
                    new CampaignSpawnObject(BOSS_INDEX, MIDDLE_BOTTOM, 1.2f),
                    new CampaignSpawnObject(BOSS_INDEX, BOTTOM, 1.2f),
                    new CampaignSpawnObject(BOSS_INDEX, TOP, 1.4f),
                    new CampaignSpawnObject(BOSS_INDEX, MIDDLE_TOP, 1.4f),
                    new CampaignSpawnObject(BOSS_INDEX, MIDDLE_BOTTOM, 1.4f),
                    new CampaignSpawnObject(BOSS_INDEX, BOTTOM, 1.4f),
                    new CampaignSpawnObject(BOSS_INDEX, TOP, 1.6f),
                    new CampaignSpawnObject(BOSS_INDEX, MIDDLE_TOP, 1.6f),
                    new CampaignSpawnObject(BOSS_INDEX, MIDDLE_BOTTOM, 1.6f),
                    new CampaignSpawnObject(BOSS_INDEX, BOTTOM, 1.6f),
                    new CampaignSpawnObject(BOSS_INDEX, TOP, 1.8f),
                    new CampaignSpawnObject(BOSS_INDEX, MIDDLE_TOP, 1.8f),
                    new CampaignSpawnObject(BOSS_INDEX, MIDDLE_BOTTOM, 1.8f),
                    new CampaignSpawnObject(BOSS_INDEX, BOTTOM, 1.8f),
                    new CampaignSpawnObject(BOSS_INDEX, TOP, 2.0f),
                    new CampaignSpawnObject(BOSS_INDEX, MIDDLE_TOP, 2.0f),
                    new CampaignSpawnObject(BOSS_INDEX, MIDDLE_BOTTOM, 2.0f),
                    new CampaignSpawnObject(BOSS_INDEX, BOTTOM, 2.0f),
                    new CampaignSpawnObject(BOSS_INDEX, TOP, 2.2f),
                    new CampaignSpawnObject(BOSS_INDEX, MIDDLE_TOP, 2.2f),
                    new CampaignSpawnObject(BOSS_INDEX, MIDDLE_BOTTOM, 2.2f),
                    new CampaignSpawnObject(BOSS_INDEX, BOTTOM, 2.2f),
                    new CampaignSpawnObject(BOSS_INDEX, TOP, 3.0f),
                    new CampaignSpawnObject(BOSS_INDEX, MIDDLE_TOP, 3.0f),
                    new CampaignSpawnObject(BOSS_INDEX, MIDDLE_BOTTOM, 3.0f),
                    new CampaignSpawnObject(BOSS_INDEX, BOTTOM, 3.0f),
                    new CampaignSpawnObject(BOSS_INDEX, TOP, 3.0f),
                    new CampaignSpawnObject(BOSS_INDEX, MIDDLE_TOP, 3.0f),
                    new CampaignSpawnObject(BOSS_INDEX, MIDDLE_BOTTOM, 3.0f),
                    new CampaignSpawnObject(BOSS_INDEX, BOTTOM, 3.0f),
                    new CampaignSpawnObject(BOSS_INDEX, TOP, 3.0f),
                    new CampaignSpawnObject(BOSS_INDEX, MIDDLE_TOP, 3.0f),
                    new CampaignSpawnObject(BOSS_INDEX, MIDDLE_BOTTOM, 3.0f),
                    new CampaignSpawnObject(BOSS_INDEX, BOTTOM, 3.0f),
                    new CampaignSpawnObject(BOSS_INDEX, TOP, 3.0f),
                    new CampaignSpawnObject(BOSS_INDEX, MIDDLE_TOP, 3.0f),
                    new CampaignSpawnObject(BOSS_INDEX, MIDDLE_BOTTOM, 3.0f),
                    new CampaignSpawnObject(BOSS_INDEX, BOTTOM, 3.0f),
                    new CampaignSpawnObject(BOSS_INDEX, TOP, 3.0f),
                    new CampaignSpawnObject(BOSS_INDEX, MIDDLE_TOP, 3.0f),
                    new CampaignSpawnObject(BOSS_INDEX, MIDDLE_BOTTOM, 3.0f),
                    new CampaignSpawnObject(BOSS_INDEX, BOTTOM, 3.0f),
                    new CampaignSpawnObject(BOSS_INDEX, TOP, 3.0f),
                    new CampaignSpawnObject(BOSS_INDEX, MIDDLE_TOP, 3.0f),
                    new CampaignSpawnObject(BOSS_INDEX, MIDDLE_BOTTOM, 3.0f),
                    new CampaignSpawnObject(BOSS_INDEX, BOTTOM, 3.0f),
                    new CampaignSpawnObject(BOSS_INDEX, TOP, 3.0f),
                    new CampaignSpawnObject(BOSS_INDEX, MIDDLE_TOP, 3.0f),
                    new CampaignSpawnObject(BOSS_INDEX, MIDDLE_BOTTOM, 3.0f),
                    new CampaignSpawnObject(BOSS_INDEX, BOTTOM, 3.0f),
                    new CampaignSpawnObject(BOSS_INDEX, TOP, 3.0f),
                    new CampaignSpawnObject(BOSS_INDEX, MIDDLE_TOP, 3.0f),
                    new CampaignSpawnObject(BOSS_INDEX, MIDDLE_BOTTOM, 3.0f),
                    new CampaignSpawnObject(BOSS_INDEX, BOTTOM, 3.0f),
                    new CampaignSpawnObject(BOSS_INDEX, TOP, 3.0f),
                    new CampaignSpawnObject(BOSS_INDEX, MIDDLE_TOP, 3.0f),
                    new CampaignSpawnObject(BOSS_INDEX, MIDDLE_BOTTOM, 3.0f),
                    new CampaignSpawnObject(BOSS_INDEX, BOTTOM, 3.0f),
                    new CampaignSpawnObject(BOSS_INDEX, TOP, 3.0f),
                    new CampaignSpawnObject(BOSS_INDEX, MIDDLE_TOP, 3.0f),
                    new CampaignSpawnObject(BOSS_INDEX, MIDDLE_BOTTOM, 3.0f),
                    new CampaignSpawnObject(BOSS_INDEX, BOTTOM, 3.0f),
                    new CampaignSpawnObject(BOSS_INDEX, TOP, 3.0f),
                    new CampaignSpawnObject(BOSS_INDEX, MIDDLE_TOP, 3.0f),
                    new CampaignSpawnObject(BOSS_INDEX, MIDDLE_BOTTOM, 3.0f),
                    new CampaignSpawnObject(BOSS_INDEX, BOTTOM, 3.0f),
                    new CampaignSpawnObject(BOSS_INDEX, TOP, 3.0f),
                    new CampaignSpawnObject(BOSS_INDEX, MIDDLE_TOP, 3.0f),
                    new CampaignSpawnObject(BOSS_INDEX, MIDDLE_BOTTOM, 3.0f),
                    new CampaignSpawnObject(BOSS_INDEX, TOP, 10.0f),
                    new CampaignSpawnObject(BOSS_INDEX, MIDDLE_TOP, 10.0f),
                    new CampaignSpawnObject(BOSS_INDEX, MIDDLE_BOTTOM, 10.0f),
                    new CampaignSpawnObject(BOSS_INDEX, BOTTOM, 10.0f)
                };
        }
    }
}
