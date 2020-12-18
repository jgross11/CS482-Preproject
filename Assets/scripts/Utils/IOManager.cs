using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IOManager : MonoBehaviour
{
    public static bool saveDataExists;

    // save player information to local file
    public static void SaveToLocalFile(){
        Debug.LogError("Not yet implemented!");
    }

    // load player information from local file
    public static void LoadFromLocalFile(){
        Debug.LogError("Not yet implemented!");
    }

    // save player information to playerpreferences
    public static void SaveToPlayerPreferences(){
        PlayerPrefs.SetInt(SaveObject.HAPPINESS, SaveObject.numHappiness);
        PlayerPrefs.SetInt(SaveObject.MAX_SWARM_WAVE, SaveObject.maxSwarmWave);
        PlayerPrefs.SetInt(SaveObject.MAX_CAMPAIGN_WAVE, SaveObject.maxCampaignWave);
        // TODO set all save data attributes here...

        // acts as force save, otherwise only save occurs on safe exit of game (everything is lost on game crash)
        PlayerPrefs.Save();
    }

    // load player information from playerpreferences
    public static void LoadFromPlayerPreferences(){

        // determine if save data exists
        saveDataExists = PlayerPrefs.HasKey(SaveObject.HAPPINESS);

        // TODO get all save data attributes here...
        SaveObject.numHappiness = PlayerPrefs.GetInt(SaveObject.HAPPINESS, 0);
        SaveObject.maxSwarmWave = PlayerPrefs.GetInt(SaveObject.MAX_SWARM_WAVE, 0);
        SaveObject.maxSwarmWave = PlayerPrefs.GetInt(SaveObject.MAX_CAMPAIGN_WAVE, 0);
    }

    // save player information to binary file
    public static void SaveToBinary(){
        Debug.LogError("Not yet implemented!");
    }

    // load player information from binary file
    public static void LoadFromBinary(){
        Debug.LogError("Not yet implemented!");
    }

    public static void WipeAllSavedData(){
        PlayerPrefs.DeleteAll();
    }
}
