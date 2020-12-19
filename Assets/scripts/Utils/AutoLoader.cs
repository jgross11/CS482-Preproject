using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Load save file if one exists
        IOManager.LoadFromPlayerPreferences();

        // if a save file was found, this is true
        if(IOManager.saveDataExists){

            // automatically load action menu scene
            SceneTransitioner.Transition("Action Menu");
        }
        // otherwise, it is false, and the player must click the "New Game" button
    }
}
