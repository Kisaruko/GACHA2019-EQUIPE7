using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveControler : MonoBehaviour
{
    private Material _mat;

    [SerializeField, Range(0, 1)] private float _dissolveAmount;


    public float DissolveAmount
    {
        get { return _dissolveAmount; }
        set
        {
            _dissolveAmount = value;
            if (_mat != null)
            {
                _mat.SetFloat("_DissolveAmount", _dissolveAmount);
            }
        }
    }


    private void Awake()
    {
        _mat = GetComponent<Renderer>().material;
        if (_mat != null) _mat.SetFloat("_DissolveAmount", _dissolveAmount);
    }



}
