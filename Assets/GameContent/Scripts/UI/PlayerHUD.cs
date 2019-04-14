using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerHUD : MonoBehaviour
{
    #region Attributes

    [SerializeField] private CanvasGroup _reticle = null;
    [SerializeField] private PlayerInteractions _intercations = null;
    private bool _displayingReticle;
    private float _targeReticleAlpha;

    #endregion

    #region Methods

    #region Lifecycle

    private void Start()
    {
        _reticle.alpha = 0;
    }

    private void Update()
    {
        if (_intercations)
        {
            if(_intercations.CanDisplayInteraction != _displayingReticle)
            {
                _displayingReticle = _intercations.CanDisplayInteraction;
                SetDisplayReticle(_displayingReticle);
            }
        }

    }

    #endregion

    #region Private

    /// <summary>
    /// Updates the reticle display state
    /// </summary>
    /// <param name="state"></param>
    private void SetDisplayReticle(bool state)
    {
        if(state == true)
        {
            _reticle.alpha = 1;
            _reticle.transform.DOPunchScale(Vector3.one * 1.02f, 0.3f,1);
        }
        else
        {
            _reticle.alpha = 0;
        }
    }

    #endregion

    #endregion
}