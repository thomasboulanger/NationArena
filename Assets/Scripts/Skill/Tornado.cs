using System.Collections.Generic;
using UnityEngine;

public class Tornado : MonoBehaviour
{
    private Rigidbody _rb;
    private List<GameObject> _players = new List<GameObject>();

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        Destroy(gameObject,7);
    }

    private void Update()
    {
        _rb.AddForce(transform.forward);
        
        if (_players.Count > 0)
        {
            foreach (GameObject player in _players)
            {
                Rigidbody rb = player.GetComponent<Rigidbody>();
                Vector3 normalForce = (player.transform.position - transform.position).normalized;
                

                rb.AddForce(normalForce * -(500 * rb.GetComponent<PlayerInputScript>().RepulseForceModifier * Time.deltaTime));            }
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
