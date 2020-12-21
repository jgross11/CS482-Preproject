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

    public override void Act(int attack)
    {

        hits = Physics2D.CircleCastAll(transform.position, radius, Vector3.right, Mathf.Infinity, 1 << LayerMask.NameToLayer("employee"));
        int count = 0;
        foreach(RaycastHit2D h in hits)
        {

            count++;
            

            if(h.collider.tag == "employee" && h.collider.gameObject.GetComponent<Employee>().type != "manager")
            {
                h.collider.gameObject.GetComponent<Employee>().attack = h.collider.gameObject.GetComponent<Employee>().baseAttack + attack;
            }
        }
        Debug.Log(count);
    }

    void OnDestroy()
    {
        hits = Physics2D.CircleCastAll(transform.position, radius, Vector3.right, Mathf.Infinity, 1 << LayerMask.NameToLayer("employee"));

        foreach(RaycastHit2D h in hits)
        {
            if (h.collider.tag == "employee")
            {
                h.collider.gameObject.GetComponent<Employee>().attack = h.collider.gameObject.GetComponent<Employee>().baseAttack;
            }
        }
    }

}
