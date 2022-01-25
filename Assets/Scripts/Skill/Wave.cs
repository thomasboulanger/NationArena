using UnityEngine;

public class Wave : MonoBehaviour
{
    public float damage;

    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        Destroy(gameObject,4);
    }

    private void Update()
    {
        _rb.AddForce(transform.forward);
    }
}
