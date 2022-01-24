using System.Collections.Generic;
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

    private Rigidbody _rigidbody;
    private Vector2 _inputVector; 
    private string _elementInMemory = "";

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
    }

    void Update()
    {
        if (GameController.inRound)
        {
            MovePlayer();
        }
    }

    private void MovePlayer()
    {
        Vector3 tmpVec = new Vector3(_inputVector.x, 0f, _inputVector.y);
        tmpVec = tmpVec.normalized * speed * Time.deltaTime;
        _rigidbody.MovePosition(transform.position + tmpVec);

        if (_rigidbody.velocity.magnitude > 5f)
        {
            _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, 5f);
        }

        if (_inputVector != Vector2.zero)
        {
            transform.forward = Vector3.Lerp(transform.forward, new Vector3(_inputVector.x, 0, _inputVector.y),
                Time.deltaTime * lerpSpeed);
        }
        else
        {
            _rigidbody.angularVelocity = Vector3.zero;
        }
    }

    private void OnMovement(InputValue value)
    {
         _inputVector = value.Get<Vector2>();
    }

    private void OnSelectElementTop()
    {
        //fire
        _elementInMemory += "F";
        if (_elementInMemory.Length > 2)
        {
            _elementInMemory = _elementInMemory.Substring(1);
        }
        if (_elementInMemory == "FF")
        {
            _elementInMemory = "F";
        }
    }
    private void OnSelectElementBottom()
    {
        //earth
        _elementInMemory += "E";
        if (_elementInMemory.Length > 2)
        {
            _elementInMemory = _elementInMemory.Substring(1);
        }
        if (_elementInMemory == "EE")
        {
            _elementInMemory = "E";
        }
    }
    private void OnSelectElementLeft()
    {
        //water
        _elementInMemory += "W";
        if (_elementInMemory.Length > 2)
        {
            _elementInMemory = _elementInMemory.Substring(1);
        }
        if (_elementInMemory == "WW")
        {
            _elementInMemory = "W";
        }
    }
    private void OnSelectElementRight()
    {
        //wind
        _elementInMemory += "I";
        if (_elementInMemory.Length > 2)
        {
            _elementInMemory = _elementInMemory.Substring(1);
        }
        if (_elementInMemory == "II")
        {
            _elementInMemory = "I";
        }
    }

    private void OnCastSpell()
    {
        switch (_elementInMemory)
        {
            case "":
                break;
            case "F":
                //fire
                GameObject go = Instantiate(GameController.Skills[0], anchor.transform.position, quaternion.identity);
                go.transform.forward = transform.forward;
                go.GetComponent<KnockBack>().Init(gameObject);
                break;
            case "E":
                //earth
                
                break;
            case "W":
                //water
                
                break;
            case "I":
                //wind
                
                break;
            case "FE":
                //fire earth
                
                break;
            case "EF":
                //fire earth

                break;
            case "FW":
                //fire water
                
                break;
            case "WF":
                //fire water

                break;
            case "FI":
                //fire wind

                break;
            case "IF":
                //fire wind

                break;
            case "EW":
                //earth water
                
                break;
            case "WE":
                //earth water

                break;
            case "EI":
                //earth wind
                
                break;
            case "IE":
                //earth wind

                break;
            case "WI":
                //water wind
                
                break;
            case "IW":
                //water wind

                break;
            default:
                Debug.LogError("ton switch deconne");
                break;
        }
        _elementInMemory = "";
    }
}