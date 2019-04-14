﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public event Action<bool> OnPause = null;
    public GameObject pauseAnchor = null;
    public event Action OnLoadScene = null;
    public GameObject loadingAnchor = null;
    public bool isPaused = false;

    public static UIManager Instance = null;

    public void BackToMenu()
    {
        LoadScene(0);
    }

    public void LoadNextScene()
    {
        int _index = SceneManager.GetActiveScene().buildIndex;
        if (_index >= SceneManager.sceneCountInBuildSettings - 1) _index = 0;
        else _index++;

        LoadScene(_index);
    }

    public void ReloadScene()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void LoadScene(int _sceneIndex)
    {
        if (isPaused) Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        OnLoadScene?.Invoke();
        loadingAnchor.SetActive(true);
        SceneManager.LoadScene(_sceneIndex);
    }

    public void Pause(bool _doPause)
    {
        // Set cursor
        Cursor.lockState = _doPause ? CursorLockMode.Confined : CursorLockMode.Locked;
        Cursor.visible = _doPause;

        Time.timeScale = _doPause ? 0 : 1;
        isPaused = _doPause;
        pauseAnchor.SetActive(_doPause);

        OnPause?.Invoke(_doPause);
    }

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Pause(!isPaused);
        }
    }
}
