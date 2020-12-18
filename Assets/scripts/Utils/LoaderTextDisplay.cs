using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoaderTextDisplay : MonoBehaviour
{
    public Text display;

    void Update()
    {
        display.text = "Happiness: " + SaveObject.numHappiness;
    }

    public void IncrementHappiness(){
        SaveObject.numHappiness++;
    }

    public void DecrementHappiness(){
        SaveObject.numHappiness--;
    }
}
