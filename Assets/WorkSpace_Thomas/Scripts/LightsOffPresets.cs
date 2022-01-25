using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LightsOffPreset", menuName = "ScriptableObjects/LightOffPreset", order = 1)]
public class LightsOffPresets : ScriptableObject
{
    [Header("Conditons")] 
    public float EventMinDuration;
    public float EventMaxDuration;
    public int MaxPlayerAliveToLaunch;
    
    [Header("Fog")]
    public Color FogColor;
    public float FogDensity;
    [Range(0,1)]public float HaloStrength;
}
