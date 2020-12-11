using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// handles setting of selected employee
public class towerMenuOption : MonoBehaviour
{
    // the menu script whose selected employee will be changing
    public playMenuHandler menuScript;

    // the index in playMenuHandler.availableEmployees
    // whose employee is displayed in this menu tile
    public int employeeIndex;

    void OnMouseDown(){
        menuScript.SetSelectedEmployee(employeeIndex);
    }
}
