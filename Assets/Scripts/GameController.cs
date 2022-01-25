using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static List<GameObject> spawnPoints = new List<GameObject>();
    public static List<GameObject> Skills = new List<GameObject>();
    public static bool inRound = true; //change to false in build
    public static int roundNumber;

    public List<GameObject> skillList = new List<GameObject>();
    
    private void Awake()
    {
        roundNumber = 0;
        foreach (GameObject skill in skillList)
        {
            Skills.Add(skill);
        }
        foreach (GameObject spawn in GameObject.FindGameObjectsWithTag("SpawnPoint"))
        {
            spawnPoints.Add(spawn.gameObject);
        }
        inRound = true;
    }
}
