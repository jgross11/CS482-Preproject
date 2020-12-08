using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieSpawnerScript : MonoBehaviour
{
    //random number generator
    System.Random rand = new System.Random();


    // zombie prefab
    public GameObject[] zombie;

    // 3D coords to spawn zombie at
    public Transform spawnPosition;

    // time between spawns in seconds
    public float spawnTimer;

    // current time between spawns
    private float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // add time between the last frame and this one to counter
        currentTime += Time.deltaTime;
        
        // if it is time to spawn a new zombie
        if(spawnTimer < currentTime) {
            int num = rand.Next();

            // spawn random zombie at given position
            Instantiate(zombie[num%zombie.Length], spawnPosition);

            // reset spawn timer
            currentTime = 0;
        }
    }
}
