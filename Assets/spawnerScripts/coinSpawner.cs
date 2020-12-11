using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinSpawner : MonoBehaviour
{

    public GameObject Coin;

    public Transform position;

    public float spawnTimer;

    public float currentTime;


    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        
        // if it is time to spawn a new zombie
        if (spawnTimer < currentTime)
        {
            
            // spawn random zombie at given position
            Instantiate(Coin, position);

            // reset spawn timer
            currentTime = 0;
        }
    }
}
