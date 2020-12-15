using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// handles logic for tower-placeable tile on the map
public class towerTile : MonoBehaviour
{

    // menu script used when creating a tower
    public playMenuHandler menuScript;

    // the tile's spriterenderer
    public SpriteRenderer spriteRenderer;
    
    // the current tower occupying this tile
    public GameObject tower;

    void Start(){

        // get this sprite renderer for hovering purposes
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnMouseEnter(){
        // change color if a tower can be added here
        if(tower == null && menuScript.GetSelectedEmployee() != null && menuScript.CanAffordTower()){
            spriteRenderer.color = Color.gray;
        }
    }

    void OnMouseExit(){
        // revert color back to normal
        spriteRenderer.color = Color.white;
    }

    void OnMouseOver(){
        // continuously check if a tower can be added here
        if(tower == null && menuScript.GetSelectedEmployee() != null && menuScript.CanAffordTower()){
            spriteRenderer.color = Color.gray;
        } 
    }

    // TODO think about this
    bool CanPlaceHere(){
        return false;
    }

    void OnMouseDown(){
        // if a tower can be added here
        if(tower == null && menuScript.GetSelectedEmployee() != null && menuScript.CanAffordTower()){
            // create a copy of the selected employee at this position
            tower = Instantiate(menuScript.CreateSelectedEmployee(), gameObject.transform.position, gameObject.transform.rotation, gameObject.transform);

            // temporarily disable the tile collider to ensure tower is clicked
            GetComponent<Collider2D>().enabled = false;

            // revert tile color to normal 
            spriteRenderer.color = Color.white;

            // deselect tower in menu if no more can be afforded
            if(!menuScript.CanAffordTower()){
                menuScript.Deselect();
            }
        }
        
    }
}
