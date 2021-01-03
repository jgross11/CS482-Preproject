using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmSelectionTile : MonoBehaviour
{
    
    public SwarmMenuHandler handler;
    public int index;

    void OnMouseEnter(){
        handler.UpdateHover(index);
    }

    void OnMouseDown(){
        if(handler.selectionToMakeIndex != -1){
            handler.UpdateSelectedTower(index);
        }   
    }

    void OnMouseExit(){
        handler.UpdateHover(-1);
    }
}
