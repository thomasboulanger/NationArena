using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarScript : MonoBehaviour
{
    public GameObject Parent;
    
    private void Start()
    {
        RaycastHit hit;
        Physics.Linecast(gameObject.transform.position, new Vector3(gameObject.transform.position.x ,gameObject.transform.position.y - 100, gameObject.transform.position.z), out hit);

        if (hit.transform.name == "Water") Destroy(Parent);
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Water")Destroy(Parent);

        if (other.name == "HitBox") Parent.transform.parent = other.GetComponentInParent<Transform>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color =  Color.red;
        Gizmos.DrawLine(gameObject.transform.position, new Vector3(gameObject.transform.position.x ,gameObject.transform.position.y - 100, gameObject.transform.position.z));
    }
}
