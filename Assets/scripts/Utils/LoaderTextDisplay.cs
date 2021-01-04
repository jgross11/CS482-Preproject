using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoaderTextDisplay : MonoBehaviour
{
    public Text display;

    void Update()
    {
        display.text = "Essence: " + SaveObject.numEssence;
    }

    public void IncrementHappiness(){
        SaveObject.numEssence++;
    }

    public void DecrementHappiness(){
        SaveObject.numEssence--;
    }
}
