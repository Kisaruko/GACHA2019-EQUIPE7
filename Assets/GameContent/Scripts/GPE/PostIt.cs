using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostIt : MonoBehaviour
{
    [SerializeField] private TextMesh _text = null;
    [SerializeField] private float _fadeInDuration = 0.2f;
    [SerializeField] private float _fadeOutDuration = 0.5f;
    [SerializeField] private float _displayAmount = 0;

    private Material _mat;
    private float _targetDisplayAmount;
    private bool _displaying;


    private void Awake()
    {
        _mat = _text.gameObject.GetComponent<Renderer>().material;
    }


    private void Start()
    {
        _mat.SetFloat("_DissolveAmount",1 - _displayAmount);
    }


    private void FixedUpdate()
    {
        float t = Time.fixedDeltaTime;

        if (_displayAmount < _targetDisplayAmount)
        {
            t /= Mathf.Abs(_fadeInDuration);
        }
        else
        {
            t /= Mathf.Abs(_fadeOutDuration);
        }

        _displayAmount = Mathf.Lerp(1 - _mat.GetFloat("_DissolveAmount"), _targetDisplayAmount, t);

        if (_displayAmount < 0.1f && !_displaying)
        {
            _displayAmount = 0;
        }

        if (_displaying)
        {
            _displaying = false;
        }


        _mat.SetFloat("_DissolveAmount",1 - _displayAmount);

        if(_targetDisplayAmount != 0)
        {
            _targetDisplayAmount = 0;
        }
    }


    public void ShowNote()
    {
        _targetDisplayAmount = 1;
        _displaying = true;
    }
}
