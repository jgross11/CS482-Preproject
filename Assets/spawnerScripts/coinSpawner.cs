using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinSpawner : MonoBehaviour
{

    public GameObject Coin;

    public Transform spawnerPosition;

    public Vector3 position;

    public Quaternion rotation;

    public float spawnTimer;

    public float currentTime;


    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
        position = spawnerPosition.position;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        
        // if it is time to spawn a new coin
        if (spawnTimer < currentTime)
        {
            
            // spawn random zombie at given position
            Instantiate(Coin, position, rotation);

            // reset spawn timer
            currentTime = 0;
        }
    }
}
