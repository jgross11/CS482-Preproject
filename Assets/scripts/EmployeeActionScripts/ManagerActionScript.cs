using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerActionScript : ActionScript
{
    //array containing the objects collided with the circular raycast
    private RaycastHit2D[] hits;

    //the radius for the circular raycast
    public float radius;

    public override bool CanAct()
    {
        return true;
    }

   
    
    void Start()
    {
        //finds all objects on the employee layer
        hits = Physics2D.CircleCastAll(transform.position, radius, Vector3.right, Mathf.Infinity, 1 << LayerMask.NameToLayer("employee"));

        int attack = this.transform.parent.gameObject.GetComponent<Employee>().attack;

        //adds the boost to all non boost types
        foreach (RaycastHit2D h in hits)
        {
            if (h.collider.tag == "employee" && h.collider.gameObject.GetComponent<Employee>().type != "boost")
            {
                h.collider.gameObject.GetComponent<Employee>().attack += attack;
            }
        }
    }
    
    
    
    
    public override void Act(int attack)
    {

    }


    //when this tower is destroyed it will remove the boost from each tower it affected
    void OnDestroy()
    {
        hits = Physics2D.CircleCastAll(transform.position, radius, Vector3.right, Mathf.Infinity, 1 << LayerMask.NameToLayer("employee"));

        int attack = this.transform.parent.gameObject.GetComponent<Employee>().attack;

        foreach (RaycastHit2D h in hits)
        {
            if (h.collider.tag == "employee" && h.collider.gameObject.GetComponent<Employee>().type != "boost")
            {
                h.collider.gameObject.GetComponent<Employee>().attack -= attack;
            }
        }
    }

}
