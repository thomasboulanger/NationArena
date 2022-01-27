using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    [Header("General")]

    [Header("Intro/Outro")] 
    public List<AudioClip> IntroClips;
    public List<AudioClip> OutroClips;
    public AudioClip Rules;
    
    [Header("Music Object")]
    public GameObject Caster;
    public GameObject MusicPlayer;
    public GameObject Crowds;

    [Header("Background Sounds")]
    public List<AudioClip> BackgroundMusicClips;
    //public AudioClip HappyCrowd;
    //public AudioClip SadCrowd;
    
    [Header("Player Sounds")]
    public List<AudioClip> PlayerDeathClip;
    public List<AudioClip> PlayerWinClip;
    public List<AudioClip> PlayerOutClip;
    
    
    //Private
    private AudioSource CasterAudio;
    private AudioSource MusicPlayerAudio;
    //private AudioSource CrowdAudio;

    private void Start()
    {
        CasterAudio = Caster.GetComponent<AudioSource>();
        MusicPlayerAudio = MusicPlayer.GetComponent<AudioSource>();
        
        SelectNextBackgroundTack();
    }

    private void Update()
    {
        if (MusicPlayerAudio.clip == null)
        {
            SelectNextBackgroundTack();
        }
    }

    void SelectNextBackgroundTack()
    {
        int index = Random.Range(0, BackgroundMusicClips.Count);

        MusicPlayerAudio.PlayOneShot(BackgroundMusicClips[index]);
    }


    public void OnPlayerDeath(int PlayerNumber)
    {
        switch (PlayerNumber)
        {
            case 1:
                CasterAudio.PlayOneShot(PlayerDeathClip[0]);
                break;
            case 2:
                CasterAudio.PlayOneShot(PlayerDeathClip[1]);
                break;
            case 3:
                CasterAudio.PlayOneShot(PlayerDeathClip[2]);
                break;
            case 4:
                CasterAudio.PlayOneShot(PlayerDeathClip[3]);
                break;
        }
    }

    public void OnPlayerWin(int PlayerNumber)
    {
        switch (PlayerNumber)
        {
            case 1:
                CasterAudio.PlayOneShot(PlayerWinClip[0]);
                break;
            case 2:
                CasterAudio.PlayOneShot(PlayerWinClip[1]);
                break;
            case 3:
                CasterAudio.PlayOneShot(PlayerWinClip[2]);
                break;
            case 4:
                CasterAudio.PlayOneShot(PlayerWinClip[3]);
                break;
        }
    }
}
