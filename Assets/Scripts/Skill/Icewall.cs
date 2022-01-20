using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Icewall : MonoBehaviour
{
    public static float distanceFromAnchor;
    
    public string description;
    [Space]
    public float DistanceFromAnchor;
    public List<GameObject> IcewallGameObjects = new List<GameObject>();
    public int nbSpike;
    public float speedOfSpikeSpawn;
    public int delayBeforeDestroy;
    
    private float _timer;
    private int _spawnedSpike;
    private float _distanceUnderGround;
    private float _growthSpeed;
    
    private void Awake()
    {
        distanceFromAnchor = DistanceFromAnchor;
    }
    
    private void Update()
    {
        if (GameController.inRound)
        {
            _timer += Time.deltaTime;
            if (_timer > speedOfSpikeSpawn)
            {
                _timer -= speedOfSpikeSpawn;
                if (_spawnedSpike < nbSpike)
                {
                    _spawnedSpike++;
                    int index = Random.Range(0, IcewallGameObjects.Count);
                    
                    GameObject go = Instantiate(IcewallGameObjects[index],transform.position + new Vector3(0,0,_spawnedSpike *.8f),quaternion.identity);
                    go.transform.SetParent(gameObject.transform);
                }
                else
                {
                    Destroy(gameObject,delayBeforeDestroy);
                }
            }
        }
    }
}
