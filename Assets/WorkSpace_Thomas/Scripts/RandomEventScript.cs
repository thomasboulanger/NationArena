using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomEventScript : MonoBehaviour
{
    [Header("General Event")] 
    [Range(1,10)]public int MaxEventAtTheSameTime;
    [Range(0, 100)] public int PercentageForNewEvent = 35;

    [Header("Possible Event")] 
    public bool LightsOutEventPossible = true;
    public bool SpawnRandomPillarEventPossible = true;

    [Header("Lights off Event")] 
    public float MinLOEventTime, MaxLOEventTime;
    public Color AmbiantLightBaseColor;
    public GameObject LightBlockerPrefab;
    [HideInInspector]public bool IsInLOEvent;

    [Header("Add random pillars")] 
    public GameObject Arena;
    public int MinPillarsNumber;
    public int MaxPillarsNumber;
    public bool PillarGoToCellCenter;
    public List<GameObject> PillarPrefabs;

    [HideInInspector]public List<GameObject> ActivePillars = new List<GameObject>();

    [Space]
    //Privates
    private int CurrentPlayingEvent;
    [SerializeField] private float BaseTimer;
    private MaterialPropertyModifier _hexagonModifier;

    float timer;
    bool gameStarted;

    private void Start()
    {
        timer = BaseTimer;

        _hexagonModifier = FindObjectOfType<MaterialPropertyModifier>();
    }

    private void Update()
    {
        if (gameStarted)
        {
            if (timer >= 0)
            {
                timer -= Time.deltaTime;
            }
        }
        if (timer <= 0)
        {
            float chanceToLaunchEvent = Random.Range(0, 100);

            if (chanceToLaunchEvent <= PercentageForNewEvent) LaunchRandomEvent();
            
            timer = BaseTimer;
        }
        
        CheckForFenceDestroy();
    }

    void LaunchRandomEvent()
    {
        Debug.Log("Launching random event");
        
        float chanceToLaunchLOEvent = Random.Range(0, 100);
        float chanceToLaunchSPEvent = Random.Range(0, 100);

        if (chanceToLaunchLOEvent > chanceToLaunchSPEvent)if(LightsOutEventPossible) StartLightOffEvent();
        
        if (chanceToLaunchSPEvent > chanceToLaunchLOEvent)if(SpawnRandomPillarEventPossible) StartPillarSpawnEvent();
    }

    [ContextMenu("Start lights off event")]
    public void StartLightOffEvent()
    {
        //determine la durée de l'évenement aléatoirement a chaque lancement
        float eventDuration = Random.Range(MinLOEventTime, MaxLOEventTime);

        if (CurrentPlayingEvent < MaxEventAtTheSameTime) { if (IsInLOEvent == false) StartCoroutine(LightsOffEvent(eventDuration));}
    }

    void CheckForFenceDestroy()
    {
        if (_hexagonModifier._actualLayer < 3)
        {
            GameObject fenceHolder = GameObject.FindGameObjectWithTag("Fence");
            
            Destroy(fenceHolder);
        }
    }

    public void StartPillarSpawnEvent()
    {
        if (ActivePillars.Count > 2)
        {
            for (int i = 0; i < ActivePillars.Count; i++)
            {
                Destroy(ActivePillars[i]);
                ActivePillars.Remove(ActivePillars[i]);
            }
        }

        int NumberOfPillarsToSpawn = Random.Range(MinPillarsNumber, MaxPillarsNumber);

        
        
        float minX = 0, maxX = 0, minZ = 0, maxZ = 0;

        for (int i = 0; i < Arena.transform.childCount; i++)
        {
            if (Arena.transform.GetChild(i).transform.position.x < minX) minX = Arena.transform.GetChild(i).transform.position.x;
            if (Arena.transform.GetChild(i).transform.position.x > maxX) maxX = Arena.transform.GetChild(i).transform.position.x;
            if (Arena.transform.GetChild(i).transform.position.z < minZ) minZ = Arena.transform.GetChild(i).transform.position.z;
            if (Arena.transform.GetChild(i).transform.position.z > maxZ) maxZ = Arena.transform.GetChild(i).transform.position.z;
        }

        for (int i = 0; i < NumberOfPillarsToSpawn; i++)
        {
            int prefabsIndex =Random.Range(0, PillarPrefabs.Count) ;
            Vector3 position = new Vector3(Random.Range(minX,maxX),Random.Range(10,30),Random.Range(minZ,maxZ));
            GameObject newPillar = Instantiate(PillarPrefabs[prefabsIndex], position, quaternion.identity);
            ActivePillars.Add(newPillar);
        }
        
        Debug.Log("Spawned Pillars at random positions");
    }

    IEnumerator LightsOffEvent(float time)
    {
        CurrentPlayingEvent++;
        IsInLOEvent = true;
        RenderSettings.ambientLight = Color.black;
        GameObject newObj = Instantiate(LightBlockerPrefab);
        yield return new WaitForSeconds(time);
        RenderSettings.ambientLight = AmbiantLightBaseColor;
        Destroy(newObj);
        IsInLOEvent = false;
        CurrentPlayingEvent--;
        yield return new WaitForSeconds(0.5f);
    }

    public void GameStarted()
    {
        gameStarted = true;
    }
}
