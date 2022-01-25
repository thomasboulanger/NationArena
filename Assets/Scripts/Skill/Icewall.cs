using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Icewall : MonoBehaviour
{
    public List<GameObject> IcewallGameObjects = new List<GameObject>();
    public int nbSpike;
    public float speedOfSpikeSpawn;
    public int delayBeforeDestroy;
    
    private float _timer;
    private int _spawnedSpike;
    
    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > speedOfSpikeSpawn)
        {
            _timer -= speedOfSpikeSpawn;
            if (_spawnedSpike < nbSpike)
            {
                _spawnedSpike++;
                int rand = Random.Range(0, IcewallGameObjects.Count);

                GameObject go = Instantiate(IcewallGameObjects[rand],
                        Vector3.forward * _spawnedSpike * .8f, quaternion.identity);
                go.transform.SetParent(gameObject.transform);
            }
            else
            {
                Destroy(gameObject, delayBeforeDestroy);
            }
        }
    }
}
