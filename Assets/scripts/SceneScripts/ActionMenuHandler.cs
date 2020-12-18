using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionMenuHandler : MonoBehaviour
{
    
    public Button campaignButton;
    public Button researchButton;
    public Button swarmButton;
    public Text userInfoText;

    // Start is called before the first frame update
    void Start()
    {
        swarmButton.interactable = SaveObject.maxCampaignWave > 20;
        researchButton.interactable = SaveObject.maxCampaignWave > 40;
        userInfoText.text = "\nHappiness currency count: " + SaveObject.numHappiness;
        userInfoText.text += "\nHighest campaign wave: " + SaveObject.maxCampaignWave;
        userInfoText.text += "\nHighest swarm wave: " + SaveObject.maxSwarmWave;
    }
}
