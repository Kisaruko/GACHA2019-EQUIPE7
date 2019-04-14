using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    private GameObject lastObjectSee;
    public float timerHighlight = 0;
    public float maxtimer = 0.75f;
    public bool canRotateObject;
    public bool CanRotateObject
    {
        get { return canRotateObject; }
        set
        {
            canRotateObject = value;
            GetComponent<PlayerController_LG>().canLook = !value;
            GetComponent<PlayerController_LG>().canMove = !value;

            Cursor.lockState = (value == true) ? CursorLockMode.Confined : CursorLockMode.Locked;
        }
    }
    public float speedRotateObj = 0.1f;
    public float distMaxDrop = 3;
    Vector3 initialPosObj;

    //True if there is a player interactionand timerHighlight is zero
    private bool _canDisplayInteraction;
    public bool CanDisplayInteraction { get { return _canDisplayInteraction; } } //Readonly _canDisplayInteraction

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, distMaxDrop, LayerMask.GetMask("Interactive")))
        {
            if (lastObjectSee != hit.transform.gameObject)
            {
                lastObjectSee = hit.transform.gameObject;
                timerHighlight = 0;

                _canDisplayInteraction = false; //Resets can display to false
            }
            else
            {
                if (timerHighlight < maxtimer)
                {
                    timerHighlight += Time.deltaTime;
                }
                else
                {
                    _canDisplayInteraction = true; //Can display reticle

                }
            }
            if (Input.GetMouseButtonDown(0) && hit.transform.gameObject.CompareTag("Drop"))
            {
                hit.transform.SetParent(Camera.main.transform);
                hit.rigidbody.useGravity = false;
                //hit.transform.gameObject.GetComponent<Collider>().enabled = false;
            }
            /*else if (hit.transform.gameObject.CompareTag("Interact") && Input.GetMouseButtonDown(0))
            {
                try
                {
                    //hit.transform.gameObject.GetComponent<Interact>().StartEvent();
                }
                catch (System.Exception)
                {

                    throw;
                }
                
            }*/

        }
        else
        {
            _canDisplayInteraction = false; //Resets can display to false
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (Camera.main.transform.childCount > 0)
            {
                Camera.main.transform.GetChild(0).gameObject.GetComponent<Rigidbody>().useGravity = true;
                //Camera.main.transform.GetChild(0).gameObject.GetComponent<Collider>().enabled = true;
                Camera.main.transform.GetChild(0).transform.SetParent(null);

            }
            CanRotateObject = false;
        }
        if (Input.GetMouseButton(0))
        {
            if (Input.GetMouseButtonDown(1))
            {
                CanRotateObject = !canRotateObject;
                initialPosObj = Vector3.zero;

            }
            if (canRotateObject)
            {
                /* Vector3 _pos = Input.mousePosition - initialPosObj;
                 if (hand.transform.childCount > 0)
                 {
                     hand.transform.GetChild(0).transform.Rotate(speedRotateObj * _pos);
                 }*/

                Camera.main.transform.GetChild(0).transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * speedRotateObj);
                return;
                /*
                Vector3 posCamera = Camera.main.transform.position;
                if (Input.mousePosition.y >= Screen.height * (0.5) && Input.mousePosition.y < Screen.height)
                {
                    Debug.Log((180 * (Input.mousePosition.y - Screen.height / 2)) / Screen.height);
                    Camera.main.transform.GetChild(0).transform.eulerAngles = new Vector3(
                        Camera.main.transform.GetChild(0).transform.eulerAngles.x,
                        Camera.main.transform.GetChild(0).transform.eulerAngles.y,
                        (360 * (Input.mousePosition.y - Screen.height / 2)) / Screen.height);
                }
                else if (Input.mousePosition.y <= Screen.height * (0.5) && Input.mousePosition.y > 0)
                {
                    Debug.Log((180 * Input.mousePosition.y) / Screen.height);
                    Camera.main.transform.GetChild(0).transform.eulerAngles = new Vector3(
                        Camera.main.transform.GetChild(0).transform.eulerAngles.x,
                        Camera.main.transform.GetChild(0).transform.eulerAngles.y,
                        (360 * (Input.mousePosition.y)) / Screen.height);
                }

                if (Input.mousePosition.x >= Screen.width * (0.5) && Input.mousePosition.x < Screen.width)
                {
                    Debug.Log((180 * (Input.mousePosition.x - Screen.width / 2)) / Screen.width);
                    Camera.main.transform.GetChild(0).transform.eulerAngles = new Vector3(
                        Camera.main.transform.GetChild(0).transform.eulerAngles.x,
                        (360 * (Input.mousePosition.x)) / Screen.width,
                        Camera.main.transform.GetChild(0).transform.eulerAngles.z);
                }
                else if (Input.mousePosition.x <= Screen.width * (0.5) && Input.mousePosition.x > 0)
                {
                    Debug.Log((180 * Input.mousePosition.x) / Screen.width);
                    Camera.main.transform.GetChild(0).transform.eulerAngles = new Vector3(
                        Camera.main.transform.GetChild(0).transform.eulerAngles.x,
                        (360 * (Input.mousePosition.x)) / Screen.width,
                        Camera.main.transform.GetChild(0).transform.eulerAngles.z);
                }*/
            }

        }

    }
} //Fin du script --> Pierrick + Kérian
