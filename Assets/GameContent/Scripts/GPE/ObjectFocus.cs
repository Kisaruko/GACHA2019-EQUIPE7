using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObjectFocus : MonoBehaviour
{
    private Camera _focusCamera;
    private Collider _interactionCollider;


    private void Awake()
    {
        _focusCamera = GetComponentInChildren<Camera>();
        _interactionCollider = GetComponent<Collider>();
    }


    /// <summary>
    /// Sets the players focus state on the object
    /// </summary>
    /// <param name="isFocus"></param>
    public void SetPlayerFocusState(bool isFocus)
    {
        PlayerInteractions playerInteraction = FindObjectOfType<PlayerInteractions>();
        _interactionCollider.enabled = !isFocus;
        Cursor.visible = isFocus;

        if (isFocus)
        {
            playerInteraction.Desactive();
            Cursor.lockState = CursorLockMode.None;
            _focusCamera.depth = 30;
            GetComponentInChildren<EnigmeCadenaMenu>().currentCamera = _focusCamera;
        }
        else
        {
            StartCoroutine(UnlockView(playerInteraction));
        }
    }

    private IEnumerator UnlockView(PlayerInteractions playerInteraction)
    {

        _focusCamera.transform.DOShakePosition(0.3f, 0.005f, 20); //Camera shake feedback

        yield return new WaitForSeconds(1);

        Cursor.lockState = CursorLockMode.Locked;
        //Reactivate player controler
        playerInteraction.Active();
        GetComponentInChildren<EnigmeCadenaMenu>().currentCamera = playerInteraction.controller.camera;
        _focusCamera.depth = -5;
        gameObject.tag = "Untagged";
    }

}
