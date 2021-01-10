using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// helps avoid ambiguity / verbosity between System.Random and UnityEngine.Random
using r = UnityEngine.Random;


// handles all spawning logic for the zombie spawner found in swarm mode
public class SwarmZombieSpawner : ZombieSpawner
{
    // upper bound on the index (types) of zombie available to spawn
    public int zombieUpperBound;

    // text that displays when next zombie is spawning
    public Text nextZombieText;

    // decreases as wave number increases
    public float timeBetweenSpawns = 5.0f;

    // counter for time between spawns
    public float currentTime = -5.0f;

    // increases as wave number increases
    // in actuality, [1, this] number of zombies are randomly spawned
    public int numZombiesToSpawn = 1;

    // level that zombies are when spawned in
    public int zombieSpawnLevel = 1;

    // may be variable in the future? in seconds
    public float timeBetweenWaves = 30f;

    void Start(){

        // if played through advancement stage
        // set params to endless stage
        if(SaveObject.loadSwarmWave > 30){
            timeBetweenSpawns = 0.5f;
            numZombiesToSpawn = 4;
            zombieUpperBound = zombieSelectionArray.Length;

        // otherwise, vary params according to progress through advancement stage
        } else{
            // decrease time between spawns by 0.5 seconds every four waves
            timeBetweenSpawns = 5.0f - (0.5f*(SaveObject.loadSwarmWave/4));

            // increase number of zombies that can spawn at one time every ten waves
            numZombiesToSpawn = (SaveObject.loadSwarmWave / 10) + 1; 

            // increase types of zombies that can spawn every 6 waves
            zombieUpperBound = (SaveObject.loadSwarmWave / 6) + 1;
        }

        // zombie level has no cap, but is incremented every 5 waves
        zombieSpawnLevel = (SaveObject.loadSwarmWave / 5) + 1;
    }

    // Update is called once per frame
    void Update()
    {
        // add to wave timer
        timeSinceWaveStart += Time.deltaTime;

        // add to time between spawns counter
        currentTime += Time.deltaTime;

        // if it is time to spawn zombie(s)
        if(currentTime > timeBetweenSpawns){

            // reset time between spawns counter
            currentTime = 0;

            // spawn zombie(s)
            Spawn();
        }

        // if it is time to advance to the next wave
        if(timeSinceWaveStart >= timeBetweenWaves){

            // increment current wave number
            SaveObject.loadSwarmWave++;

            // if new highest wave achieved
            if(SaveObject.loadSwarmWave > SaveObject.maxSwarmWave){

                // record record in save object
                SaveObject.maxSwarmWave = SaveObject.loadSwarmWave-1;
                Debug.Log("Passed wave " + SaveObject.maxSwarmWave);
                // record record in playerprefs and save record
                PlayerPrefs.SetInt(SaveObject.MAX_SWARM_WAVE, SaveObject.maxSwarmWave);
                PlayerPrefs.Save();
            }

            // reset wave timer
            timeSinceWaveStart = -5.0f;

            // reset spawn timer
            currentTime = -5.0f;

            // update wave params if necessary
            Start();
        }

        // update next zombie text
        nextZombieText.text = "Advancement in " + (Math.Round(timeBetweenWaves - timeSinceWaveStart, 1)) + "s";
    }

    public void Reset(){
        // wipe all currently existing zombies, if any exist
        Transform[] children = transform.GetComponentsInChildren<Transform>();
        
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

        // reset wave timers
        currentTime = -5.0f;
        timeSinceWaveStart = -5.0f;
    }

    public override void Spawn(){

        int actualNumToSpawn = (int)Math.Round(r.value * (numZombiesToSpawn-1)) + 1;
        Debug.Log(actualNumToSpawn);
        
        // TODO maybe god zombie shouldn't be equally likely to spawn as basic zombie?
        int typeToSpawn = (int)Math.Round(r.value * (zombieUpperBound-1));

        // spawn actualNumToSpawn zombies
        for(int i = 0; i < actualNumToSpawn; i++){

            // get script of zombie       whose type is this,       at one of the predefined spawning locations,                    offset by a random, small X amount,       standing up straight, as a child of this object
            Zombie zScript = Instantiate(zombieSelectionArray[typeToSpawn], spawningLocations[(int)Math.Round(r.value * 3)] + new Vector3((float)Math.Round(0.5f + (double)r.Range(0, 2)), 0, 0), Quaternion.identity, this.transform).GetComponent<Zombie>();
            
            // set zombie's level
            zScript.SetLevel(zombieSpawnLevel);
        }
    }
}
