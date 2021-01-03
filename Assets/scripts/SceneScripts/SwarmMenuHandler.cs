using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwarmMenuHandler : MonoBehaviour
{
    
    // sprites that display the currently selected towers on your team
    public SpriteRenderer[] selectedTowerTilePreviews;

    // sprites for the available towers to choose from in the menu
    public SpriteRenderer[] towerMenuTilePreviews;

    // displays the currently set swarm wave number to load
    public Text swarmNumberText;

    // displays a brief description of the tower that is being hovered over / team setting directions
    public Text descriptionText;

    // index in the swarm team whose tower choice is being selected
    public int selectionToMakeIndex;

    // sprite of the tile whose tower choice is being selected
    public SpriteRenderer selectionToMakeSpriteRenderer;

    // enabled / disabled according to the current and max swarm wave selection
    public Button incrementSwarmNumberButton;
    public Button decrementSwarmNumberButton;

    // descriptions of the towers
    public string[] descriptions = {
        "Produce\nType: Offense\nRange: Medium\nThrows carrots that deal damage to zombies.",
        "Cashier\nType: Support\nRange: Short\nGenerates coins to purchase more employees with.",
        "Butcher\nType: Offense\nRange: Short\nSwings cleaver when zombie comes within range.",
        "Furniture\nType: Defense\nRange: Short\nConstructs pillow fort to defend against attacks.",
        "Cans\nType: Offense\nRange: Long\nThrows soda cans that deal damage to zombies.",
        "Pharmacist\nType: Support\nRange: Long\nCreates pills that heal targeted ally.",
        "Manager\nType: Support\nRange: Medium\nBoosts the stats of nearby employees.",
        "Greeter\nType: Support\nRange: Short\nGenerates happiness to use in Research mode.",
        "Steve\nType: Offense\nRange: All\nTargets oldest zombie, regardless of the row it is in.",
        "Agent M\nType: Offense\nRange: Long\nThrows Malwart shurikens that deal damage to zombies."
    };
    
    // Start is called before the first frame update
    void Start()
    {
        // start with no selection being made
        selectionToMakeIndex = -1;
        
        // set description text to selection instructions
        UpdateHover(-1);

        // populate swarm team with previously saved towers
        LoadPreviousTeam();

        // determine which of increment and decrement buttons should be interactable
        DetermineButtonAbility();
    }

    // Update is called once per frame
    void Update()
    {
        // doesn't need to be here but w/e
        swarmNumberText.text = "Swarm Wave # To Start At: " + SaveObject.loadSwarmWave;
    }

    public void DetermineButtonAbility(){
        incrementSwarmNumberButton.enabled = SaveObject.loadSwarmWave > SaveObject.maxSwarmWave ? false : true;
        decrementSwarmNumberButton.enabled = SaveObject.loadSwarmWave == 1 ? false : true;
    }

    public void IncrementWaveNumber(){
        if(SaveObject.loadSwarmWave <= SaveObject.maxSwarmWave){
            if(++SaveObject.loadSwarmWave == SaveObject.maxSwarmWave) 
                incrementSwarmNumberButton.enabled = false;
            else if(!decrementSwarmNumberButton.enabled)
                decrementSwarmNumberButton.enabled = true;
        }
    }

    public void DecrementWaveNumber(){
        if(SaveObject.loadSwarmWave > 1){
            if(--SaveObject.loadSwarmWave == 1) 
                decrementSwarmNumberButton.enabled = false;
            else if(!incrementSwarmNumberButton.enabled)
                incrementSwarmNumberButton.enabled = true;
        }
    }

    private void LoadPreviousTeam(){
        // deconstruct swarm tower selection integer
        for(int i = 0; i < selectedTowerTilePreviews.Length; i++){
            // set current index's sprite to      the sprite of the tower in the menu whose index is the value at the index in the swarm tower selection integer
            selectedTowerTilePreviews[i].sprite = towerMenuTilePreviews[(SaveObject.swarmTowerSelections >> i*4) & 0xf].sprite;
        }
    }

    // index - index of tower whose value will be updated
    // value - the value representing the type of tower to update
    // selected swarm towers are stored in one integer, with each four bit group 
    // containing a value (0-15) representing the type of tower to populate in that spot
    // these values align with the layout in the SwarmMenu scene
    // 0 = produce, 1 = cashier, ... can = 4, pharmacist = 5, ... agent m = 9
    public void UpdateSelectedTower(int value){

        // rewrite current tower selection integer
        // result: the only changed bits are the four bits at the selected index's slot in the integer                                       
        // basically, what goes on:       (     clear the four (0xf) bits at the index this value lies in    ) (insert the four bits representing the new value at the proper location)
        SaveObject.swarmTowerSelections = (SaveObject.swarmTowerSelections & ~(0xf << selectionToMakeIndex*4)) | (value << selectionToMakeIndex*4);

        // set sprite in team display
        selectedTowerTilePreviews[selectionToMakeIndex].sprite = towerMenuTilePreviews[value].sprite;

        // if a selection was made
        if(selectionToMakeSpriteRenderer != null){

            // reset selected sprite
            selectionToMakeSpriteRenderer.color = Color.white;
            selectionToMakeSpriteRenderer = null;
        }
    }

    public void UpdateTeamTowerSelection(int index, SpriteRenderer spr){
        
        // if a selection was made
        if(selectionToMakeSpriteRenderer != null){

            // reset selected sprite
            selectionToMakeSpriteRenderer.color = Color.white;
            selectionToMakeSpriteRenderer = null;
        }

        // set selected index and sprite
        selectionToMakeIndex = index;
        selectionToMakeSpriteRenderer = spr;
    }

    public void UpdateHover(int index){
        // if invalid index passed
        if(index < 0 || index >= descriptions.Length){
            descriptionText.text= "To make or update a selection, select the slot you wish to fill, then select a tower.";
        }
        else{
            // set appropriate tower's description
            descriptionText.text = descriptions[index];
        }
    }
}
