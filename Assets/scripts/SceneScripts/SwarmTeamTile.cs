using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmTeamTile : MonoBehaviour
{
    // updated when hovered / selected
    private SpriteRenderer mySR;

    // used to update selection in swarm tower selection logic
    public SwarmMenuHandler handler;

    // index in @handler's selected tile array
    public int index;

    void Start(){
        mySR = GetComponent<SpriteRenderer>();
    }

    void OnMouseEnter(){
        if (mySR.color != Color.yellow) mySR.color = Color.gray;
    }

    void OnMouseDown(){
        handler.UpdateTeamTowerSelection(index, mySR);
        mySR.color = Color.yellow;
    }

    void OnMouseExit(){
        if (mySR.color != Color.yellow) mySR.color = Color.white;
    }
}
