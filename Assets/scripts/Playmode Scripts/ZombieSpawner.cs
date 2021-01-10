using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// abstract class representing a generic zombie spawner
public abstract class ZombieSpawner : MonoBehaviour
{

    // zombie type constants
    protected const int BASIC_INDEX = 0;
    protected const int TANK_INDEX = 1;
    protected const int HEALER_INDEX = 2;
    protected const int MINI_BOSS_INDEX = 3;
    protected const int BOSS_INDEX = 4;

    // spawn position constants
    protected const int TOP = 0;
    protected const int MIDDLE_TOP = 1;
    protected const int MIDDLE_BOTTOM = 2;
    protected const int BOTTOM = 3;

    // all spawners must draw from a zombie prefab array
    public GameObject[] zombieSelectionArray;

    // all spawners must have a variety of spawning locations
    public Vector3[] spawningLocations;

    // all spawners must keep track of the time since wave started
    // to know when to spawn their next zombie
    public float timeSinceWaveStart = -5f;

    // all subclasses will have their own method of spawning (fixed or random)
    // and will need to implement their respective method themselves
    public abstract void Spawn();
}
