using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveObject
{
    // amount of james' essence 
    public static int numEssence;
    public const string ESSENCE = "essence";

    // highest swarm wave number
    public static int maxSwarmWave;
    public const string MAX_SWARM_WAVE = "maxswarmwavenumber";

    // highest campaign wave number
    public static int maxCampaignWave;
    public const string MAX_CAMPAIGN_WAVE = "maxcampaignwavenumber";

    // swarm wave to load (used when transitioning to swarm game mode)
    public static int loadSwarmWave;
    public const string LOAD_SWARM_WAVE = "loadswarmwave";

    // swarm tower selections
    // one integer, but first 6 four-bit slots represent the value of a tower in the selection menu
    public static int swarmTowerSelections;
    public const string SWARM_TOWER_SELECTIONS = "swarmtowerselections";
}
