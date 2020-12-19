using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// abstract class representing a generic zombie spawner
public abstract class ZombieSpawner : MonoBehaviour
{
    // all spawners must draw from a zombie prefab array
    public GameObject[] zombieSelectionArray;

    // all spawners must have a variety of spawning locations
    public Vector3[] spawningLocations;

    // all spawners must keep track of the time since wave started
    // to know when to spawn their next zombie
    public float timeSinceWaveStart;

    // all subclasses will have their own method of spawning (fixed or random)
    // and will need to implement their respective method themselves
    public abstract void Spawn();
}
