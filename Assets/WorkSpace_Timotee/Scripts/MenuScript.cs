using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public AudioMixer mixer;

    public void SetVolume(float volume)
    {
        mixer.SetFloat("MasterVolume", volume);
        GameObject.Find("SliderText").GetComponent<Text>().text = "Volume: " + (int) ((volume * 1.25f) + 100) + "%";
        
    }

    public void SetFullscreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
    }

    public void SetResolution(float resolution)
    {
        switch ((int) resolution)
        {
            case 0:
                Screen.SetResolution(640,360,Screen.fullScreen);
                break;
            case 1:
                Screen.SetResolution(800,600,Screen.fullScreen);
                break;
            case 2:
                Screen.SetResolution(1280,720,Screen.fullScreen);
                break;
            case 3:
                Screen.SetResolution(1600,900,Screen.fullScreen);
                break;
            case 4:
                Screen.SetResolution(1920,1080,Screen.fullScreen);
                break;
        }
    }
}
