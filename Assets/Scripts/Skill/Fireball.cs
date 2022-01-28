using System;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float damage;

    private Rigidbody _rb;

  
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        Destroy(gameObject,10);
    }

    private void Update()
    {
        _rb.AddForce(transform.forward * 2);
    }
}
