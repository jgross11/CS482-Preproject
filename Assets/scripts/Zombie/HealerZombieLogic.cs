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

    // parent rigidbody
    public Rigidbody2D parentRGB;

    // dynamic rigidbody counter
    public int rgbyCount = 0;


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

        parentRGB = transform.parent.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // if the 2 frame fully-enabled rigidbody window is over
        if(parentRGB.bodyType == RigidbodyType2D.Dynamic && rgbyCount++ > 1){

            // apply a small force (doesn't actually move the object) so other rigidbodies can detect this one
            parentRGB.AddForce(Vector2.up);

            // change this zombie's rigidbody back to static to minimize lag
            parentRGB.bodyType = RigidbodyType2D.Static;

            // reset frame window counter
            rgbyCount = 0;
        }
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
        return timeMoved >= movementTime;
    }
    public override void Act(){

        // start 2 frame window where the zombie's rigidbody is detectable by other zombie healers
        // small window where rigidbody is fully enabled is necessary, as otherwise, other zombie healers would never detect 
        // this healer, even if it is in their scanning range, due to Static rigidbody <---> Static rigidbody collisions not being recorded
        parentRGB.bodyType = RigidbodyType2D.Dynamic;

        // initial target is this zombie's script
        Zombie target = zombieScript;

        // if there is at least one other zombie in range
        if(zombiesInRange.Count != 0){

            // iterate through zombies in range and determine zombie with least amount of health
            // TODO determine if healing should be prioritized by percentage or raw difference 

            // what a marvelous workaround this is
            // no guarantee that each Zombie script in dict will exist - if zombie has died since last action, will not show up in dict
            // however, if zombie dies during the scanning of the dict, the enumerator will throw an error due to value being removed during enumeration
            // so, cannot iterate directly through dict. Workaround: obtain a list of the dict's keys, iterate through that, and check if Zombie value is null
            // if so, safe to remove the key from dict, otherwise a valid Zombie reference was obtained!
            List<int> keyList = new List<int>(zombiesInRange.Keys);
            foreach(int key in keyList)
            {
                Zombie zInRangeScript = zombiesInRange[key];
                if(zInRangeScript == null) {
                    zombiesInRange.Remove(key);
                }
                else if(zInRangeScript.currentHealth < target.currentHealth){
                    target = zInRangeScript;
                }
            }
        }

        // heal the target by this zombie's healing value amount
        target.Heal(value);

        // healing animation on caster
        zombieScript.sprite.color = Color.cyan;

        // healing animation on target
        target.sprite.color = Color.cyan;

        // start animation end timer
        StartCoroutine(EndHealingAnimation(target));
    }

    public IEnumerator EndHealingAnimation(Zombie target){
        yield return new WaitForSeconds(0.3f);
        zombieScript.UpdateSprite();
        target.UpdateSprite();
    }

    void OnTriggerEnter2D(Collider2D coll){
        // if a new zombie comes within range
        if(coll.tag == "zombie" && !zombiesInRange.ContainsKey(coll.gameObject.GetInstanceID())){
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
}
