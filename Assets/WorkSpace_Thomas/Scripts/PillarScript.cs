using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarScript : MonoBehaviour
{
    public GameObject Parent;

    private RandomEventScript EventManager;
    
    private void Start()
    {
        RaycastHit hit;
        Physics.Linecast(gameObject.transform.position, new Vector3(gameObject.transform.position.x ,gameObject.transform.position.y - 100, gameObject.transform.position.z), out hit);

        if (hit.transform.name == "Water") Destroy(Parent);

        EventManager = FindObjectOfType<RandomEventScript>().GetComponent<RandomEventScript>();
        
        if (EventManager.PillarGoToCellCenter) if (hit.transform.name == "HitBox") 
            Parent.transform.position = new Vector3(hit.transform.position.x, Parent.transform.position.y, hit.transform.position.z);

        if (hit.transform.CompareTag("Pillar")) Destroy(Parent);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Water")Destroy(Parent);

        if (other.name == "HitBox") Parent.transform.parent = other.transform.parent.transform.GetChild(0);

        if (other.CompareTag("Pillar")) Destroy(Parent);

        //if (other.CompareTag("Player"))
        //{
            //si le pillier tombe sur un joueur il peut lui faire des dégats
            //if (other.transform.position.y < gameObject.transform.position.y) {}
        //}
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color =  Color.red;
        Gizmos.DrawLine(gameObject.transform.position, new Vector3(gameObject.transform.position.x ,gameObject.transform.position.y - 100, gameObject.transform.position.z));
    }

    private void OnDestroy()
    {
        EventManager.ActivePillars.Remove(Parent);
    }
}
