using UnityEngine;

public class SingleEarth : MonoBehaviour
{
   private Vector3 _originalPos;

   private void Start()
   {
      _originalPos = transform.position;
   }

   private void Update()
   {
      if (transform.position.y <= _originalPos.y + 1)
      {
         transform.position += new Vector3(0f, Time.deltaTime * 2,0f);
      }
   }
}
