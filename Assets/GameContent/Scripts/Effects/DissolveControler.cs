using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveControler : MonoBehaviour
{
    private Material _mat;

    [SerializeField, Range(0, 1)] private float _dissolveAmount;


    public void SetDissolveAmount(float amount)
    {
        _dissolveAmount = amount;
        if (_mat != null)
        {
            _mat.SetFloat("_DissolveAmount", _dissolveAmount);
        }
    }


    private void Awake()
    {
        _mat = GetComponent<Renderer>().material;
        if (_mat != null) _mat.SetFloat("_DissolveAmount", _dissolveAmount);
    }



}
