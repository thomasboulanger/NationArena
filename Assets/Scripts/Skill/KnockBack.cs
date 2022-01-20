using System.Runtime.InteropServices;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
  public float radius;
  public float force;
  
  private Collider[] _colliders;
  private GameObject _caster;

  public void Init(GameObject caster)
  {
    _caster = caster;
  }
  
  private void OnTriggerEnter(Collider other) 
  {
    if (other != _caster)
    {
      Knockback();
    }
  }

  private void Knockback()
  {
    _colliders = Physics.OverlapSphere(transform.position, radius);

    foreach (Collider nearby in _colliders)
    {
      PlayerInputScript player = nearby.GetComponent<PlayerInputScript>();
      if (player)
      {
        Rigidbody _rb = player.GetComponent<Rigidbody>();
        _rb.AddExplosionForce(force,transform.position,radius);
        
      }
    }
    Destroy(gameObject);
  }
}
