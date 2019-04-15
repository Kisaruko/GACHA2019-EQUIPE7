using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveControler : MonoBehaviour
{
    private Material _mat;

    [SerializeField, Range(0, 1)] private float _dissolveAmount;
    public bool general;
    private float targetAmount;
    public float speed = 0.5f;


    public void SetDissolveAmount(float amount)
    {
        if (_mat == null) return;

        targetAmount = amount;
    }


    private void Update()
    {
        _dissolveAmount = Mathf.Lerp(_dissolveAmount, targetAmount, Time.fixedDeltaTime / speed);

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
