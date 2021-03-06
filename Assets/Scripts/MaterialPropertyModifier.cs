using System.Collections.Generic;
using UnityEngine;

public class MaterialPropertyModifier : MonoBehaviour
{
    [Range(0, 1)]
    public float dissolveValue = 0f;
    public GameObject arena;
    public float delayGroundFall;
    [Space]
    public List<Renderer> tileRenderers = new List<Renderer>();


    [HideInInspector]
    public float _groundtimer;
    private bool _dissolveOneLayer;
    [HideInInspector]public int _actualLayer = 3;
    
    private static readonly int DissolveValue = Shader.PropertyToID("_DissolveValue");

    private void Start()
    {
        for (int i = 0; i < arena.transform.childCount; i++)
        {
            if (!arena.transform.GetChild(i).CompareTag("GroundInamovible"))
            {
                tileRenderers.Add(arena.transform.GetChild(i).GetChild(0).GetComponent<Renderer>());
            }
        }
    }

    void Update()
    {
        if (GameController.inRound && !_dissolveOneLayer)
        {
            _groundtimer += Time.deltaTime;
        }
        else
        {
            _groundtimer = 0f;
        }

        if (_groundtimer >= delayGroundFall)
        {
            _groundtimer -= delayGroundFall;
            _dissolveOneLayer = true;
        }

        if (_dissolveOneLayer)
        {
            UpdateTileList();
        }
    }

    [ContextMenu("Update All Materials In List")]
    void UpdateTileList()
    {
        dissolveValue += Time.deltaTime / 2f;
        if (dissolveValue <= 1)
        {
            foreach (Renderer rndr in tileRenderers)
            { 
                if (rndr.transform.parent.tag.Contains(_actualLayer.ToString()))
                { 
                    rndr.material.SetFloat(DissolveValue, dissolveValue); // For Single Material instances
                }
            }
        }
        else
        {
            List<Renderer> rndrList = new List<Renderer>();
            foreach (Renderer rndr in tileRenderers)
            {
                if (rndr.transform.parent.tag.Contains(_actualLayer.ToString()))
                {
                    rndrList.Add(rndr);
                    rndr.transform.parent.GetChild(1).GetComponent<Rigidbody>().useGravity = true;
                    rndr.transform.parent.GetChild(1).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    Destroy(rndr.transform.parent.gameObject, 1.5f); 
                }
            }

            foreach (Renderer renderer in rndrList)
            {
                tileRenderers.Remove(renderer); 
                Destroy(renderer.gameObject);
            }
            rndrList.Clear();
            _actualLayer--; 
            _dissolveOneLayer = false;
            dissolveValue = 0f; 
        }
        // rndr.sharedMaterial.SetFloat(DissolveValue, DisolveValue); // For The Shared Material
    }
    /*[ContextMenu("Update Material Property")]
    public void UpdateMaterialProperty()
    {
        Shader.SetGlobalFloat(DissolveValue, DisolveValue); // If Property is not exposed in shader
    }*/
    public void Init(GameObject go)
    {
        _actualLayer = 3;
        _groundtimer = 0;
        tileRenderers.Clear();
        arena = go;
        for (int i = 0; i < arena.transform.childCount; i++)
        {
            if (!arena.transform.GetChild(i).CompareTag("GroundInamovible"))
            {
                tileRenderers.Add(arena.transform.GetChild(i).GetChild(0).GetComponent<Renderer>());
            }
        }
        
    }
}
