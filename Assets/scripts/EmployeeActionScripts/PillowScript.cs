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
        health = this.gameObject.GetComponent<Employee>().currentHealth;
        maxHealth = this.gameObject.GetComponent<Employee>().maxHealth;
        ChangeSprite(2);
    }

    // Update is called once per frame
    void Update()
    {

        health = this.gameObject.GetComponent<Employee>().currentHealth;

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

        

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }


    void ChangeSprite(int i)
    {
        spriteRenderer.sprite = spriteArray[i]; 
    }


}
