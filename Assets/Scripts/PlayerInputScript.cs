using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputScript : MonoBehaviour
{
    public static List<GameObject> playerList = new List<GameObject>();

    public float lerpSpeed = 7f;
    public float speed = 1f;
    public GameObject anchorGround;
    public GameObject anchor;
    public GameObject visualGameObject;

    [HideInInspector]
    public float RepulseForceModifier = 1;


   
    
    private Rigidbody _rigidbody;
    private Vector2 _inputVector;
    private string _elementInMemory = "";
    private float speedModifier;
    private bool _isFireUp, _isEarthUp, _isWaterUp, _isWindUp;
    private float 
        _fireCooldown,
        _earthCooldown,
        _waterCooldown,
        _windCooldown,
        _fireTimer,
        _earthTimer,
        _waterTimer,
        _windTimer;

    void Start()
    {
        playerList.Add(gameObject);
        for (int i = 0; i < playerList.Count; i++) 
        {
            if (gameObject == playerList[i])
            {
                transform.position = GameController.spawnPoints[i].transform.position;
                transform.name = "Joueur " + i + 1;
            }
        }
        _rigidbody = GetComponent<Rigidbody>();
        speedModifier = 1;
    }

    void Update()
    {
        if (GameController.inRound)
        {
            MovePlayer();
            if (!_isFireUp) _isFireUp = DecrementCooldown(_fireCooldown, _fireTimer);
            if (!_isEarthUp) _isEarthUp = DecrementCooldown(_earthCooldown, _earthTimer);
            if (!_isWaterUp) _isWaterUp = DecrementCooldown(_waterCooldown, _waterTimer);
            if (!_isWindUp) _isWindUp = DecrementCooldown(_windCooldown, _windTimer);
        }
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
            transform.forward = new Vector3(transform.forward.x + tmpVec.x, transform.forward.y ,transform.forward.z + tmpVec.z);
        else
            _rigidbody.angularVelocity = Vector3.zero;
    }
    
    private bool DecrementCooldown(float Cooldown, float timer)
    {
        if (timer < Cooldown)
        {
            timer += Time.deltaTime;
            return false;
        }
        else
        {
            timer = 0f;
            return true;
        }
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
                GameObject fireball = Instantiate(GameController.Skills[0], anchor.transform.position, quaternion.identity);
                fireball.transform.forward = transform.forward;
                fireball.GetComponent<KnockBack>().Init(gameObject, 500, 5, true);
                break;
            case "E":
                //earth
                
                break;
            case "W":
                //water
                GameObject wave = Instantiate(GameController.Skills[1], anchorGround.transform.position + transform.forward * 2, quaternion.identity);
                wave.transform.forward = transform.forward;
                wave.GetComponent<KnockBack>().Init(gameObject, 500, 1, false);
                break;
            case "I":
                //wind
                GameObject wind = Instantiate(GameController.Skills[3], anchor.transform.position + transform.forward * 2, quaternion.identity);
                wind.transform.forward = transform.forward;
                break;
            case "FE" : case "EF":
                //fire earth
                GameObject meteor = Instantiate(GameController.Skills[5], transform.position + Vector3.up * 7, quaternion.identity);
                meteor.transform.forward = transform.forward;
                
                break;
            case "FW": case "WF":
                //fire water

                break;
            case "FI": case "IF":
                //fire wind

                break;
            case "EW": case "WE":
                //earth water
                GameObject armor = Instantiate(GameController.Skills[7], transform.position + transform.up, quaternion.identity);
                Destroy(armor.gameObject,10);
                StartCoroutine(armorlifetime());
                armor.transform.SetParent(transform);
                break;
            case "EI":  case "IE":
                //earth wind
                GameObject reversePosition = Instantiate(GameController.Skills[9], anchor.transform.position, quaternion.identity);
                reversePosition.transform.forward = transform.forward;
                reversePosition.GetComponent<ReversePosition>().Init(gameObject);
                break;
            case "WI":  case "IW":
                //water wind
                GameObject iceWall = Instantiate(GameController.Skills[8], anchorGround.transform.position, quaternion.identity);
                iceWall.transform.forward = transform.forward;
                break;
            default:
                Debug.LogError("ton switch deconne");
                break;
        }
        _elementInMemory = "";
    }

    IEnumerator armorlifetime()
    {
        speedModifier = .5f;
        RepulseForceModifier = .3f;
        yield return new WaitForSeconds(10);
        speedModifier = 1f;
        RepulseForceModifier = 1f;
    }
}