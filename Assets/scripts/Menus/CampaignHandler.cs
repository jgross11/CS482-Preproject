﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CampaignHandler : MonoBehaviour
{

    public Text campaignText;
    void Start()
    {
        campaignText.text = SaveObject.maxCampaignWave > 0 ? "Continue Campaign" : "Start Campaign";
    }
}
