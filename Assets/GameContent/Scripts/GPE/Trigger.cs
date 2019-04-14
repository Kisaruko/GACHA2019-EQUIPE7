using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    public ObjectType interactType = ObjectType.Player;
    public UnityEvent interactEvent = new UnityEvent();
    public GameObject prefab = null;
    public Transform prefabPosition = null;
    public string prefabText = string.Empty;

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
            if (prefab && prefabPosition)
            {
                GameObject _prefab = Instantiate(prefab, prefabPosition.position, Quaternion.identity);
                TextMesh _text = _prefab.GetComponent<TextMesh>();
                if (_text) _text.text = prefabText;

                Destroy(_prefab, (prefabText.Length / 2));
            }
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

