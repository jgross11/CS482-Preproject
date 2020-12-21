using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureActionScript : ActionScript
{

    //bool so that pillow is only spawned once
    private bool spawnPillow = true;

    //pillow game object
    public GameObject pillow;

    private GameObject myPillow;

    public override bool CanAct()
    {
        return true;
    }

    public override void Act(int attack)
    {

        if (spawnPillow) {
            myPillow = Instantiate(pillow, transform.position + new Vector3(1, 0, 0), transform.rotation);
            spawnPillow = false;
        }

        if (!spawnPillow && myPillow != null)
        {

            Debug.Log(myPillow.gameObject.GetComponent<Employee>().currentHealth);

            myPillow.gameObject.GetComponent<Employee>().Heal(attack);

            Debug.Log(myPillow.gameObject.GetComponent<Employee>().currentHealth);
        }

    }
}
