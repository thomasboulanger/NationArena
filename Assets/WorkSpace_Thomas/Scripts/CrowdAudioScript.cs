using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CrowdAudioScript : MonoBehaviour
{
    public AudioClip NormalCrowd;
    public AudioClip CheerCrowd;
    public AudioClip DisapointedCrowd;
    [Space]
    public AudioSource CrowdSound;

    private void Start()
    {
        CrowdSound.clip = NormalCrowd;
        CrowdSound.loop = true;
    }

    [ContextMenu("Crowd Reaction")]
    public void CrowdReactionOnPlayerDeath()
    {
        int ChanceToSound = Random.Range(0, 100);
        int ChanceToCheer = Random.Range(0, 100);
        int DisapointedChance = Random.Range(0, 100);

        if (ChanceToSound >= 50)
        {
            if (ChanceToCheer > DisapointedChance) CrowdSound.PlayOneShot(CheerCrowd);
            else CrowdSound.PlayOneShot(DisapointedCrowd);
        }
    }
}
