using System;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    private List<GameObject> _players = new List<GameObject>();
    private void Start()
    {
        Destroy(gameObject,10);
    }

    private void Update()
    {
        if (_players.Count > 0)
        {
            foreach (GameObject player in _players)
            {
                Rigidbody rb = player.GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * 500 * rb.GetComponent<PlayerInputScript>().RepulseForceModifier * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player")) _players.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player")) _players.Remove(other.gameObject);
    }
}
