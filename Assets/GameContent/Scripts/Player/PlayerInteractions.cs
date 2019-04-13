using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    public GameObject hand;
    private GameObject lastObjectSee;
    public float timerHighlight = 0;
    public float maxtimer = 0.75f;
    public bool canRotateObject;
    public float speedRotateObj = 0.1f;
    public float distMaxDrop = 3;
    Vector3 initialPosObj;
    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;

    public float minimumX = -360F;
    public float maximumX = 360F;

    public float minimumY = -60F;
    public float maximumY = 60F;

    float rotationY = 0F;


    private void FixedUpdate()
    {
        if (!canRotateObject)
        {
            if (axes == RotationAxes.MouseXAndY)
            {
                float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

                rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

                transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
            }
            else if (axes == RotationAxes.MouseX)
            {
                transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
            }
            else
            {
                rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

                transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
            }
        }
    }

    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, distMaxDrop, LayerMask.GetMask("Interactive")) )
        {
            if (lastObjectSee != hit.transform.gameObject)
            {
                lastObjectSee = hit.transform.gameObject;
                timerHighlight = 0;
            }
            else
            {
                if (timerHighlight < maxtimer)
                {
                    timerHighlight += Time.deltaTime;
                }
                else
                {
                    // reticule a mettre
                }
            }
            if (hand && Input.GetMouseButtonDown(0) && hit.transform.gameObject.CompareTag("Drop"))
            {
                hit.transform.SetParent(hand.transform);
                hit.transform.position = hand.transform.position;
                hit.rigidbody.isKinematic = true;                
            }
            else if (hit.transform.gameObject.CompareTag("Interact"))
            {
                Debug.Log("Interact");

            }

        }
        if(Input.GetMouseButtonUp(0))
        {
            if(hand.transform.childCount > 0)
            {
                hand.transform.GetChild(0).gameObject.GetComponent<Rigidbody>().isKinematic = false;
                hand.transform.GetChild(0).transform.SetParent(null);
               
            }
            canRotateObject = false;
        }
        if (Input.GetMouseButton(0))
        {
            if (Input.GetMouseButtonDown(1))
            {
                canRotateObject = !canRotateObject;
                initialPosObj = Vector3.zero;

            }
            if (canRotateObject)
            {
                /* Vector3 _pos = Input.mousePosition - initialPosObj;
                 if (hand.transform.childCount > 0)
                 {
                     hand.transform.GetChild(0).transform.Rotate(speedRotateObj * _pos);
                 }*/
                Vector3 posCamera = Camera.main.transform.position;
                if(Input.mousePosition.y >= Screen.height * (0.5)&&Input.mousePosition.y < Screen.height)
                {
                    Debug.Log((180 * (Input.mousePosition.y - Screen.height / 2)) / Screen.height);
                    hand.transform.GetChild(0).transform.eulerAngles = new Vector3(
                        hand.transform.GetChild(0).transform.eulerAngles.x,
                        hand.transform.GetChild(0).transform.eulerAngles.y,
                        (360 * (Input.mousePosition.y - Screen.height / 2)) / Screen.height);
                }
                else if (Input.mousePosition.y <= Screen.height * (0.5) && Input.mousePosition.y > 0)
                    {
                        Debug.Log((180 * Input.mousePosition.y) / Screen.height);
                        hand.transform.GetChild(0).transform.eulerAngles = new Vector3(
                            hand.transform.GetChild(0).transform.eulerAngles.x,
                            hand.transform.GetChild(0).transform.eulerAngles.y,
                            (360 * (Input.mousePosition.y)) / Screen.height);
                    }

                if (Input.mousePosition.x >= Screen.width * (0.5) && Input.mousePosition.x < Screen.width)
                {
                    Debug.Log((180 * (Input.mousePosition.x - Screen.width / 2)) / Screen.width);
                    hand.transform.GetChild(0).transform.eulerAngles = new Vector3(
                        hand.transform.GetChild(0).transform.eulerAngles.x,
                        (360 * (Input.mousePosition.x)) / Screen.width,
                        hand.transform.GetChild(0).transform.eulerAngles.z);
                }
                else if (Input.mousePosition.x <= Screen.width * (0.5) && Input.mousePosition.x > 0)
                {
                    Debug.Log((180 * Input.mousePosition.x) / Screen.width);
                    hand.transform.GetChild(0).transform.eulerAngles = new Vector3(
                        hand.transform.GetChild(0).transform.eulerAngles.x,
                        (360 * (Input.mousePosition.x)) / Screen.width,
                        hand.transform.GetChild(0).transform.eulerAngles.z);
                }




            }

        }

    }
} //Fin du script --> Pierrick + Kérian
