using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    public PlayerController_LG controller;
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
            controller.canLook = !value;
            controller.canMove = !value;

            Cursor.lockState = (value == true) ? CursorLockMode.Confined : CursorLockMode.Locked;
        }
    }
    public float speedRotateObj = 0.1f;
    public float distMaxDrop = 3;
    Vector3 initialPosObj;

    //True if there is a player interactionand timerHighlight is zero
    private bool _canDisplayInteraction;
    public bool CanDisplayInteraction { get { return _canDisplayInteraction; } } //Readonly _canDisplayInteraction

    public void Desactive()
    {
        GetComponent<Animator>().enabled = false;
        controller.enabled = false;
        this.enabled = false;
    }

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
                Camera.main.transform.GetChild(0).gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            }
            if (Input.GetMouseButtonDown(0) && hit.transform.gameObject.CompareTag("Interact"))
            {
                hit.transform.GetComponent<Trigger>().ActiveInteract();
            }
        }
        else
        {
            _canDisplayInteraction = false; //Resets can display to false
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (Camera.main.transform.childCount > 0)
            {
                Rigidbody _rigidbody = Camera.main.transform.GetChild(0).gameObject.GetComponent<Rigidbody>();
                if (_rigidbody)
                {
                    _rigidbody.useGravity = true;
                    _rigidbody.constraints = RigidbodyConstraints.None;
                }
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
    private void Awake()
    {
        controller  = GetComponent<PlayerController_LG>();
    }
} //Fin du script --> Pierrick + Kérian
