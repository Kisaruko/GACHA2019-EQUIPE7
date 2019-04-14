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
    public Camera currentCamera;
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
        if (Physics.Raycast(currentCamera.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, LayerMask.GetMask("Interactive"))&&Input.GetMouseButtonDown(0))
        {
            HighlightSurfaceFresnelControler fresnelControler;
            if (objSelected)
            {

                fresnelControler = objSelected.GetComponent<HighlightSurfaceFresnelControler>();
                if (fresnelControler)
                {
                    objSelected.transform.localScale = Vector3.one;
                    fresnelControler.SetHighlight(false);
                }

            }
            if(hit.transform.gameObject.tag == "Cadenas")
            {
                objSelected = hit.transform.gameObject;
                fresnelControler = objSelected.GetComponent<HighlightSurfaceFresnelControler>();

                if (fresnelControler)
                {
                    objSelected.transform.localScale = Vector3.one * 1.1f;
                    objSelected.GetComponent<HighlightSurfaceFresnelControler>().SetHighlight(true);
                }
            }
        
            


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
            if (objSelected) { objSelected.transform.Rotate(Vector3.right * +36); }
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
    private void Start()
    {
        currentCamera = FindObjectOfType<Camera>();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
