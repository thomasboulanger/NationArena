using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    
    [Header("Intro/Outro")] 
    public List<AudioClip> IntroClips;
    public List<AudioClip> OutroClips;
    public AudioClip Rules;
    
    [Header("Background Sounds")]
    public List<AudioClip> BackgroundMusicClips;
   
    [FormerlySerializedAs("PlayerDeathClip")] [Header("Player Sounds")]
    public List<AudioClip> PlayerOutClip;
    public List<AudioClip> PlayerWinClip;
    
    public AudioSource CasterAudio;
    public AudioSource MusicPlayerAudio;

    [ContextMenu("NextBackgroundSound")]
    void SelectNextBackgroundTack()
    {
        int index = Random.Range(0, BackgroundMusicClips.Count);
        MusicPlayerAudio.volume = .2f;
        MusicPlayerAudio.PlayOneShot(BackgroundMusicClips[index]);
    }


    public void OnPlayerDeath(int PlayerNumber)
    {
        switch (PlayerNumber)
        {
            case 0:
                CasterAudio.PlayOneShot(PlayerOutClip[PlayerNumber]);
                break;
            case 1:
                CasterAudio.PlayOneShot(PlayerOutClip[PlayerNumber]);
                break;
            case 2:
                CasterAudio.PlayOneShot(PlayerOutClip[PlayerNumber]);
                break;
            case 3:
                CasterAudio.PlayOneShot(PlayerOutClip[PlayerNumber]);
                break;
        }
    }

    public void OnPlayerWin(int PlayerNumber)
    {
        switch (PlayerNumber)
        {
            case 0:
                CasterAudio.PlayOneShot(PlayerWinClip[PlayerNumber]);
                break;
            case 1:
                CasterAudio.PlayOneShot(PlayerWinClip[PlayerNumber]);
                break;
            case 2:
                CasterAudio.PlayOneShot(PlayerWinClip[PlayerNumber]);
                break;
            case 3:
                CasterAudio.PlayOneShot(PlayerWinClip[PlayerNumber]);
                break;
        }
    }

    [ContextMenu("Play Intro")]
    public void PlayRoundIntro()
    {
        int index = Random.Range(0, IntroClips.Count);
        CasterAudio.PlayOneShot(IntroClips[index]);
    }
    
    
    [ContextMenu("Play Outro")]
    public void PlayRoundOutro()
    {
        int index = Random.Range(0, OutroClips.Count);
        CasterAudio.PlayOneShot(OutroClips[index]);
    }
    
    
    [ContextMenu("Play Rules Audio")]
    public void PlayRulesAudio()
    {
        CasterAudio.PlayOneShot(Rules);
    }
}
