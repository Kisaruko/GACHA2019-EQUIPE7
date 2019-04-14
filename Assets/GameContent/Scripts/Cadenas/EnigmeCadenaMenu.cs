using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
            
        }
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, LayerMask.GetMask("Interact"))&&Input.GetMouseButtonDown(0))
        {
            
            objSelected = hit.transform.gameObject;
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
