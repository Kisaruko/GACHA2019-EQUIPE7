using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    #region Attributes

    [SerializeField] private CanvasGroup _reticle;
    [SerializeField] private PlayerInteractions _intercations;
    private bool _displayingReticle;

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
        }
        else
        {
            _reticle.alpha = 0;
        }
    }

    #endregion

    #endregion
}
