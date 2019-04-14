using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeLidsOpen : MonoBehaviour
{
    private Animation _anim;

    private void Awake()
    {
        _anim = GetComponentInChildren<Animation>();   
    }


    private void Update()
    {
        if(_anim.isPlaying == false)
        {
            Destroy(gameObject);
        }
    }
}
