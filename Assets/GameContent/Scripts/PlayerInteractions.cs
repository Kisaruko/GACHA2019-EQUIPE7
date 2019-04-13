using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{

    void Start()
    {
        
    }


    void Update()
    {

        RaycastHit hit;
        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, LayerMask.GetMask("Interactive")))
        {
            Debug.Log(hit);
        }

    }
} //Fin du script --> Pierrick + Kérian
