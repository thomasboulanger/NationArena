using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerInputScript : MonoBehaviour
{
    public static List<GameObject> playerList = new List<GameObject>();

    public float speed = 1f;
    public GameObject anchorGround;
    public GameObject anchor;
    public GameObject visualGameObject;
    public GameObject animatorGameObject;
    public Material[] playerMat;

    [HideInInspector] public float RepulseForceModifier = 1;
    [HideInInspector] public bool isDead;
    [HideInInspector] public int playerIndex;
    
    [SerializeField]
    private List<GameObject> _hpBars = new List<GameObject>();
    private  List<GameObject> cooldownList = new List<GameObject>();
    private Animator _animator;
    private Rigidbody _rigidbody;
    private Vector2 _inputVector;
    private string _elementInMemory = "";
    private float speedModifier;
    private bool _isFireUp, _isEarthUp, _isWaterUp, _isWindUp;
    private Image _fireImg, _earthImg, _waterImg, _windImg;
    [SerializeField]
    private float 
        _fireCooldown,
        _earthCooldown,
        _waterCooldown,
        _windCooldown,
        _fireTimer,
        _earthTimer,
        _waterTimer,
        _windTimer;

    private bool _trigger;
    private bool _dontCallPlayerOut;

    void Start()
    {
        playerList.Add(gameObject);
        _animator = animatorGameObject.GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        isDead = false;
        foreach (GameObject cd in GameObject.FindGameObjectsWithTag("Cooldown"))
        {
            cooldownList.Add(cd);
        }
        for (int i = 0; i < playerList.Count; i++) 
        {
            if (gameObject == playerList[i])
            {
                playerIndex = i;
                foreach (GameObject hpBar in  GameController.HpBars)
                {
                    if (hpBar.transform.name.Contains((i + 1).ToString())) transform.GetComponent<HealthBar>().Init(hpBar);
                }
                foreach (GameObject cd in cooldownList)
                {
                    if (cd.transform.name.Contains((i + 1).ToString()) && cd.transform.name.Contains("Fire"))
                        _fireImg = cd.GetComponent<Image>();
                    if (cd.transform.name.Contains((i + 1).ToString()) && cd.transform.name.Contains("Earth"))
                        _earthImg = cd.GetComponent<Image>();
                    if (cd.transform.name.Contains((i + 1).ToString()) && cd.transform.name.Contains("Water"))
                        _waterImg = cd.GetComponent<Image>();
                    if (cd.transform.name.Contains((i + 1).ToString()) && cd.transform.name.Contains("Wind"))
                        _windImg = cd.GetComponent<Image>();
                }
                transform.position = GameController.spawnPoints[i].transform.position;
                transform.name = "Player " + (i + 1);
                visualGameObject.transform.GetComponent<Renderer>().material = playerMat[i];
                
            }
        }
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().PlayerReady(playerIndex);
        speedModifier = 1;
    }

    void Update()
    {
        if (GameController.inRound && !isDead)
        {
            MovePlayer();
            if (!_isFireUp) _isFireUp = DecrementCooldown(_fireCooldown, ref _fireTimer);
            if (!_isEarthUp) _isEarthUp = DecrementCooldown(_earthCooldown, ref _earthTimer);
            if (!_isWaterUp) _isWaterUp = DecrementCooldown(_waterCooldown, ref _waterTimer);
            if (!_isWindUp) _isWindUp = DecrementCooldown(_windCooldown, ref _windTimer);

            _fireImg.fillAmount = _fireTimer/_fireCooldown;
            _earthImg.fillAmount = _earthTimer/_earthCooldown;
            _waterImg.fillAmount =   _waterTimer/_waterCooldown;
            _windImg.fillAmount =   _windTimer/_windCooldown;
        }
        if (isDead && !_trigger) PlayerDead();
    }
    
    private void MovePlayer()
    {
        Vector3 tmpVec = new Vector3(_inputVector.x, 0f, _inputVector.y);
        tmpVec = tmpVec.normalized * speed * speedModifier * Time.deltaTime;
        _rigidbody.MovePosition(transform.position + tmpVec);

        if (_rigidbody.velocity.magnitude > 5f)
        {
            _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, 5f);
        }

        if (_inputVector != Vector2.zero)
        {
            transform.forward = new Vector3(transform.forward.x + tmpVec.x, transform.forward.y,
                transform.forward.z + tmpVec.z);
            _animator.SetBool("isMoving",true);
        }
        else
        {
            _rigidbody.angularVelocity = Vector3.zero;
            _animator.SetBool("isMoving",false);
        }
    }
    
    private bool DecrementCooldown(float cooldown, ref float timer)
    {
        if (timer <= cooldown)
        {
            timer += Time.deltaTime;
            return false;
        }
        timer = 0f;
        return true;
    }
    
    private void OnMovement(InputValue value)
    {
         _inputVector = value.Get<Vector2>();
    }

    private void OnSelectElementTop()
    { //fire
        _elementInMemory += "F";
        if (_elementInMemory.Length > 2) _elementInMemory = _elementInMemory.Substring(1);
        if (_elementInMemory == "FF") _elementInMemory = "F";
    }
    private void OnSelectElementBottom()
    { //earth
        _elementInMemory += "E";
        if (_elementInMemory.Length > 2) _elementInMemory = _elementInMemory.Substring(1);
        if (_elementInMemory == "EE") _elementInMemory = "E";
    }
    private void OnSelectElementLeft()
    { //water
        _elementInMemory += "W";
        if (_elementInMemory.Length > 2) _elementInMemory = _elementInMemory.Substring(1);
        if (_elementInMemory == "WW") _elementInMemory = "W";
    }
    private void OnSelectElementRight()
    { //wind
        _elementInMemory += "I";
        if (_elementInMemory.Length > 2) _elementInMemory = _elementInMemory.Substring(1);
        if (_elementInMemory == "II") _elementInMemory = "I";
    }

    private void OnCastSpell()
    {
        switch (_elementInMemory)
        {
            case "":
                break;
            case "F":
                //fire
                if (_isFireUp)
                {
                    GameObject fireball = Instantiate(GameController.Skills[0], anchor.transform.position, quaternion.identity);
                    fireball.transform.forward = transform.forward;
                    fireball.GetComponent<KnockBack>().Init(gameObject, 500, 5, true);
                    ActivateCastAnimation("Cast");
                    _isFireUp = false;
                }
                break;
            case "E":
                //earth
                if (_isEarthUp)
                {
                    GameObject earth = Instantiate(GameController.Skills[2], transform.position, quaternion.identity);
                    earth.transform.forward = transform.forward;
                    earth.GetComponent<Earth>().Init(gameObject);
                    ActivateCastAnimation("CastOnGround");
                    _isEarthUp = false;
                }
                break;
            case "W":
                //water
                if (_isWaterUp)
                {
                    GameObject wave = Instantiate(GameController.Skills[1], anchorGround.transform.position + transform.forward * 2, quaternion.identity);
                    wave.transform.forward = transform.forward;
                    wave.GetComponent<KnockBack>().Init(gameObject, 500, 1, false);
                    ActivateCastAnimation("CastOnGround");
                    _isWaterUp = false;   
                }
                break;
            case "I":
                //wind
                if (_isWindUp)
                {
                    GameObject wind = Instantiate(GameController.Skills[3], anchor.transform.position + transform.forward * 2, quaternion.identity);
                    wind.transform.forward = transform.forward;
                    ActivateCastAnimation("Cast");
                    _isWindUp = false;
                }
                break;
            case "FE" : case "EF":
                //fire earth
                if (_isFireUp && _isEarthUp)
                {
                    GameObject meteor = Instantiate(GameController.Skills[5], transform.position + Vector3.up * 7, quaternion.identity);
                    meteor.transform.forward = transform.forward;
                    meteor.transform.Rotate(45,0,0);
                    ActivateCastAnimation("Cast");
                    _isFireUp = false;
                    _isEarthUp = false;
                }
                break;
            case "FW": case "WF":
                //fire water
                if (_isFireUp && _isWaterUp)
                {
                    GameObject heal = Instantiate(GameController.Skills[4], transform.position, quaternion.identity);
                    Destroy(heal.gameObject,4);
                    transform.GetComponent<HealthBar>().Heal(15);
                    heal.transform.SetParent(transform);
                    ActivateCastAnimation("CastOnGround");
                    _isFireUp = false;
                    _isWaterUp = false;
                }
                break;
            case "FI": case "IF":
                //fire wind
                if (_isFireUp && _isWindUp)
                {
                    GameObject tornado = Instantiate(GameController.Skills[6], anchor.transform.position, quaternion.identity);
                    tornado.transform.forward = transform.forward;
                    ActivateCastAnimation("Cast");
                    _isFireUp = false;
                    _isWindUp = false;
                }
                break;
            case "EW": case "WE":
                //earth water
                if (_isEarthUp && _isWaterUp)
                {
                    GameObject armor = Instantiate(GameController.Skills[7], transform.position + transform.up, quaternion.identity);
                    Destroy(armor.gameObject,5);
                    StartCoroutine(armorlifetime());
                    armor.transform.SetParent(transform);
                    ActivateCastAnimation("Cast");
                    _isEarthUp = false;
                    _isWaterUp = false;
                }
                break;
            case "EI":  case "IE":
                //earth wind
                if (_isEarthUp && _isWindUp)
                {
                    GameObject reversePosition = Instantiate(GameController.Skills[9], anchor.transform.position, quaternion.identity);
                    reversePosition.transform.forward = transform.forward;
                    reversePosition.GetComponent<ReversePosition>().Init(gameObject);
                    ActivateCastAnimation("Cast");
                    _isWindUp = false;
                    _isEarthUp = false;
                }
                break;
            case "WI":  case "IW":
                //water wind
                if (_isWaterUp && _isWindUp)
                {
                    GameObject iceWall = Instantiate(GameController.Skills[8], anchorGround.transform.position, quaternion.identity);
                    iceWall.transform.forward = transform.forward;
                    ActivateCastAnimation("CastOnGround");
                    _isWaterUp = false;
                    _isWindUp = false;
                }
                break;
            default:
                Debug.LogError("ton switch deconne");
                break;
        }
        _elementInMemory = "";
    }

    private void ActivateCastAnimation(string str)
    {
        _animator.Play(str);
    }

    public void PlayAgain()
    {
        visualGameObject.SetActive(true);
        isDead = false;
        speedModifier = 1;
        transform.GetComponent<HealthBar>().Heal(100);
        for (int i = 0; i < playerList.Count; i++) 
        {
            if (gameObject == playerList[i])
            {
                transform.position = GameController.spawnPoints[i].transform.position;
            }
        }
    }
    
    private void PlayerDead()
    {
        _trigger = true;
        if (!_dontCallPlayerOut)
        {
            GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().OnPlayerDeath(playerIndex);
        }
        ActivateCastAnimation("isDead");
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Water"))
        {
            isDead = true;
            if (GameController.alivePlayers.Count == 2)
            {
                _dontCallPlayerOut = true;
            }
            transform.GetComponent<HealthBar>().GetHit(100);
        }
    }

    IEnumerator armorlifetime()
    {
        speedModifier = .5f;
        RepulseForceModifier = .3f;
        yield return new WaitForSeconds(5);
        speedModifier = 1f;
        RepulseForceModifier = 1f;
    }
}