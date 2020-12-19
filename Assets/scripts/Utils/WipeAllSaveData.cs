using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WipeAllSaveData : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("Player prefs wiped!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
