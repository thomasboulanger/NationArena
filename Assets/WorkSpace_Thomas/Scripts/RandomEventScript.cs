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
    public bool SpawnAddFenceEventPossible = true;
    
    [Header("Lights off Event")]
    public LightsOffPresets LightOffEventPreset;
    public float FogFadeTime;
    [HideInInspector]public bool IsInLOEvent;

    [Header("Add random pillars")] 
    public GameObject Arena;
    public GameObject PillarPrefab;
    public int MinPillarsNumber;
    public int MaxPillarsNumber;
    private List<GameObject> ActivePillars = new List<GameObject>();

    [Space]
    //Privates
    private int CurrentPlayingEvent;
    [SerializeField] private float BaseTimer;

    float timer;

    private void Start()
    {
        timer = BaseTimer;
    }

    private void Update()
    {
        if (timer >= 0)
        {
            timer -= Time.deltaTime;
        }
        
        if (timer <= 0)
        {
            float chanceToLaunchEvent = Random.Range(0, 100);

            if (chanceToLaunchEvent <= PercentageForNewEvent) LaunchRandomEvent();
            
            timer = BaseTimer;
        }
    }

    void LaunchRandomEvent()
    {
        Debug.Log("Launching random event");
        
        float chanceToLaunchLOEvent = Random.Range(0, 100);
        float chanceToLaunchAFEvent = Random.Range(0, 100);
        float chanceToLaunchSPEvent = Random.Range(0, 100);

        if (chanceToLaunchLOEvent > chanceToLaunchAFEvent && chanceToLaunchLOEvent > chanceToLaunchSPEvent)if(LightsOutEventPossible) StartLightOffEvent();
        if (chanceToLaunchSPEvent > chanceToLaunchAFEvent && chanceToLaunchSPEvent > chanceToLaunchLOEvent)if(SpawnRandomPillarEventPossible) StartPillarSpawnEvent();
        if (chanceToLaunchAFEvent > chanceToLaunchSPEvent && chanceToLaunchAFEvent > chanceToLaunchLOEvent)if(SpawnRandomPillarEventPossible) StartAddFenceEvent();
    }

    [ContextMenu("Start lights off event")]
    public void StartLightOffEvent()
    {
        //determine la durée de l'évenement aléatoirement a chaque lancement
        float eventDuration = Random.Range(LightOffEventPreset.EventMinDuration, LightOffEventPreset.EventMaxDuration);

        if (CurrentPlayingEvent < MaxEventAtTheSameTime)
        {
            if (IsInLOEvent == false) StartCoroutine(LightsOffEvent(eventDuration));
        }
    }

    public void StartAddFenceEvent()
    {
        Debug.Log("Added Fences");
    }

    public void StartPillarSpawnEvent()
    {
        if (ActivePillars.Count > 3)
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
            Vector3 position = new Vector3(Random.Range(minX,maxX),5,Random.Range(minZ,maxZ));
            GameObject newPillar = Instantiate(PillarPrefab, position, quaternion.identity);
            ActivePillars.Add(newPillar);
        }
        
        Debug.Log("Spawned Pillars at random positions");
    }

    IEnumerator LightsOffEvent(float time)
    {
        CurrentPlayingEvent++;
        IsInLOEvent = true;
        FadeFog(true);
        RenderSettings.fog = true;
        RenderSettings.fogColor = LightOffEventPreset.FogColor;
        RenderSettings.haloStrength = LightOffEventPreset.HaloStrength;
        yield return new WaitForSeconds(time);
        FadeFog(false);
        RenderSettings.fog = false;
        IsInLOEvent = false;
        CurrentPlayingEvent--;
        yield return new WaitForSeconds(0.5f);
        RenderSettings.fog = false;
    }

    void FadeFog(bool state)
    {
        switch (state)
        {
            case true:
                RenderSettings.fogDensity = Mathf.Lerp(0, LightOffEventPreset.FogDensity, FogFadeTime);
                break;
            case false:
                RenderSettings.fogDensity = Mathf.Lerp(LightOffEventPreset.FogDensity, 0 , FogFadeTime);
                break;
        }
        

    }
}
