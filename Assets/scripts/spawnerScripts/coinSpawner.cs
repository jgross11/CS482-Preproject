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

    private List<GameObject> coins;


    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
        position = spawnerPosition.position;
        coins = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        
        // if it is time to spawn a new coin
        if (spawnTimer < currentTime)
        {
            
            // spawn random zombie at given position
            coins.Add(Instantiate(Coin, position, rotation));

            // reset spawn timer
            currentTime = 0;
        }
    }

    public void Reset(){
        // reset spawn timer
        currentTime = 0;

        // delete any created coins
        foreach(GameObject coin in coins)
        {
            Destroy(coin);
        }
        coins = new List<GameObject>();
    }
}
