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
        // TODO set all save data attributes here...
        PlayerPrefs.SetInt(SaveObject.ESSENCE, SaveObject.numEssence);
        PlayerPrefs.SetInt(SaveObject.MAX_SWARM_WAVE, SaveObject.maxSwarmWave);
        PlayerPrefs.SetInt(SaveObject.MAX_CAMPAIGN_WAVE, SaveObject.maxCampaignWave);
        PlayerPrefs.SetInt(SaveObject.SWARM_TOWER_SELECTIONS, SaveObject.swarmTowerSelections);

        // acts as force save, otherwise only save occurs on safe exit of game (everything is lost on game crash)
        PlayerPrefs.Save();
    }

    // load player information from playerpreferences
    public static void LoadFromPlayerPreferences(){

        // determine if save data exists
        saveDataExists = PlayerPrefs.HasKey(SaveObject.MAX_CAMPAIGN_WAVE);

        // TODO get all save data attributes here...
        SaveObject.numEssence = PlayerPrefs.GetInt(SaveObject.ESSENCE, 0);
        SaveObject.maxSwarmWave = PlayerPrefs.GetInt(SaveObject.MAX_SWARM_WAVE, 0);
        SaveObject.maxCampaignWave = PlayerPrefs.GetInt(SaveObject.MAX_CAMPAIGN_WAVE, 0);

        // default swarm wave to load is most challenging not yet beat
        SaveObject.loadSwarmWave = SaveObject.maxSwarmWave+1;
        SaveObject.swarmTowerSelections = PlayerPrefs.GetInt(SaveObject.SWARM_TOWER_SELECTIONS, 0);
    }

    // save player information to binary file
    public static void SaveToBinary(){
        Debug.LogError("Not yet implemented!");
    }

    // load player information from binary file
    public static void LoadFromBinary(){
        Debug.LogError("Not yet implemented!");
    }

    // delete all save information from player prefs
    public static void WipeAllSavedData(){
        PlayerPrefs.DeleteAll();
    }
}
