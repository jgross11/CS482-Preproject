using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampaignLogicHandler : MonoBehaviour
{
    // used to reveal only the appropriate towers for player to use
    public playMenuHandler menuScript;

    // copy of entire tower list to use when revealing more towers
    public GameObject[] allAvailableEmployees;

    // the prefab for a blank board
    public GameObject clearBoardPrefab;

    // game over splash screen
    public GameObject gameOverGO;

    // the instance of the board to wipe
    public GameObject boardInstance;

    // the position at which to place a new board
    public Vector3 initialBoardPos;

    // positions to spawn bama women at
    public Vector3[] bamaWomenPositions;

    // array containing references to the bama gameobjects still on the board
    private GameObject[] bamaOnBoard;

    // bama women prefab
    public GameObject BAMAPrefab;

    // coin spawn time reset upon loading of new wave
    public coinSpawner coinScript;

    // used to obtain next wave contents
    public CampaignZombieSpawner spawnerScript;

    // game over detection collider - disabled when one zombie triggers game over, reenabled after game over sequence
    public Collider2D gameOverDetectorCollider;

    void Start(){
        // TODO determine what the scaling formula for tower unlocks in campaign mode is
        // allow only the appropriate employees to be used, more to be added as campaign progresses
        allAvailableEmployees = menuScript.availableEmployees;

        // obtain original board position to reference when placing new board upon wave ending
        initialBoardPos = boardInstance.transform.position;

        // init number of bamas on board to 4
        bamaOnBoard = new GameObject[4];

        // set parameters to start of wave
        ResetWaveParameters();
    }

    void Update(){
        
    }

    // handles game over logic
    public void GameOver(){

        // TODO fancy game over animations here
        StartCoroutine(GameOverAnimations());

    }

    public IEnumerator GameOverAnimations(){
        
        // move game over splash screen in front of camera
        gameOverGO.transform.position += Vector3.forward;

        // do any animations above, wait five (x) seconds, and then reset wave
        yield return new WaitForSeconds(5);

        // reset board to start-of-wave state
        ResetWaveParameters();

        // reload current wave
        spawnerScript.LoadNextWave();

        // reenable game over collider to detect game over for next wave
        gameOverDetectorCollider.enabled = true;

        // move game over splash screen behind camera
        gameOverGO.transform.position -= Vector3.forward;
    }

    // handles all logic necessary to mark the end of the current wave
    public void EndCurrentWave(){
        StartCoroutine(EndCurrentWaveAnimations());
    }

    public IEnumerator EndCurrentWaveAnimations(){

        // ensure wave / coin counter doesn't continuously increment
        spawnerScript.numAliveZombies = -1;

        // do any animations, sounds, etc. before this, wait five seconds, save and reset for next wave
        yield return new WaitForSeconds(5);
        
        // increment max campaign wave in save file, save changes to disk
        PlayerPrefs.SetInt(SaveObject.MAX_CAMPAIGN_WAVE, ++SaveObject.maxCampaignWave);
        PlayerPrefs.Save();

        // reset board, coin spawner, etc.
        ResetWaveParameters();

        // start next wave
        spawnerScript.LoadNextWave();
    }

    // handles resetting of board state
    public void ResetWaveParameters(){
        
        // reset board to blank
        Destroy(boardInstance);
        boardInstance = Instantiate(clearBoardPrefab, initialBoardPos, Quaternion.identity);

        // deselect selected employee
        menuScript.SetSelectedEmployee(-1);

        // set number of coins to start with for next wave
        // TODO determine proper scaling function for coin count
        menuScript.SetCoins(10 + SaveObject.maxCampaignWave);

        // calculate and reveal appropriate towers in menu
        menuScript.availableEmployees = SaveObject.maxCampaignWave / 5 + 1 > allAvailableEmployees.Length ? allAvailableEmployees : TrimAvailableEmployees(SaveObject.maxCampaignWave / 5 + 1);
        menuScript.CreateTowerPreview();
        menuScript.RefreshAffordableTowers();

        // reset coin spawn timer
        coinScript.currentTime = 0;

        // respawn bama women
        for(int i = 0; i < bamaOnBoard.Length; i++){
            if(bamaOnBoard[i] != null){
                Destroy(bamaOnBoard[i]);
            }
            bamaOnBoard[i] = Instantiate(BAMAPrefab, bamaWomenPositions[i], Quaternion.identity);
        }
    }

    // newIndexAmount - the upper index whose employee will be displayed. All higher-indice employees are locked
    private GameObject[] TrimAvailableEmployees(int newIndexAmount){

        // create subarray
        GameObject[] result = new GameObject[newIndexAmount];

        // add unlocked tower to sub array
        for(int i = 0; i < newIndexAmount; i++){
            result[i] = allAvailableEmployees[i];
        }

        // return subarray
        return result;
    }
}
