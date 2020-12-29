using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Employee : MonoBehaviour
{

    public int maxHealth, currentHealth, attack, level, experience, experienceForNextLevel, cost;
    public float timeBetweenActions;
    private bool isActive;
    public string type;
    private SpriteRenderer sprite;


    // unity doesn't like inheritance, so cannot be directly assigned
    private ActionScript actionScript;
    public float actionCooldown;

    //a radius value is now needed, should be updated if another boost tower is implemented with a larger radius than this, although, a radius of 5 seems to work well
    private int radiusForBoost = 5;

    // time to display level up animation
    private float levelUpAnimationTime = 1.0f;

    // period of time current animation has been played
    private float levelUpAnimationTimer = 0.0f;

    // true upon leveling up for a short period of time
    private bool shouldPlayLevelUpAnimation = false;

    // Start is called before the first frame update
    void Start()
    {

        // bypass unity's anti-inheritance stance by attaching the concrete script to the tower's child
        actionScript = this.gameObject.transform.GetChild(0).GetComponent<ActionScript>();
        isActive = true;

        // initially has full health
        currentHealth = maxHealth;


        //raycast to find any boost towers in a specific radius of the new towers position
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, radiusForBoost, Vector3.right, Mathf.Infinity, 1 << LayerMask.NameToLayer("employee"));

        //adds the boost to the attack value of the new tower for all boost towers already placed within a radius
        foreach(RaycastHit2D h in hits)
        {
            if(h.collider.gameObject.GetComponent<Employee>().type == "boost" && type != "boost")
            {
                attack += h.collider.gameObject.GetComponent<Employee>().attack;

            }
        }

        // obtain reference to this object's sprite
        sprite = GetComponent<SpriteRenderer>();
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

        // if recently leveled up
        if(shouldPlayLevelUpAnimation){

            // add time to timer
            levelUpAnimationTimer += Time.deltaTime;

            // assign random color to sprite
            sprite.color = new Color(Random.Range(0.7f, 1.0f), Random.Range(0.7f, 1.0f), Random.Range(0.7f, 1.0f));

            // if animation should end
            if(levelUpAnimationTimer > levelUpAnimationTime){

                // reset stats
                levelUpAnimationTimer = 0.0f;
                shouldPlayLevelUpAnimation = false;

                // draw appropriate sprite color based on health
                UpdateSprite();
            }
        }
    }

    // handles reduction of health by a given amount
    // as well as death case, if necessary
    public void TakeDamage(int amount){
        currentHealth -= amount;
        if(currentHealth < 1){
            Destroy(gameObject);
        }

        // reindicate wellness level visually
        UpdateSprite();
    }

    // handles addition of health by a given amount
    // as well as overheal, if necessary
    public void Heal(int val){
        // TODO determine if overheal is okay
        currentHealth = currentHealth + val > maxHealth ? maxHealth : currentHealth + val;

        // reindicate wellness level visually
        UpdateSprite();
    }

    // handles experience gaining by a given amount
    // as well as level-up case, if necessary
    public void AddExperience(int amount){
        experience += amount;
        if(experience >= experienceForNextLevel){
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

        // play animation
        shouldPlayLevelUpAnimation = true;
    }   

    // determines closeness to death of current employee and displays this visually
    public void UpdateSprite(){
        sprite.color = new Color(1.0f, (float) currentHealth / (float) maxHealth, (float) currentHealth / (float) maxHealth);
    }

}
