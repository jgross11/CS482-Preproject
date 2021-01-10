using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmPlaySetup : MonoBehaviour
{
    
    public playMenuHandler menuScript;
    public SwarmZombieSpawner spawnerScript;
    public GameObject[] towers;
    
    // Start is called before the first frame update
    void Start()
    {
        LoadSelectedTowers();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoadSelectedTowers(){
        // (SaveObject.swarmTowerSelections >> i*4) & 0xf
        for(int i = 0; i < menuScript.availableEmployees.Length; i++){
            menuScript.availableEmployees[i] = towers[(SaveObject.swarmTowerSelections >> i*4) & 0xf];
        }
        menuScript.CreateTowerPreview();
    }
}
