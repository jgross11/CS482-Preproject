using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// handles a bunch of stuff that it probably shouldn't
// including: loading, selecting, and creation of actual tower
// from a given list of tower prefabs
public class playMenuHandler : MonoBehaviour
{
    // array containing prefabs for employee towers
    public GameObject[] availableEmployees;

    // array containing menu box slots to draw employee sprites in
    public GameObject[] menuSlots;

    // index in availableEmployees of the selected employee
    public int selectedSlotIndex;

    // the employee tower the player is considering creating
    public GameObject selectedEmployee;
    
    // selected employee script
    public Employee employeeScript; 

    // number of coins / happiness (short term currency) gathered thus far
    private int numberOfCoins = 10;

    // time scale text
    public Text timeScaleText;
    
    // Start is called before the first frame update
    void Start()
    {

        // number of coins depends on current wave
        // TODO figure out how to not make this a predetermined amount
        // TODO determine scaling function
        numberOfCoins = 10 + SaveObject.maxCampaignWave;

        // create preview of each available employee type
        // in the header menu
        CreateTowerPreview();

        // no tower is initially selected
        selectedSlotIndex = -1;

        // compute initial affordability
        RefreshAffordableTowers();

        // set initial time scale
        timeScaleText.text = Time.timeScale + "x";
    }

    // updates tower menu to display the sprites of available towers
    public void CreateTowerPreview(){
        for(int i = 0; i < availableEmployees.Length; i++)
        {
            // reference to employee in array
            GameObject go = availableEmployees[i];

            // slot in menu
            GameObject slot = menuSlots[i];

            // set menu sprite to this employee's sprite
            slot.GetComponent<SpriteRenderer>().sprite = go.GetComponent<SpriteRenderer>().sprite;
        }
    }
    
    /*
        Completely deselects the currently selected tower option in the menu
    */
    public void Deselect(){

        // reset menu tower sprite color
        menuSlots[selectedSlotIndex].GetComponent<SpriteRenderer>().color = CanAffordTower(employeeScript) ? Color.white : Color.grey;

        // wipe selected tower index
        selectedSlotIndex = -1;

        // wipe selected employee gameobject
        selectedEmployee = null;

        // wipe selected employee script
        employeeScript = null;
    }

    /*
        Given the index of the employee in availableEmployees,
        handle menu sprite state changes
    */
    public void SetSelectedEmployee(int index){
        // if index is valid (a valid tower preview has been clicked) and the targeted tower is affordable
        if(index > -1 && index < availableEmployees.Length && CanAffordTower(availableEmployees[index].GetComponent<Employee>())){

            // if a sprite has already been selected
            if(selectedEmployee != null){

                // change color of previously selected sprite back to normal
                menuSlots[selectedSlotIndex].GetComponent<SpriteRenderer>().color = Color.white;
            }

            // update selected slot index and selected employee references
            selectedSlotIndex = index;
            selectedEmployee = availableEmployees[index];

            // change color of new selected sprite to yellow
            menuSlots[index].GetComponent<SpriteRenderer>().color = Color.yellow;

            // update reference to employee information script to draw stats
            employeeScript = selectedEmployee.GetComponent<Employee>();
        
        // otherwise, an invalid index was passed (invalid click, etc.)
        } else{

            // if a sprite has already been selected
            if(selectedEmployee != null){

                // change color of previously selected sprite back to normal
                menuSlots[selectedSlotIndex].GetComponent<SpriteRenderer>().color = Color.white;
            };
            
            // wipe selected references
            selectedEmployee = null;
            selectedSlotIndex = -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseEnter(){
        Debug.Log("entered");
    }

    // decrement selected employee cost from current
    // number of coins and return the referenced employee prefab
    public GameObject CreateSelectedEmployee(){
        AddCoins(-employeeScript.cost);
        return selectedEmployee;
    }

    // determines if the currently selected tower can be bought
    // returns true if the tower can be bought, false otherwise
    public bool CanAffordTower(){
        return numberOfCoins >= employeeScript.cost;
    }

    // determines if the given Employee can be bought
    // returns true if the tower can be bought, false otherwise
    public bool CanAffordTower(Employee es){
        return numberOfCoins >= es.cost;
    }

    /*
        Iterates through list of available employees and
        changes their menu preview colors appropriately
    */
    public void RefreshAffordableTowers(){
        for(int i = 0; i < availableEmployees.Length; i++)
        {
            if(i == selectedSlotIndex){
                continue;
            }
            // a mouthful
            menuSlots[i].GetComponent<SpriteRenderer>().color = CanAffordTower(availableEmployees[i].GetComponent<Employee>()) ? Color.white : Color.grey;
        }
    }

    // adds the given number of coins (short term currency)
    // to the total amount and recomputes affordable towers
    public void AddCoins(int number){
        numberOfCoins += number;
        RefreshAffordableTowers();
    }

    // get number of coins
    public int GetCoins()
    {
        return numberOfCoins;
    }

    // set number of coins
    public void SetCoins(int val){
        numberOfCoins = val;
    }

    // returns the selected employee prefab
    public GameObject GetSelectedEmployee(){
        return selectedEmployee;
    }

    public void ChangeTimeScale(){
        Time.timeScale += 1;
        if(Time.timeScale > 3) Time.timeScale = 1;
        timeScaleText.text = Time.timeScale + "x";
    }
}
