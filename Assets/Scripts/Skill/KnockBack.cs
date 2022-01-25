using UnityEngine;

public class KnockBack : MonoBehaviour
{
  private float _radius;
  private float _force;
  private Collider[] _colliders;
  private GameObject _caster;
  private bool _isDestroyable;
  
  public void Init(GameObject caster, float force, float radius, bool destroy)
  {
    _caster = caster;
    _force = force;
    _radius = radius;
    _isDestroyable = destroy;
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
    _colliders = Physics.OverlapSphere(transform.position, _radius);

    foreach (Collider nearby in _colliders)
    {
      PlayerInputScript player = nearby.GetComponent<PlayerInputScript>();
      if (player)
      {
        Rigidbody rb = player.GetComponent<Rigidbody>();
        rb.AddExplosionForce(_force * player.RepulseForceModifier,transform.position,_radius);
      }
    }

    if (_isDestroyable)
    {
      Destroy(gameObject);
    }
  }
}
