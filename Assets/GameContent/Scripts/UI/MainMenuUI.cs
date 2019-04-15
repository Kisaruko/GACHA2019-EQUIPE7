using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    #region Attributes

    [SerializeField] private CanvasGroup _startActivePanel = null;
    public Dropdown colorBlindDropdown;
    public GameObject cadenas;

    #endregion



    #region Methods

    #region Lifecycle

    private void Start()
    {
        if (_startActivePanel != null)
        {
            SetCurrentActivePannel(_startActivePanel);
        }
        PopulateColorBlindMode();
    }

    private void Update()
    {
        SetSettings();
    }

    #endregion

    private void PopulateColorBlindMode()
    {
        string[] colorBlindNames = Enum.GetNames(typeof(ColorBlindMode));
        List<string> names = new List<string>(colorBlindNames);

        colorBlindDropdown.AddOptions(names);
    }

    #region Public 


    /// <summary>
    /// Loads the scene given in the parameter
    /// </summary>
    /// <param name="scene"></param>
    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void SetSettings()
    {
        AllManager.Instance.colorBlindEnumIndex = colorBlindDropdown.value;
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

    public void SetCadenasActive()
    {
        cadenas.SetActive(!cadenas.activeSelf);
    }

    /// <summary>
    /// Leave application
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }

    #endregion

    #endregion
}
