using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Employee : MonoBehaviour
{

    public int maxHealth, currentHealth, attack, baseAttack, level, experience, experienceForNextLevel, cost;
    public float timeBetweenActions;
    private bool isActive;
    public string type;

    // unity doesn't like inheritance, so cannot be directly assigned
    private ActionScript actionScript;
    public float actionCooldown; 

    // Start is called before the first frame update
    void Start()
    {
        

        // bypass unity's anti-inheritance stance by attaching the concrete script to the tower's child
        actionScript = this.gameObject.transform.GetChild(0).GetComponent<ActionScript>();
        isActive = true;

        // initially has full health
        currentHealth = maxHealth;

        //base attack is used to store the original attack value before any stat update occurs by Manager or other tower
        baseAttack = attack;
    }

    // Update is called once per frame
    void Update()
    {
        // if the tower cannot yet act
        if(actionCooldown < timeBetweenActions){
            // decrease time until next action can occur
            actionCooldown += Time.deltaTime;
            
        // if the tower is ready to act and can act
        } else if(isActive && actionScript.CanAct()){

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
        currentHealth -= amount;
        if(currentHealth < 1){
            Destroy(gameObject);
        }
    }

    // handles addition of health by a given amount
    // as well as overheal, if necessary
    public void Heal(int val){
        // TODO determine if overheal is okay
        currentHealth = currentHealth + val > maxHealth ? maxHealth : currentHealth + val;
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
        maxHealth += 5;
        currentHealth += 5;
        attack += 2;
        experienceForNextLevel = (int)(experienceForNextLevel * 1.1f) + 2;
    }   

}
