using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampaignLogicHandler : MonoBehaviour
{
    // used to reveal only the appropriate towers for player to use
    public playMenuHandler menuScript;

    // the prefab for a blank board
    public GameObject clearBoardPrefab;

    // the instance of the board to wipe
    public GameObject boardInstance;

    // the position at which to place a new board
    public Vector3 initialBoardPos;

    // coin spawn time reset upon loading of new wave
    public coinSpawner coinScript;

    // used to obtain next wave contents
    public CampaignZombieSpawner spawnerScript;

    void Start(){
        // TODO determine what the scaling formula for tower unlocks in campaign mode is
        // allow only the appropriate employees to be used, more to be added as campaign progresses
        menuScript.availableEmployees = TrimAvailableEmployees(SaveObject.maxCampaignWave / 5 + 1);

        // obtain original board position to reference when placing new board upon wave ending
        initialBoardPos = boardInstance.transform.position;
    }

    void Update(){
        
    }

    // handles all logic necessary to mark the end of the current wave
    public void EndCurrentWave(){

        // increment max campaign wave in save file
        PlayerPrefs.SetInt(SaveObject.MAX_CAMPAIGN_WAVE, ++SaveObject.maxCampaignWave);
        
        // reset board to blank
        Destroy(boardInstance);
        boardInstance = Instantiate(clearBoardPrefab, initialBoardPos, Quaternion.identity);

        // reset coin spawn timer
        coinScript.currentTime = 0;

        // set number of coins to start with for next wave
        // TODO determine proper scaling function for coin count
        menuScript.SetCoins(10 + SaveObject.maxCampaignWave);

        // start next wave
        spawnerScript.LoadNextWave();
    }

    // newIndexAmount - the upper index whose employee will be displayed. All higher-indice employees are locked
    private GameObject[] TrimAvailableEmployees(int newIndexAmount){

        // create subarray
        GameObject[] result = new GameObject[newIndexAmount];

        // add unlocked tower to sub array
        for(int i = 0; i < newIndexAmount; i++){
            result[i] = menuScript.availableEmployees[i];
        }

        // return subarray
        return result;
    }
}
