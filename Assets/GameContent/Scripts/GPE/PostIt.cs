using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostIt : MonoBehaviour
{
    [SerializeField, TextArea] private string _note = "";
    [SerializeField] private TextMesh _text = null;
    [SerializeField] private float _fadeDuration = 0.5f;
    private Color _currentColor;
    [SerializeField] private float _alpha = 0;

    private void Start()
    {
        _text.text = _note;
        _currentColor = new Color(_currentColor.r, _currentColor.g, _currentColor.b, _alpha);
    }


    private void FixedUpdate()
    {
        _alpha = Mathf.Lerp(_text.color.a, 0, Time.fixedDeltaTime / Mathf.Abs(_fadeDuration));

        if(_alpha < 0.1f)
        {
            _alpha = 0;
        }

        _text.color = new Color(_currentColor.r, _currentColor.g, _currentColor.b, _alpha);
    }


    public void ShowNote()
    {
        _alpha = 1;
        _text.color = new Color(_currentColor.r, _currentColor.g, _currentColor.b, _alpha);
    }
}
