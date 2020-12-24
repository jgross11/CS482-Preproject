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
        switch(index){
            case 1:
                return new CampaignSpawnObject[]{
						new CampaignSpawnObject(0, 0, 0.5453418f),
						new CampaignSpawnObject(0, 0, 3.883249f),
						new CampaignSpawnObject(0, 1, 6.978427f),
						new CampaignSpawnObject(0, 1, 10.27909f),
						new CampaignSpawnObject(0, 2, 13.61001f),
						new CampaignSpawnObject(0, 2, 16.38238f),
						new CampaignSpawnObject(0, 0, 19.87154f),
						new CampaignSpawnObject(0, 1, 20.39416f),
						new CampaignSpawnObject(0, 2, 20.95358f),
						new CampaignSpawnObject(0, 3, 21.73433f),
						new CampaignSpawnObject(0, 3, 22.9944f),
						new CampaignSpawnObject(0, 3, 24.19734f)
				};
            case 3:
                return new CampaignSpawnObject[]{
						new CampaignSpawnObject(0, 0, 0.4219258f),
						new CampaignSpawnObject(0, 0, 0.979355f),
						new CampaignSpawnObject(0, 0, 1.556867f),
						new CampaignSpawnObject(0, 0, 9.072618f),
						new CampaignSpawnObject(0, 0, 9.582633f),
						new CampaignSpawnObject(0, 0, 10.27635f),
						new CampaignSpawnObject(0, 1, 11.55747f),
						new CampaignSpawnObject(0, 1, 13.09451f),
						new CampaignSpawnObject(0, 1, 14.46344f),
						new CampaignSpawnObject(0, 1, 15.95345f),
						new CampaignSpawnObject(0, 1, 17.55553f),
						new CampaignSpawnObject(0, 0, 18.16442f),
						new CampaignSpawnObject(0, 0, 18.63675f),
						new CampaignSpawnObject(0, 0, 19.17589f),
						new CampaignSpawnObject(0, 1, 19.77448f),
						new CampaignSpawnObject(0, 1, 21.04341f),
						new CampaignSpawnObject(0, 1, 22.28817f),
						new CampaignSpawnObject(0, 2, 23.15628f),
						new CampaignSpawnObject(0, 2, 23.60776f),
						new CampaignSpawnObject(0, 2, 24.12417f),
						new CampaignSpawnObject(0, 1, 25.1483f),
						new CampaignSpawnObject(0, 1, 26.35785f),
						new CampaignSpawnObject(0, 1, 27.58803f),
						new CampaignSpawnObject(0, 3, 28.11779f),
						new CampaignSpawnObject(0, 3, 28.60057f),
						new CampaignSpawnObject(0, 2, 29.19908f),
						new CampaignSpawnObject(0, 1, 29.70821f),
						new CampaignSpawnObject(0, 0, 30.24431f),
						new CampaignSpawnObject(0, 1, 30.68689f),
						new CampaignSpawnObject(0, 2, 31.14419f),
						new CampaignSpawnObject(0, 0, 31.58411f),
						new CampaignSpawnObject(0, 1, 32.06869f),
						new CampaignSpawnObject(0, 2, 32.52846f),
						new CampaignSpawnObject(0, 0, 33.04696f),
						new CampaignSpawnObject(0, 1, 33.50901f),
						new CampaignSpawnObject(0, 2, 34.03465f)
				};
            case 2:
                return new CampaignSpawnObject[]{
						new CampaignSpawnObject(0, 0, 0.7556411f),
						new CampaignSpawnObject(0, 0, 1.389642f),
						new CampaignSpawnObject(0, 1, 2.111593f),
						new CampaignSpawnObject(0, 1, 2.856263f),
						new CampaignSpawnObject(0, 0, 3.563386f),
						new CampaignSpawnObject(0, 0, 4.300234f),
						new CampaignSpawnObject(0, 1, 5.015901f),
						new CampaignSpawnObject(0, 1, 5.737638f),
						new CampaignSpawnObject(0, 0, 6.370429f),
						new CampaignSpawnObject(0, 0, 7.115189f),
						new CampaignSpawnObject(0, 1, 7.863537f),
						new CampaignSpawnObject(0, 1, 8.630167f),
						new CampaignSpawnObject(0, 0, 9.258086f),
						new CampaignSpawnObject(0, 0, 10.01256f),
						new CampaignSpawnObject(0, 0, 10.72202f),
						new CampaignSpawnObject(0, 1, 11.36449f),
						new CampaignSpawnObject(0, 1, 12.01349f),
						new CampaignSpawnObject(0, 1, 12.67664f),
						new CampaignSpawnObject(0, 0, 13.28739f),
						new CampaignSpawnObject(0, 0, 13.94457f),
						new CampaignSpawnObject(0, 0, 14.6449f),
						new CampaignSpawnObject(0, 1, 15.26528f),
						new CampaignSpawnObject(0, 1, 15.94253f),
						new CampaignSpawnObject(0, 1, 16.55697f),
						new CampaignSpawnObject(0, 0, 17.09468f),
						new CampaignSpawnObject(0, 0, 17.44166f),
						new CampaignSpawnObject(0, 0, 17.93696f),
						new CampaignSpawnObject(0, 0, 18.17698f),
						new CampaignSpawnObject(0, 1, 18.62461f),
						new CampaignSpawnObject(0, 1, 19.0016f),
						new CampaignSpawnObject(0, 1, 19.50498f),
						new CampaignSpawnObject(0, 1, 19.8034f),
						new CampaignSpawnObject(0, 0, 20.26392f),
						new CampaignSpawnObject(0, 0, 20.58336f),
						new CampaignSpawnObject(0, 0, 20.94423f),
						new CampaignSpawnObject(0, 0, 21.30531f),
						new CampaignSpawnObject(0, 1, 21.84958f),
						new CampaignSpawnObject(0, 1, 22.26173f),
						new CampaignSpawnObject(0, 1, 22.67821f),
						new CampaignSpawnObject(0, 1, 23.0765f)
				}; 
            case 4:
                return new CampaignSpawnObject[]{
						new CampaignSpawnObject(0, 0, 0.5874951f),
						new CampaignSpawnObject(0, 1, 1.334906f),
						new CampaignSpawnObject(0, 1, 2.763239f),
						new CampaignSpawnObject(0, 0, 3.331414f),
						new CampaignSpawnObject(0, 0, 4.887691f),
						new CampaignSpawnObject(0, 2, 5.504981f),
						new CampaignSpawnObject(0, 2, 6.91331f),
						new CampaignSpawnObject(0, 1, 7.425032f),
						new CampaignSpawnObject(0, 0, 8.776243f),
						new CampaignSpawnObject(0, 1, 9.114018f),
						new CampaignSpawnObject(0, 2, 9.450239f),
						new CampaignSpawnObject(0, 0, 10.26108f),
						new CampaignSpawnObject(0, 1, 10.57794f),
						new CampaignSpawnObject(0, 2, 10.9354f),
						new CampaignSpawnObject(0, 3, 11.60421f),
						new CampaignSpawnObject(0, 3, 11.98757f),
						new CampaignSpawnObject(0, 3, 12.35642f),
						new CampaignSpawnObject(0, 0, 13.24792f),
						new CampaignSpawnObject(0, 1, 13.51473f),
						new CampaignSpawnObject(0, 2, 13.89681f),
						new CampaignSpawnObject(0, 0, 14.6329f),
						new CampaignSpawnObject(0, 1, 15.13648f),
						new CampaignSpawnObject(0, 2, 15.81569f),
						new CampaignSpawnObject(0, 1, 16.41159f),
						new CampaignSpawnObject(0, 0, 16.89521f),
						new CampaignSpawnObject(0, 3, 17.43334f),
						new CampaignSpawnObject(0, 1, 18.00895f),
						new CampaignSpawnObject(0, 2, 18.61068f),
						new CampaignSpawnObject(0, 0, 19.17562f),
						new CampaignSpawnObject(0, 1, 19.73245f),
						new CampaignSpawnObject(0, 2, 20.29696f),
						new CampaignSpawnObject(0, 3, 20.89693f),
						new CampaignSpawnObject(0, 0, 21.45526f),
						new CampaignSpawnObject(0, 1, 22.03554f),
						new CampaignSpawnObject(0, 2, 22.59304f),
						new CampaignSpawnObject(0, 3, 23.178f)
				};
            case 5:
                return new CampaignSpawnObject[]{
						new CampaignSpawnObject(1, 0, 0.6380009f),
						new CampaignSpawnObject(1, 1, 2.56992f),
						new CampaignSpawnObject(1, 2, 4.532207f),
						new CampaignSpawnObject(0, 0, 6.274105f),
						new CampaignSpawnObject(0, 1, 6.750828f),
						new CampaignSpawnObject(0, 2, 7.310286f),
						new CampaignSpawnObject(0, 0, 7.771842f),
						new CampaignSpawnObject(0, 1, 8.320497f),
						new CampaignSpawnObject(0, 2, 8.858018f),
						new CampaignSpawnObject(1, 3, 10.14896f),
						new CampaignSpawnObject(1, 2, 11.57405f),
						new CampaignSpawnObject(1, 1, 12.97969f),
						new CampaignSpawnObject(1, 0, 14.30064f),
						new CampaignSpawnObject(0, 0, 15.50194f),
						new CampaignSpawnObject(0, 1, 15.95257f),
						new CampaignSpawnObject(0, 2, 16.45998f),
						new CampaignSpawnObject(0, 3, 16.98867f),
						new CampaignSpawnObject(0, 2, 17.46318f),
						new CampaignSpawnObject(0, 1, 18.01994f),
						new CampaignSpawnObject(0, 0, 18.59392f),
						new CampaignSpawnObject(1, 0, 20.49498f),
						new CampaignSpawnObject(1, 1, 20.80182f),
						new CampaignSpawnObject(1, 2, 21.16364f),
						new CampaignSpawnObject(1, 3, 21.53482f)
				};
            case 6:
                return new CampaignSpawnObject[]{
						new CampaignSpawnObject(0, 0, 0.5248638f),
						new CampaignSpawnObject(0, 1, 1.583773f),
						new CampaignSpawnObject(0, 2, 2.514238f),
						new CampaignSpawnObject(0, 3, 3.349055f),
						new CampaignSpawnObject(1, 0, 4.407491f),
						new CampaignSpawnObject(1, 1, 4.679324f),
						new CampaignSpawnObject(1, 2, 4.959478f),
						new CampaignSpawnObject(1, 3, 5.237309f),
						new CampaignSpawnObject(0, 0, 9.757545f),
						new CampaignSpawnObject(0, 1, 10.05414f),
						new CampaignSpawnObject(0, 2, 10.38018f),
						new CampaignSpawnObject(0, 3, 10.71618f),
						new CampaignSpawnObject(0, 2, 11.01814f),
						new CampaignSpawnObject(0, 1, 11.33043f),
						new CampaignSpawnObject(0, 0, 11.67125f),
						new CampaignSpawnObject(0, 1, 11.97188f),
						new CampaignSpawnObject(0, 2, 12.34033f),
						new CampaignSpawnObject(0, 3, 12.67345f),
						new CampaignSpawnObject(1, 0, 13.77835f),
						new CampaignSpawnObject(1, 1, 15.09952f),
						new CampaignSpawnObject(1, 2, 16.25511f),
						new CampaignSpawnObject(1, 3, 17.6763f),
						new CampaignSpawnObject(0, 0, 20.95918f),
						new CampaignSpawnObject(0, 0, 21.28031f),
						new CampaignSpawnObject(0, 1, 21.65245f),
						new CampaignSpawnObject(0, 1, 21.99486f),
						new CampaignSpawnObject(0, 2, 22.38047f),
						new CampaignSpawnObject(0, 2, 22.72034f),
						new CampaignSpawnObject(0, 3, 23.09981f),
						new CampaignSpawnObject(0, 3, 23.4004f),
				};            
            case 7:
                return new CampaignSpawnObject[]{
						new CampaignSpawnObject(1, 0, 1.329101f),
						new CampaignSpawnObject(1, 0, 3.165498f),
						new CampaignSpawnObject(1, 0, 5.128194f),
						new CampaignSpawnObject(1, 1, 6.502516f),
						new CampaignSpawnObject(1, 1, 7.99469f),
						new CampaignSpawnObject(1, 1, 9.532f),
						new CampaignSpawnObject(1, 1, 10.96817f),
						new CampaignSpawnObject(1, 1, 12.69888f),
						new CampaignSpawnObject(0, 2, 13.96835f),
						new CampaignSpawnObject(0, 3, 14.36578f),
						new CampaignSpawnObject(0, 2, 14.74614f),
						new CampaignSpawnObject(0, 3, 15.08637f),
						new CampaignSpawnObject(0, 2, 15.47266f),
						new CampaignSpawnObject(0, 3, 15.79821f),
						new CampaignSpawnObject(1, 0, 16.97593f),
						new CampaignSpawnObject(1, 1, 17.45822f),
						new CampaignSpawnObject(1, 2, 17.9057f),
						new CampaignSpawnObject(1, 3, 18.28548f),
						new CampaignSpawnObject(0, 0, 19.49871f),
						new CampaignSpawnObject(0, 1, 19.77759f),
						new CampaignSpawnObject(0, 0, 20.06783f),
						new CampaignSpawnObject(0, 1, 20.3892f),
						new CampaignSpawnObject(0, 0, 20.67272f),
						new CampaignSpawnObject(0, 1, 20.95825f),
						new CampaignSpawnObject(0, 2, 21.29828f),
						new CampaignSpawnObject(0, 3, 21.59458f),
						new CampaignSpawnObject(0, 2, 21.97783f),
						new CampaignSpawnObject(0, 1, 22.23783f),
						new CampaignSpawnObject(0, 0, 22.51309f),
						new CampaignSpawnObject(0, 1, 22.77892f),
						new CampaignSpawnObject(0, 2, 23.03856f),
						new CampaignSpawnObject(0, 3, 23.29911f)
				};
            case 8:
                return new CampaignSpawnObject[]{
						new CampaignSpawnObject(1, 0, 0.4033155f),
						new CampaignSpawnObject(1, 1, 0.8520811f),
						new CampaignSpawnObject(1, 2, 1.342854f),
						new CampaignSpawnObject(1, 3, 1.791221f),
						new CampaignSpawnObject(1, 1, 6.282711f),
						new CampaignSpawnObject(1, 2, 6.656371f),
						new CampaignSpawnObject(1, 0, 7.117184f),
						new CampaignSpawnObject(1, 3, 7.662485f),
						new CampaignSpawnObject(1, 1, 11.48713f),
						new CampaignSpawnObject(1, 2, 11.94055f),
						new CampaignSpawnObject(1, 0, 12.37629f),
						new CampaignSpawnObject(1, 3, 12.98875f),
						new CampaignSpawnObject(1, 1, 16.41306f),
						new CampaignSpawnObject(1, 2, 16.75452f),
						new CampaignSpawnObject(1, 3, 17.16569f),
						new CampaignSpawnObject(1, 0, 17.64712f),
						new CampaignSpawnObject(1, 0, 21.11669f),
						new CampaignSpawnObject(1, 3, 21.77622f),
						new CampaignSpawnObject(1, 0, 22.51278f),
						new CampaignSpawnObject(1, 2, 23.19634f)
				};
            case 9:
                return new CampaignSpawnObject[]{
						new CampaignSpawnObject(0, 0, 0.4209475f),
						new CampaignSpawnObject(0, 1, 1.071849f),
						new CampaignSpawnObject(0, 0, 1.428648f),
						new CampaignSpawnObject(0, 1, 1.84451f),
						new CampaignSpawnObject(0, 0, 2.245658f),
						new CampaignSpawnObject(0, 1, 2.669864f),
						new CampaignSpawnObject(0, 0, 3.080596f),
						new CampaignSpawnObject(0, 1, 3.482135f),
						new CampaignSpawnObject(0, 0, 3.922974f),
						new CampaignSpawnObject(0, 1, 4.380161f),
						new CampaignSpawnObject(0, 0, 4.823738f),
						new CampaignSpawnObject(0, 1, 5.251984f),
						new CampaignSpawnObject(0, 0, 5.630158f),
						new CampaignSpawnObject(0, 1, 6.062499f),
						new CampaignSpawnObject(1, 2, 7.575298f),
						new CampaignSpawnObject(1, 3, 7.928162f),
						new CampaignSpawnObject(1, 1, 8.746149f),
						new CampaignSpawnObject(1, 0, 9.182003f),
						new CampaignSpawnObject(1, 2, 10.14223f),
						new CampaignSpawnObject(1, 3, 10.63214f),
						new CampaignSpawnObject(1, 1, 11.16282f),
						new CampaignSpawnObject(1, 0, 11.72163f),
						new CampaignSpawnObject(0, 2, 12.91842f),
						new CampaignSpawnObject(0, 3, 13.20232f),
						new CampaignSpawnObject(0, 2, 13.51634f),
						new CampaignSpawnObject(0, 3, 13.8864f),
						new CampaignSpawnObject(0, 2, 14.22476f),
						new CampaignSpawnObject(0, 3, 14.59088f),
						new CampaignSpawnObject(0, 2, 14.91543f),
						new CampaignSpawnObject(0, 3, 15.28641f),
						new CampaignSpawnObject(0, 2, 15.65595f),
						new CampaignSpawnObject(0, 3, 16.00748f),
						new CampaignSpawnObject(0, 2, 16.35793f),
						new CampaignSpawnObject(0, 3, 16.76973f),
						new CampaignSpawnObject(0, 2, 17.1125f),
						new CampaignSpawnObject(0, 3, 17.4518f),
						new CampaignSpawnObject(0, 2, 17.80878f),
						new CampaignSpawnObject(0, 3, 18.21827f),
						new CampaignSpawnObject(0, 2, 18.55403f),
						new CampaignSpawnObject(0, 3, 18.92963f),
						new CampaignSpawnObject(0, 2, 19.28925f),
						new CampaignSpawnObject(0, 3, 19.67028f),
						new CampaignSpawnObject(0, 0, 20.07735f),
						new CampaignSpawnObject(0, 1, 20.3982f),
						new CampaignSpawnObject(0, 2, 20.80185f),
						new CampaignSpawnObject(0, 3, 21.17676f),
						new CampaignSpawnObject(0, 0, 21.5739f),
						new CampaignSpawnObject(0, 1, 21.97104f),
						new CampaignSpawnObject(0, 2, 22.34238f),
						new CampaignSpawnObject(0, 3, 22.75053f),
						new CampaignSpawnObject(0, 0, 23.15285f),
						new CampaignSpawnObject(0, 1, 23.53155f),
						new CampaignSpawnObject(0, 2, 23.8764f),
						new CampaignSpawnObject(0, 3, 24.2875f),
						new CampaignSpawnObject(0, 2, 24.67669f),
						new CampaignSpawnObject(0, 1, 25.05489f),
						new CampaignSpawnObject(0, 0, 25.43308f),
						new CampaignSpawnObject(0, 1, 25.7724f),
						new CampaignSpawnObject(0, 2, 26.12266f),
						new CampaignSpawnObject(0, 3, 26.504f),
						new CampaignSpawnObject(0, 2, 26.8659f),
						new CampaignSpawnObject(0, 1, 27.27474f),
						new CampaignSpawnObject(0, 0, 27.6268f)
				};
            case 10:
                return new CampaignSpawnObject[]{
						new CampaignSpawnObject(2, 1, 0.7637398f),
						new CampaignSpawnObject(2, 2, 1.316073f),
						new CampaignSpawnObject(0, 0, 2.767185f),
						new CampaignSpawnObject(0, 1, 3.15762f),
						new CampaignSpawnObject(0, 2, 3.578604f),
						new CampaignSpawnObject(0, 3, 3.983543f),
						new CampaignSpawnObject(0, 0, 4.540139f),
						new CampaignSpawnObject(0, 1, 4.938185f),
						new CampaignSpawnObject(0, 2, 5.388143f),
						new CampaignSpawnObject(0, 3, 5.788246f),
						new CampaignSpawnObject(0, 0, 6.404911f),
						new CampaignSpawnObject(0, 1, 6.783171f),
						new CampaignSpawnObject(0, 2, 7.308513f),
						new CampaignSpawnObject(0, 3, 7.980189f),
						new CampaignSpawnObject(0, 0, 8.526679f),
						new CampaignSpawnObject(0, 1, 8.895185f),
						new CampaignSpawnObject(0, 2, 9.349133f),
						new CampaignSpawnObject(0, 3, 9.787659f),
						new CampaignSpawnObject(1, 0, 13.04631f),
						new CampaignSpawnObject(1, 1, 13.40945f),
						new CampaignSpawnObject(1, 2, 13.82403f),
						new CampaignSpawnObject(1, 3, 14.30481f),
						new CampaignSpawnObject(1, 0, 15.61846f),
						new CampaignSpawnObject(1, 1, 16.10448f),
						new CampaignSpawnObject(1, 2, 16.64416f),
						new CampaignSpawnObject(1, 3, 17.18964f),
						new CampaignSpawnObject(1, 0, 20.03183f),
						new CampaignSpawnObject(1, 1, 20.46878f),
						new CampaignSpawnObject(1, 2, 21.10611f),
						new CampaignSpawnObject(1, 3, 21.70553f),
						new CampaignSpawnObject(2, 1, 27.22316f),
						new CampaignSpawnObject(2, 2, 27.65799f),
						new CampaignSpawnObject(0, 0, 29.43622f),
						new CampaignSpawnObject(0, 0, 30.00331f),
						new CampaignSpawnObject(0, 1, 30.50227f),
						new CampaignSpawnObject(0, 1, 31.02265f),
						new CampaignSpawnObject(0, 3, 31.50393f),
						new CampaignSpawnObject(0, 3, 32.11753f),
						new CampaignSpawnObject(0, 2, 32.69601f),
						new CampaignSpawnObject(0, 2, 33.56432f),
						new CampaignSpawnObject(0, 1, 34.35392f),
						new CampaignSpawnObject(0, 0, 34.91359f),
						new CampaignSpawnObject(0, 3, 35.68115f),
						new CampaignSpawnObject(0, 2, 36.16351f),
						new CampaignSpawnObject(0, 1, 36.78122f),
						new CampaignSpawnObject(0, 0, 37.35246f),
						new CampaignSpawnObject(0, 3, 37.91236f),
						new CampaignSpawnObject(0, 2, 38.3892f),
						new CampaignSpawnObject(0, 1, 39.00969f),
						new CampaignSpawnObject(0, 0, 39.61378f),
						new CampaignSpawnObject(0, 1, 40.965f),
						new CampaignSpawnObject(0, 2, 41.48872f),
						new CampaignSpawnObject(2, 1, 43.55643f),
						new CampaignSpawnObject(2, 2, 44.14285f),
						new CampaignSpawnObject(2, 3, 44.61797f),
						new CampaignSpawnObject(2, 0, 45.14049f)
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
