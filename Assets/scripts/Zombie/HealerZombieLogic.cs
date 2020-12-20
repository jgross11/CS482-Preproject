using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealerZombieLogic : ZombieLogicScript
{

    // time for this tower to move after spawning
    public float movementTime;
    
    // time this tower has moved thus far: 0 <= this <= movementTime
    public float timeMoved;

    // time between this zombie trying to heal
    public float timeBetweenHeals;

    // time this tower has waited until next heal opportunity: 0 <= this <= timeBetweenHeals
    public float healCooldown;

    // dictionary that contains Zombie references to all zombies in healing range
    public Dictionary<int, Zombie> zombiesInRange;

    // this zombie's Zombie script
    public Zombie zombieScript;


    // Start is called before the first frame update
    void Start()
    {
        // init moved time / heal cooldown to 0
        timeMoved = 0;
        healCooldown = 0;

        // init dict
        zombiesInRange = new Dictionary<int, Zombie>();

        // obtain reference to this zombie
        zombieScript = transform.parent.GetComponent<Zombie>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // this healer simply walks forward for a predetermined period of time
    // and then stops (backliner) and searches for zombies to heal.
    public override bool CanMove(){
        return timeMoved < movementTime;
    }
    public override void Move(){
        timeMoved += Time.deltaTime;
        transform.parent.position += Vector3.left * Time.deltaTime * movementSpeed;
    }

    // this healer can act after it has stopped moving, so long as either it has lost health
    // or there is one zombie for it to heal TODO fix this tower healing when no one needs healed in an efficient manner
    public override bool CanAct(){
        return timeMoved >= movementTime && (zombiesInRange.Count != 0 || zombieScript.currentHealth != zombieScript.maxHealth);
    }
    public override void Act(){
        // initial target is this zombie's script
        Zombie target = zombieScript;

        // if there is at least one other zombie in range
        if(zombiesInRange.Count != 0){

            // iterate through zombies in range and determine zombie with least amount of health
            // TODO determine if healing should be prioritized by percentage or raw difference 
            foreach(KeyValuePair<int, Zombie> entry in zombiesInRange)
            {
                if(entry.Value.currentHealth < target.currentHealth){
                    target = entry.Value;
                }
            }
        }

        // heal the target by this zombie's healing value amount
        target.Heal(value);
    }

    void OnTriggerEnter2D(Collider2D coll){
        // if a new zombie comes within range
        if(coll.tag == "zombie"){

            // add zombie to dict for targeting purposes when actable
            zombiesInRange.Add(coll.gameObject.GetInstanceID(), coll.gameObject.GetComponent<Zombie>());
        }
    }

    void OnTriggerStay2D(Collider2D coll){
        // if a new zombie comes within range
        if(coll.tag == "zombie" && !zombiesInRange.ContainsKey(coll.gameObject.GetInstanceID())){

            // add zombie to dict for targeting purposes when actable
            zombiesInRange.Add(coll.gameObject.GetInstanceID(), coll.gameObject.GetComponent<Zombie>());
        }
    }

    void OnTriggerExit2D(Collider2D coll){
        // if a zombie leaves this zombie's range
        if(coll.tag == "zombie"){

            // remove zombie from dict
            zombiesInRange.Remove(coll.gameObject.GetInstanceID());
        }
    }
}
