using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth : MonoBehaviour
{
    public List<GameObject> rocks = new List<GameObject>();


    private void Start()
    {
        for (int i = 0; i < rocks.; i++)
        {
            
        }
        GameObject go = Instantiate(IcewallGameObjects[rand],
            Vector3.forward * _spawnedSpike * .8f, quaternion.identity);
    }
}
