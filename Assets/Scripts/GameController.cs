using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static List<GameObject> spawnPoints = new List<GameObject>();
    public static List<GameObject> Skills = new List<GameObject>();
    public static bool inRound = false;

    [SerializeField] private GameObject _mainMenuUI;
    [SerializeField] private AudioManager _audioManager;

    public List<GameObject> skillList = new List<GameObject>();
    
    private void Awake()
    {
        foreach (GameObject skill in skillList)
        {
            Skills.Add(skill);
        }
        foreach (GameObject spawn in GameObject.FindGameObjectsWithTag("SpawnPoint"))
        {
            spawnPoints.Add(spawn.gameObject);
        }
    }

    public void StartPlay()
    {
        
        inRound = true;
    }
}
