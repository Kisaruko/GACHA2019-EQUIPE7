using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class EnigmeCadenaMenu : MonoBehaviour
{
    [NonSerialized]
    public GameObject objSelected;
    public string codeCadena = "012345";
    public NumberCadena[] cylindre = new NumberCadena[6];
    bool canRotate = true;
    string _actualNum = "";
    bool isOpen = false;
    public UnityEvent onOpen = new UnityEvent();

    // Update is called once per frame
    void Update()
    {
        if (isOpen) { return; }
        _actualNum = "";
        foreach (NumberCadena num in cylindre)
        {
            _actualNum += num.number.ToString();
        }

        if(codeCadena == _actualNum)
        {
            Debug.Log("finish");
            isOpen = true;
            GetComponent<Animator>().enabled = true;
            onOpen.Invoke();
        }
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, LayerMask.GetMask("Interactive"))&&Input.GetMouseButtonDown(0))
        {
            if (objSelected)
            {
                objSelected.transform.localScale = Vector3.one;
                objSelected.GetComponent<HighlightSurfaceFresnelControler>().SetHighlight(false);
            }
            objSelected = hit.transform.gameObject;
            objSelected.transform.localScale = Vector3.one * 1.1f;
            objSelected.GetComponent<HighlightSurfaceFresnelControler>().SetHighlight(true);

            //Debug.Log(objSelected.name);

            //att faire une highLigh
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            
            objSelected?.transform.Rotate(Vector3.right * -36);
            try
            {
                if (objSelected.GetComponent<NumberCadena>().number < 9)
                {
                    objSelected.GetComponent<NumberCadena>().number += 1;
                }
                else { objSelected.GetComponent<NumberCadena>().number = 0; }
            }
            catch (Exception)
            {

               
            }
            
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            objSelected?.transform.Rotate(Vector3.right * +36);
            try
            {
                if (objSelected.GetComponent<NumberCadena>().number >0) {
                    objSelected.GetComponent<NumberCadena>().number -= 1;
                }
                else { objSelected.GetComponent<NumberCadena>().number = 9; }
                
            }
            catch (Exception)
            {

                
            }
        }

        

    }
    
}
