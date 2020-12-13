using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Employee : MonoBehaviour
{

    public int health, attack, level, experience, experienceForNextLevel, cost;
    public float timeBetweenActions;
    private bool isActive;

    // unity doesn't like inheritance, so cannot be directly assigned
    private ActionScript actionScript;
    private float actionCooldown; 

    // Start is called before the first frame update
    void Start()
    {
        // can act immediately
        actionCooldown = timeBetweenActions;

        // bypass unity's anti-inheritance stance by attaching the concrete script to the tower's child
        actionScript = this.gameObject.transform.GetChild(0).GetComponent<ActionScript>();
        isActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        // if the tower cannot yet act
        if(actionCooldown < timeBetweenActions){
            // decrease time until next action can occur
            actionCooldown += Time.deltaTime;

        // if the tower is ready to act and can act
        } else if(actionScript.CanAct() && isActive){

            // act, and then reset action cooldown
            // TODO determine if attack is always 
            // the scaling stat
            actionScript.Act(attack);
            actionCooldown = 0;
        }
    }

    // handles reduction of health by a given amount
    // as well as death case, if necessary
    public void TakeDamage(int amount){
        health -= amount;
        if(health < 1){
            Destroy(gameObject);
        }
    }

    // handles experience gaining by a given amount
    // as well as level-up case, if necessary
    public void AddExperience(int amount){
        experience += amount;
        if(experience > experienceForNextLevel){
            LevelUp();
        }
    }

    // dictates level up logic
    public void LevelUp(){
        // increment level
        level++;

        // keep excess experience
        experience -= experienceForNextLevel;

        // TODO balance / individualize this
        health += 5;
        attack += 2;
        experienceForNextLevel = (int)(experienceForNextLevel * 1.1f);
    }   

}
