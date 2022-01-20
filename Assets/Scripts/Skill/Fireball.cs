using System;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public static float distanceFromAnchor = 0f;

    public string description;
    [Space]
    public float DistanceFromAnchor;
    public float speed;
    public float damage;

    private Rigidbody _rb;

    private void Awake()
    {
        distanceFromAnchor = DistanceFromAnchor;
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        Destroy(gameObject,10);
    }

    private void Update()
    {
        _rb.AddForce(transform.forward);
    }
}
