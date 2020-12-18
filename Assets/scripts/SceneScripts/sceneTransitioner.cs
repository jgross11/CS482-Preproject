using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitioner : MonoBehaviour
{
    public static void Transition(string sceneName){
        if(sceneName == null || sceneName == ""){
            Debug.LogError("Scene transition call missing name of scene to target");
            return;
        }
        SceneManager.LoadScene(sceneName);
    }
}
