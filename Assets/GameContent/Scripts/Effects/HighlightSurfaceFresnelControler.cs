using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HighlightSurfaceFresnelControler : MonoBehaviour
{
    private Material _material;
    [SerializeField, Range(0, 20)] private float _flashSpeed = 2;
    public bool _flashing;


    private void Awake()
    {
        _material = GetComponent<Renderer>().material;
    }


    private void FixedUpdate()
    {
        if(_flashing)_material.SetFloat("_RimAmount", Mathf.PingPong(Time.time * _flashSpeed, 1));
    } 

    public void SetHighlight(bool state)
    {
        _flashing = state;
        _material.SetFloat("_RimAmount", 0);
    }
}
