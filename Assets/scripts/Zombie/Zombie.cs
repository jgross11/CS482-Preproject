using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{

    public int maxHealth, currentHealth, attack, level, experience;
    public float timeBetweenActions;
    public float movementSpeed;
    private bool isActive;
    public string type;
    public SpriteRenderer sprite;

    // unity doesn't like inheritance, so cannot be directly assigned
    private ZombieLogicScript zombieLogicScript;
    public float actionCooldown; 

    // Start is called before the first frame update
    void Start()
    {
        // bypass unity's anti-inheritance stance by attaching the concrete script to the zombie's child
        zombieLogicScript = this.gameObject.transform.GetChild(0).GetComponent<ZombieLogicScript>();

        // set action value
        zombieLogicScript.SetValue(attack);

        // set movement speed
        zombieLogicScript.SetMovementSpeed(movementSpeed);

        // is initially active
        isActive = true;

        // initially has full health
        currentHealth = maxHealth;

        // obtain reference to sprite renderer
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // if zombie is active
        if(isActive){

            // if the tower cannot yet act
            if(actionCooldown < timeBetweenActions){
                
                // decrease time until next action can occur
                actionCooldown += Time.deltaTime;

            // otherwise, if zombie can act
            } else if(zombieLogicScript.CanAct()){
                
                // execute action
                zombieLogicScript.Act();
                actionCooldown = 0;
            }
            
            // if zombie can move
            if(zombieLogicScript.CanMove()){
                
                // move zombie
                zombieLogicScript.Move();
            }
            
        }
    }

    // damages zombie by given amount
    // and handles death case, if necessary
    public void Damage(int value){
        
        // decrement health
        currentHealth -= value;

        // if zombie is (truly) dead
        if(currentHealth < 1){

            // destroy zombie gameobject
            Destroy(gameObject);
        }
        UpdateSprite();
    }

    // heals zombie by given amount
    // and handles overheal case, if necessary
    public void Heal(int value){
        currentHealth += value;

        if(currentHealth > maxHealth){
            currentHealth = maxHealth;
        }
        UpdateSprite();
    }

    // determines closeness to death of current zombie and displays this visually
    public void UpdateSprite(){
        sprite.color = new Color(1.0f, (float) currentHealth / (float) maxHealth, (float) currentHealth / (float) maxHealth);
    }

}
