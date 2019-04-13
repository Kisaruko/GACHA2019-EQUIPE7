using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public bool open = false;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("key"))
        {
            open = true;
            Destroy(other.gameObject);
        }
    }
}
