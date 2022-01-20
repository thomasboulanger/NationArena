using UnityEngine;

public class SingleSpikeIce : MonoBehaviour
{
   private float _distanceUnderGround;
   private float _growthSpeed;
   private Vector3 _originalPos;
   private void Start()
   {
      _growthSpeed = 3f;
      _originalPos = transform.position;
      
      if (transform.name.Contains("1"))
      {
         _distanceUnderGround = -3.3f;
      }
      else if (transform.name.Contains("2"))
      {
         _distanceUnderGround = -3.7f;
      }
      else
      {
         _distanceUnderGround = -3.34f;
      }
      transform.localPosition = new Vector3(transform.localPosition.x,transform.localPosition.y + _distanceUnderGround,transform.localPosition.z);
   }

   private void Update()
   {
      if (transform.position.y != 0f)
      {
         transform.localPosition = Vector3.Lerp(transform.localPosition,_originalPos,_growthSpeed*Time.deltaTime);
      }
   }
}