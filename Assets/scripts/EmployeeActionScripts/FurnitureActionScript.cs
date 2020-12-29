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

        //only spawn the pillow 1 time
        if (spawnPillow) {
            myPillow = Instantiate(pillow, transform.position + new Vector3(1, 0, 0), transform.rotation);
            spawnPillow = false;
        }

        //heal the pillow while the pillow exists
        if (!spawnPillow && myPillow != null)
        {
            myPillow.gameObject.GetComponent<Employee>().Heal(attack);

            // the employee gains experience by repairing his defense
            // TODO only heal (and give experience) if damage has been done to defense
            employeeScript.AddExperience(attack);
        }

    }
}
