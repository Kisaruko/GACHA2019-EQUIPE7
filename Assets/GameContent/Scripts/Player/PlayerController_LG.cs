using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_LG : MonoBehaviour
{
    #region Fields / Properties
    /// <summary>
    /// Player animator
    /// </summary>
    public Animator animator = null;

    /// <summary>
    /// Camera of the player
    /// </summary>
    public new Camera           camera                          = null;

    /// <summary>
    /// Rigidbody of the player
    /// </summary>
    public new Rigidbody        rigidbody                       = null;

    /// <summary>
    /// Change this to set if the player can move or not.
    /// </summary>
    public bool                 canMove                         = true;

    /// <summary>Backing field for <see cref="IsMoving"/>.</summary>
    public bool                 isMoving                        = false;

    /// <summary>
    /// Indicates if the player is currently moving
    /// </summary>
    public bool                 IsMoving
    {
        get { return isMoving; }
        set
        {
            isMoving = value;
            animator.SetBool("isMoving", value);
        }
    }

    /// <summary>Backing field for <see cref="IsCrouch"/>.</summary>
    public bool                 isCrouch                        = false;

    /// <summary>
    /// Indicates if the player is crouch
    /// </summary>
    public bool                 IsCrouch
    {
        get { return isCrouch; }
        set
        {
            isCrouch = value;
            canMove = !value;
            if (isMoving) IsMoving = false;
        }
    }

    /// <summary>
    /// If false, the player cannot rotate the camera
    /// </summary>
    public bool                 canLook                         = true;


    /// <summary>
    /// Speed of the player movements
    /// </summary>
    public float                speedMove                       = 10;

    /// <summary>
    /// Speed of the player look
    /// </summary>
    public float                speedLook                       = 10;


    /// <summary>
    /// Minimum angle value for the view rotation
    /// </summary>
    public float                lookYMaxAngle                   = 60;

    /// <summary>
    /// Maximum angle value for the view rotation
    /// </summary>
    public float                lookYMinAngle                   = -60;


    /// <summary>
    /// Name of the axis used to move in the horizontal axis.
    /// </summary>
    public string               moveHorizontalAxis              = "Horizontal";

    /// <summary>
    /// Name of the axis used to move in the vertical axis.
    /// </summary>
    public string               moveVerticalAxis                = "Vertical";

    /// <summary>
    /// Name of the axis used to look in the horizontal axis.
    /// </summary>
    public string               viewHorizontalAxis              = "Mouse X";

    /// <summary>
    /// Name of the axis used to look in the vertical axis.
    /// </summary>
    public string               viewVerticalAxis                = "Mouse Y";

    /// <summary>
    /// Name of the input to crouch
    /// </summary>
    public string               crouchInput                     = "Crouch";
    #endregion

    #region Methods

    #region Original Methods
    /// <summary>
    /// Manages the look movement
    /// </summary>
    private void Look()
    {
        if (!canLook) return;

        float _horizontalView = Input.GetAxis(viewHorizontalAxis);
        float _verticalView = Input.GetAxis(viewVerticalAxis);

        float _cameraRotation = camera.transform.localRotation.eulerAngles.x + (_verticalView * -speedLook);
        _cameraRotation = _cameraRotation > -lookYMinAngle ? (_cameraRotation > 360 - lookYMaxAngle ? _cameraRotation : _cameraRotation > 180 ? 360 - lookYMaxAngle : -lookYMinAngle) : _cameraRotation < -lookYMaxAngle ? -lookYMaxAngle : _cameraRotation;

        camera.transform.localRotation = Quaternion.Euler(_cameraRotation, 0, 0);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + (_horizontalView * speedLook), 0);
    }

    /// <summary>
    /// Moves the player
    /// </summary>
    private void Move()
    {
        if (Input.GetButton(crouchInput))
        {
            if (!isCrouch)
            {
                animator.SetBool("isCrouch", true);
                IsCrouch = true;
            }
        }
        else if (isCrouch)
        {
            animator.SetBool("isCrouch", false);
            IsCrouch = false;
        }

        if (!canMove) return;

        float _horizontalMove = Input.GetAxis(moveHorizontalAxis);
        float _verticalMove = Input.GetAxis(moveVerticalAxis);

        if ((_horizontalMove == 0) && (_verticalMove == 0))
        {
            if (isMoving)
            {
                IsMoving = false;
            }
            return;
        }

        Vector3 _direction = transform.forward * _verticalMove;
        _direction.y = 0;
        rigidbody.velocity = (_direction + (transform.right * _horizontalMove)) * speedMove;

        if (!isMoving)
        {
            IsMoving = true;
        }
    }
    #endregion

    #region Unity Methods
    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Get missing component references
        if (!animator)
        {
            animator = GetComponent<Animator>();
            if (!animator) Debug.LogError($"Missing Animator on Player \"{name}\"");
        }
        if (!camera)
        {
            camera = GetComponentInChildren<Camera>();
            if (!camera) Debug.LogError($"Missing Camera on Player \"{name}\"");
        }
        camera.transform.LookAt(transform.forward);
        if (!rigidbody)
        {
            rigidbody = GetComponent<Rigidbody>();
            if (!rigidbody) Debug.LogError($"Missing Rigidbody on Player \"{name}\"");
        }
    }

    // Frame-rate independent MonoBehaviour.FixedUpdate message for physics calculations
    private void FixedUpdate()
    {
        Move();
    }

    // Start is called before the first frame update
    private void Start()
    {
        // Disable cursor and lock it on screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (UIManager.Instance) UIManager.Instance.OnPause += (bool _doPause) => canLook = !_doPause;
    }

    // Update is called once per frame
    private void Update()
    {
        Look();
    }
    #endregion

    #endregion
}
