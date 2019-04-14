﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HighlightSurfaceFresnelControler : MonoBehaviour
{
    private Material _material;
    [SerializeField, Range(0, 20)] private float _flashSpeed = 2;


    private void Awake()
    {
        _material = GetComponent<Renderer>().material;
    }


    private void FixedUpdate()
    {
        _material.SetFloat("_RimAmount", Mathf.PingPong(Time.time * _flashSpeed, 1));
    }
}
