using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JamesActionScript : ActionScript
{
    public GameObject essence;

    // list of essences created by this tower
    private List<GameObject> essences;

    public override void Start(){
        essences = new List<GameObject>();
        employeeScript = transform.parent.GetComponent<Employee>();
    }

    // James can generate essences as soon as possible,
    // so this method just returns true
    public override bool CanAct()
    {
        return true;
    }

    // James act by creating essences to be collected by the player
    public override void Act(int attack)
    {
        // create essences game object
        GameObject createdEssence = Instantiate(essence, transform.position - Vector3.forward, transform.rotation);

        // set created essence employee reference, worth value
        createdEssence.GetComponent<JamesEssenceScript>().SetValues(employeeScript, attack/3 + 1);

        // add essence to list of essences this tower created
        essences.Add(createdEssence);
    }

    void OnDestroy(){

        // must destroy any created essences upon destruction of this tower
        foreach(GameObject createdEssence in essences)
        {
            Destroy(createdEssence);
        }
    }
}
