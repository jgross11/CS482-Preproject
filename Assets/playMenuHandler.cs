using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
    
    // TODO script containing employee tower information
    // public EMPLOYEE SCRIPT NAME HERE employeeScript; 

    // number of coins / happiness (short term currency) gathered thus far
    private int numberOfCoins = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        numberOfCoins = 0;
        //Debug.Log(numberOfCoins);


        // create preview of each available employee type
        // in the header menu
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
        Given the index of the employee in availableEmployees,
        handle menu sprite state changes
    */
    public void SetSelectedEmployee(int index){
        // if index is valid (a valid tower preview has been clicked)
        if(index > -1 && index < availableEmployees.Length){

            // if a sprite has already been selected
            if(selectedEmployee != null){

                // change color of previously selected sprite back to normal
                menuSlots[selectedSlotIndex].GetComponent<SpriteRenderer>().color = Color.white;
            }

            // update selected slot index and selected employee references
            selectedSlotIndex = index;
            selectedEmployee = availableEmployees[index];

            // change color of new selected sprite to gray
            menuSlots[index].GetComponent<SpriteRenderer>().color = Color.gray;

            // update reference to employee information script to draw stats
            // employeeScript = selectedEmployee.GetComponent<EMPLOYEE SCRIPT NAME HERE>();
        
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
        // numberOfCoins -= employeeScript.cost;

        return selectedEmployee;
    }


    // determines if the selected tower can be 'bought'
    // returns true if the tower can be bought, false otherwise 
    public bool CanAffordTower(){
        return true;
        // return numberOfCoins >= employeeScript.cost;
    }

    // adds the given number of coins (short term currency)
    // to the total amount
    public void AddCoins(int number){
        numberOfCoins += number;
        Debug.Log(numberOfCoins);
    }

    // returns the selected employee prefab
    public GameObject GetSelectedEmployee(){
        return selectedEmployee;
    }
}
