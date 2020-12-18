using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IOManager : MonoBehaviour
{
    // save player information to local file
    public void saveToLocalFile(){
        Debug.LogError("Not yet implemented!");
    }

    // load player information from local file
    public void loadFromLocalFile(){
        Debug.LogError("Not yet implemented!");
    }

    // save player information to playerpreferences
    public void saveToPlayerPreferences(){
        PlayerPrefs.SetInt("happiness", SaveObject.numHappiness);
    }

    // load player information from playerpreferences
    public void loadFromPlayerPreferences(){
        SaveObject.numHappiness = PlayerPrefs.GetInt("happiness", 0);
    }

    // save player information to binary file
    public void saveToBinary(){
        Debug.LogError("Not yet implemented!");
    }

    // load player information from binary file
    public void loadFromBinary(){
        Debug.LogError("Not yet implemented!");
    }
}
