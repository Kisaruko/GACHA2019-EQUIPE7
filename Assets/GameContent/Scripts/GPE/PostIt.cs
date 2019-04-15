using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostIt : MonoBehaviour
{
    [SerializeField, TextArea] private string _note = "";
    [SerializeField] private TextMesh _text = null;
    [SerializeField] private float _fadeDuration;
    private Color _currentColor;
    private float _alpha;

    private void Start()
    {
        _text.text = _note;
        _currentColor = _text.color;
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
