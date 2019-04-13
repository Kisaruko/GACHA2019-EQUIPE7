using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    #region Attributes

    [SerializeField] private CanvasGroup _startActivePanel = null;

    #endregion



    #region Methods

    #region Lifecycle

    private void Start()
    {
        if (_startActivePanel != null)
        {
            SetCurrentActivePannel(_startActivePanel);
        }
    }

    #endregion

    #region Public 


    /// <summary>
    /// Loads the scene given in the parameter
    /// </summary>
    /// <param name="scene"></param>
    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }


    /// <summary>
    /// Set active the panel given in the parameter and disactivate the others
    /// </summary>
    /// <param name="panel"></param>
    public void SetCurrentActivePannel(CanvasGroup panel)
    {
        foreach(CanvasGroup curPanel in FindObjectsOfType<CanvasGroup>())
        {
            if (curPanel != panel)
            {
                curPanel.alpha = 0;
                curPanel.interactable = false;
                curPanel.blocksRaycasts = false;
            }
        }

        panel.alpha = 1;
        panel.interactable = true;
        panel.blocksRaycasts = true;
    }

    #endregion

    #endregion
}
