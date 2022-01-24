using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomEventScript : MonoBehaviour
{
    [Header("General Event")] 
    [Range(1,10)]public int MaxEventAtTheSameTime;
    [Range(0, 100)] public int PercentageForNewEvent = 35;
    
    [Header("Lights off Event")]
    public LightsOffPresets LightOffEventPreset;
    public float FogDensityFadeTime = 0.5f;
    [HideInInspector]public bool IsInLOEvent;
    
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

        if (chanceToLaunchLOEvent > chanceToLaunchAFEvent && chanceToLaunchLOEvent > chanceToLaunchSPEvent) StartLightOffEvent();
        if (chanceToLaunchSPEvent > chanceToLaunchAFEvent && chanceToLaunchSPEvent > chanceToLaunchLOEvent) StartPillarSpawnEvent();
        if (chanceToLaunchAFEvent > chanceToLaunchSPEvent && chanceToLaunchAFEvent > chanceToLaunchLOEvent) StartAddFenceEvent();
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
        Debug.Log("Spawned Pillars at random positions");
    }

    private float _elapsedTime = 0;
    IEnumerator LightsOffEvent(float time)
    {
        RenderSettings.fogDensity = 0;
        CurrentPlayingEvent++;
        //L'evenement est en cours temps que le timer n'est pas à 0
        IsInLOEvent = true;
        RenderSettings.fog = true;
        RenderSettings.fogDensity = LightOffEventPreset.FogDensity;

        while (_elapsedTime < FogDensityFadeTime)
        {
            _elapsedTime += Time.deltaTime;
            RenderSettings.fogDensity = Mathf.Lerp(RenderSettings.fogDensity, LightOffEventPreset.FogDensity, _elapsedTime * FogDensityFadeTime);
            break;
        }
        
        RenderSettings.fogColor = LightOffEventPreset.FogColor;
        RenderSettings.haloStrength = LightOffEventPreset.HaloStrength;
        yield return new WaitForSeconds(time);
        RenderSettings.fog = false;
        IsInLOEvent = false;
        CurrentPlayingEvent--;
    }
    
}
