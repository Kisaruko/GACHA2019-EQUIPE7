using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    #region Fields / Properties
    public bool doDesactiveAfterEnable = true;
    public string interactID = string.Empty;
    public UnityEvent interactEvent = new UnityEvent();
    public GameObject prefab = null;
    public Transform prefabPosition = null;
    public string prefabText = string.Empty;
    public bool doMoveObjectOnTrigger = false;
    public Transform toMoveObjectTransform = null;
    #endregion

    #region Methods

    #region Original Methods
    // Activate an interactable
    public void ActiveInteract()
    {
        if (prefab && prefabPosition)
        {
            GameObject _prefab = Instantiate(prefab, prefabPosition.position, prefab.transform.rotation);
            TextMesh _text = _prefab.GetComponent<TextMesh>();
            if (_text) _text.text = prefabText;

            Destroy(_prefab, (prefabText.Length / 2));
        }
        interactEvent.Invoke();

        if (doDesactiveAfterEnable) Destroy(this);
    }
    #endregion

    #region Unity Methods
    private void OnTriggerEnter(Collider other)
    {
        Interact _interact = other.GetComponent<Interact>();
        if (_interact && (interactID == _interact.interactID))
        {
            // Move object if needed
            if (doMoveObjectOnTrigger && toMoveObjectTransform)
            {
                other.transform.SetParent(null);
                other.transform.position = toMoveObjectTransform.position;
                other.transform.rotation = toMoveObjectTransform.rotation;
            }

            ActiveInteract();
        }
    }

    /*private void Start()
    {
        Collider _collider = GetComponent<Collider>();
        if (!_collider.isTrigger) gameObject.tag = "Interact";
    }*/
    #endregion

    #endregion
}
