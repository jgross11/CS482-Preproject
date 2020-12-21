using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillowScript : MonoBehaviour
{
    //first sprite
    public SpriteRenderer spriteRenderer;

    //storing all of the different possible sprites
    public Sprite[] spriteArray = new Sprite[3];

    //current health
    private int health;

    //maximum health
    private int maxHealth;


    // Start is called before the first frame update
    void Start()
    {

        //set this health to the health of the pillow, and the original sprite to the first sprite
        health = this.gameObject.GetComponent<Employee>().currentHealth;
        maxHealth = this.gameObject.GetComponent<Employee>().maxHealth;
        ChangeSprite(2);
    }

    // Update is called once per frame
    void Update()
    {

        //update this health based on the new health
        health = this.gameObject.GetComponent<Employee>().currentHealth;

        //change the sprite based on its health
        if (health >= 6)
        {
            ChangeSprite(2);
        }else if(health<6 && health >= 3)
        {
            ChangeSprite(1);
        }else if (health < 3)
        {
            ChangeSprite(0);
        }

        
        //destory the object if its health is at 0
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }


    //function to change the sprite of the pillow
    void ChangeSprite(int i)
    {
        spriteRenderer.sprite = spriteArray[i]; 
    }


}
