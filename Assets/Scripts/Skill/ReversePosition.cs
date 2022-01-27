using UnityEngine;

public class ReversePosition : MonoBehaviour
{
    private Rigidbody _rb;
    private GameObject _caster;

    public void Init(GameObject caster)
    {
        _caster = caster;
    }
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        Destroy(gameObject,15);
    }

    private void Update()
    {
        _rb.AddForce(transform.forward / 1.5f);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            (_caster.transform.position, other.transform.position) = (other.transform.position, _caster.transform.position);
            Destroy(gameObject);
        }
    }
}
