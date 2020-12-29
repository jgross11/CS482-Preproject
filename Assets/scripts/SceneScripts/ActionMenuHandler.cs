using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionMenuHandler : MonoBehaviour
{
    // buttons to be un/locked accordingly
    public Button campaignButton;
    public Button researchButton;
    public Button swarmButton;

    // text displaying user's stats
    public Text userInfoText;

    // Start is called before the first frame update
    void Start()
    {
        // swarm button unlocked once a certain campaign wave has been reached
        swarmButton.interactable = SaveObject.maxCampaignWave > 19;

        // reserach button unlocked once a certain swam wave has been reached
        researchButton.interactable = SaveObject.maxSwarmWave > 20;

        // display saved stats to user
        userInfoText.text = "\nHappiness currency count: " + SaveObject.numHappiness;
        userInfoText.text += "\nHighest campaign wave: " + SaveObject.maxCampaignWave;
        userInfoText.text += "\nHighest swarm wave: " + SaveObject.maxSwarmWave;
    }

    public void Reset(){
        Start();
    }
}
