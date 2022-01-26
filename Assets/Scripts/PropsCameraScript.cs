using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropsCameraScript : MonoBehaviour
{
    private GameObject _target;

    public float distance;
    public float rotateSpeed;
    public float baseAngle;
    public float baseHeight;
    
    // Start is called before the first frame update
    void Start()
    {
        baseAngle = baseAngle / 180;
        _target = GameObject.FindWithTag("Arena");
        transform.position =
            new Vector3((float) Math.Sin(baseAngle * Math.PI) * distance, baseHeight, (float) Math.Cos(baseAngle * Math.PI) * distance);
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(_target.transform.position, Vector3.up, (rotateSpeed * Time.deltaTime));
        transform.LookAt(_target.transform);
        transform.Rotate(Vector3.up, 90);
    }
}
