using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveCreatorController : MonoBehaviour
{
    public Text waveTimeText;
    public float waveTime;
    public Vector3[] spawnPositions;
    public GameObject[] zombieTypes;
    public int selectedZombieIndex;
    public bool isRecording;
    public string waveDataAsText;
    public Text recordingText;

    void Start(){
        Reset();

    }

    void Reset(){
        waveTime = 0;
        waveDataAsText = "return new CampaignSpawnObject[]{";
    }

    void Update(){
        if(isRecording){
            waveTime += Time.deltaTime;
            recordingText.text = "Wave time: " + Math.Round(waveTime, 2);
        }
    }

    public void CreateNewEntry(int spawnPositionIndex){
        waveDataAsText += "\n\t\t\t\t\t\tnew CampaignSpawnObject("+selectedZombieIndex+", "+spawnPositionIndex+", "+waveTime+"f),";
        Instantiate(zombieTypes[selectedZombieIndex], spawnPositions[spawnPositionIndex], Quaternion.identity);
    }

    public void UpdateSelectedZombie(int newIndex){
        selectedZombieIndex = newIndex;
    }

    public void ToggleState(){
        isRecording = !isRecording;
        if(isRecording) {
            Reset();
            recordingText.text = "Stop recording";
        }
        else {
            waveDataAsText += "\n\t\t\t\t};";
            Debug.Log(waveDataAsText);
            recordingText.text = "Start recording";
        }
    }
}
