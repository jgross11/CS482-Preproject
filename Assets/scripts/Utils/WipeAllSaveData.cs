using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WipeAllSaveData : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void WipeData(){
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        IOManager.LoadFromPlayerPreferences();
        Debug.Log("Player prefs wiped!");
        GameObject.Find("Main Camera").GetComponent<ActionMenuHandler>().Reset();
    }
}
