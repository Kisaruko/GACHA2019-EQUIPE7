using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    public ObjectType interactType = ObjectType.Player;
    public UnityEvent interactEvent = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Interact _interact = other.GetComponent<Interact>();
        if (_interact)
        {
            interactEvent.Invoke();
        }
    }
}

public enum ObjectType
{
    Key,
    Card,
    Player
}
