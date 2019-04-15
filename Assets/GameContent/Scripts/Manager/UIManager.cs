using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Fields / Properties
    public event Action<bool> OnPause = null;
    public GameObject pauseAnchor = null;
    public event Action OnLoadScene = null;
    public GameObject loadingAnchor = null;
    public bool isPaused = false;
    public Image image;

    public static UIManager Instance = null;
    #endregion

    #region Methods

    #region Original Methods
    public void BackToMenu()
    {
        LoadScene(0);
    }

    public void LoadNextSceneWithDelay(int _delay)
    {
        StartCoroutine(LoadCoroutine(_delay, LoadNextScene));
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

    public void ReloadSceneWithDelay(int _delay)
    {
        StartCoroutine(LoadCoroutine(_delay, ReloadScene));
    }

    private IEnumerator LoadCoroutine(int _delay, Action _callback)
    {
        yield return new WaitForSeconds(_delay);
        if (image != null) image.DOColor(new Color(0, 0, 0, 1), 1);
        yield return new WaitForSeconds(1);
        _callback?.Invoke();
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

    public void SetMatriochka(bool _isValid)
    {
        if (!AllManager.Instance) return;

        int _index = SceneManager.GetActiveScene().buildIndex - 1;
        if (_index < AllManager.Instance.Matriochka.Length) AllManager.Instance.Matriochka[_index] = _isValid;
    }
    #endregion

    #region Unity Methods
    private void Awake()
    {
        if (!Instance) Instance = this;
        else Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Pause(!isPaused);
        }
    }
    #endregion

    #endregion
}
