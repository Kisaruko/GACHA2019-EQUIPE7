using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePR : MonoBehaviour
{
    public GameObject[] matriochka = new GameObject[] { };

    private IEnumerator Activate()
    {
        if (!AllManager.Instance) yield break;

        yield return new WaitForSeconds(7);

        for (int _i = 0; _i < AllManager.Instance.Matriochka.Length; _i++)
        {
            if (_i < matriochka.Length) matriochka[_i].SetActive(AllManager.Instance.Matriochka[_i]);
            yield return new WaitForSeconds(.25f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        StartCoroutine(Activate());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
