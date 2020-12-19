using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// container class that holds zombie spawn information
public class CampaignSpawnObject
{
    // index in zombie spawner's zombie type array to spawn
    public int zombieIndex;

    // position in zombie spawner's zombie position array to spawn at
    public int positionIndex;

    // time at which to spawn this zombie
    public float spawnTime;

    public CampaignSpawnObject(int zIndex, int posIndex, float spaTime){
        zombieIndex = zIndex;
        positionIndex = posIndex;
        spawnTime = spaTime;
    }
}
