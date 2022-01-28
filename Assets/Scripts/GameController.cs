using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static List<GameObject> spawnPoints = new List<GameObject>();
    public static List<GameObject> Skills = new List<GameObject>();
    public static List<GameObject> HpBars = new List<GameObject>();
    public static List<GameObject> alivePlayers = new List<GameObject>();
    public List<GameObject> readyUI = new List<GameObject>();
    public static bool inRound;
    public GameObject endPanel;

    [SerializeField] private GameObject _mainMenuUI;
    [SerializeField] private AudioManager _audioManager;

    public List<GameObject> skillList = new List<GameObject>();
    private int alivePlayer;
    private bool _trigger, _deletePlayer;

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
        foreach (GameObject hpBar in GameObject.FindGameObjectsWithTag("HealthBar"))
        {
            HpBars.Add(hpBar);
        }
    }

    private void Update()
    {
        if (inRound)
        {
            GameObject tmpPlayer = new GameObject();
            foreach (GameObject player in alivePlayers)
            {
                if (player.GetComponent<PlayerInputScript>().isDead)
                {
                    alivePlayer--;
                    tmpPlayer = player;
                    _deletePlayer = true;
                    //alivePlayers.Remove(player);
                }
            }

            if (_deletePlayer)
            {
                _deletePlayer = false;
                alivePlayers.Remove(tmpPlayer);
            }

            
            if (alivePlayer == 1 && !_trigger)
            {
                OnePlayerWin();
                
            }
        }
    }

    private void OnePlayerWin()
    {
        _audioManager.OnPlayerWin(alivePlayers[0].GetComponent<PlayerInputScript>().playerIndex);
        _trigger = true;
        endPanel.SetActive(true);
        //inRound = false;
    }

    public void StartPlay()
    {
        foreach (GameObject player in PlayerInputScript.playerList)
        {
            alivePlayers.Add(player);
        }
        alivePlayer =  alivePlayers.Count;
        inRound = true;
    }
    
    public void PlayAgain()
    {
        foreach (GameObject player in PlayerInputScript.playerList)
        {
            player.GetComponent<PlayerInputScript>().PlayAgain();
        }
        alivePlayer =  alivePlayers.Count;
        inRound = true;
        _trigger = false;
        endPanel.SetActive(false);
    }
    
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void PlayerReady(int index)
    {
        readyUI[index].SetActive(true);
    }
}
