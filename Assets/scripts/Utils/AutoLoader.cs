using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        IOManager.LoadFromPlayerPreferences();
        if(IOManager.saveDataExists){
            SceneTransitioner.Transition("Action Menu");
        }
    }
}
