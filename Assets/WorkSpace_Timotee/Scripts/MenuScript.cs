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
    private Canvas _canvas;
    public AudioMixer mixer;
    private void Start()
    {
        _canvas = FindObjectOfType<Canvas>();
    }

    public void MainToPlay()
    {
        _canvas.transform.Find("Main Menu").gameObject.SetActive(false);
        _canvas.transform.Find("Play Menu").gameObject.SetActive(true);
    }

    public void MainToSettings()
    {
        _canvas.transform.Find("Main Menu").gameObject.SetActive(false);
        _canvas.transform.Find("Settings Menu").gameObject.SetActive(true);
    }

    public void MainToCredits()
    {
        _canvas.transform.Find("Main Menu").gameObject.SetActive(false);
        _canvas.transform.Find("Credits").gameObject.SetActive(true);
    }

    public void PlayToMain()
    {
        _canvas.transform.Find("Play Menu").gameObject.SetActive(false);
        _canvas.transform.Find("Main Menu").gameObject.SetActive(true);
    }

    public void SettingsToMain()
    {
        _canvas.transform.Find("Settings Menu").gameObject.SetActive(false);
        _canvas.transform.Find("Main Menu").gameObject.SetActive(true);
    }

    public void CreditsToMain()
    {
        _canvas.transform.Find("Credits").gameObject.SetActive(false);
        _canvas.transform.Find("Main Menu").gameObject.SetActive(true);
    }

    public void SetVolume(float volume)
    {
        GameObject.Find("SliderText").GetComponent<Text>().text = "Volume: " + (int) (float) ((volume * 1.25f) + 100) + "%";
        mixer.SetFloat("MasterVolume", volume * 10);
    }
}
