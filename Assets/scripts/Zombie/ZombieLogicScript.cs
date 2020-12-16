using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ZombieLogicScript : MonoBehaviour
{

    // gameobject to target with this zombie's action
    public GameObject actionTarget;

    // action value 
    public int value;

    // movement speed
    public float movementSpeed;

    // is the zombie currently moving
    // public bool isMoving;

    // determines if zombie can move
    public abstract bool CanMove();

    // determines way in which zombie moves
    public abstract void Move();

    // determines if zombie can act
    public abstract bool CanAct();

    // determines way in which zombie acts
    public abstract void Act();

    // set the action value
    public void SetValue(int val){
        value = val;
    }

    // set the movement speed
    public void SetMovementSpeed(float val){
        movementSpeed = val;
    }
}
