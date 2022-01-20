using System;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static List<GameObject> spawnPoints = new List<GameObject>();
    public static List<GameObject> Skills = new List<GameObject>();
    public static bool inRound = true; //change to false in build
    public static int roundNumber;

    public List<GameObject> skillList = new List<GameObject>();

    private void Awake()
    {
        foreach (GameObject spawn in GameObject.FindGameObjectsWithTag("SpawnPoint"))
        {
            spawnPoints.Add(spawn.gameObject);
        }

        foreach (GameObject skill in skillList)
        {
            Skills.Add(skill);
        }
    }

    private void Start()
    {
       

        inRound = true;
        roundNumber = 0;
    }

    public void StartGame()
    {
        roundNumber++;
        
    }

    public void Pause()
    {
        
    }
    
}
