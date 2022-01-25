using UnityEngine;

public class Meteor : MonoBehaviour
{
    public float damage;
    private Collider[] _colliders;


    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        Destroy(gameObject,15);
    }

    private void Update()
    {
        _rb.AddForce(transform.forward);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        _colliders = Physics.OverlapSphere(transform.position, 8);

        foreach (Collider nearby in _colliders)
        {
            PlayerInputScript player = nearby.GetComponent<PlayerInputScript>();
            if (player)
            {
                Rigidbody rb = player.GetComponent<Rigidbody>();
                rb.AddExplosionForce(1000 * player.RepulseForceModifier,transform.position,8);
            }
        } 
        Destroy(gameObject);
    }
}
