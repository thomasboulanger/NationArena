using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Earth : MonoBehaviour
{
    public List<GameObject> rocks = new List<GameObject>();
    private GameObject _caster;
    
    private void Start()
    {
        Destroy(gameObject,1.5f);
        for (int i = 0; i < rocks.Count; i++)
        {
            GameObject go = Instantiate(rocks[i], transform.position - transform.up, quaternion.identity);
            go.transform.SetParent(transform);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != _caster && other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.AddExplosionForce(500 * other.GetComponent<PlayerInputScript>().RepulseForceModifier,transform.position,5);
        }
    }

    public void Init(GameObject player)
    {
        _caster = player;
    }
}
