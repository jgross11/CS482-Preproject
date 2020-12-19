using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CampaignHandler : MonoBehaviour
{
    // button text to update according to save state
    public Text campaignText;
    void Start()
    {
        // set text accordingly
        campaignText.text = SaveObject.maxCampaignWave > 0 ? "Continue Campaign" : "Start Campaign";
    }
}
