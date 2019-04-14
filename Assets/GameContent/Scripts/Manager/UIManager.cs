using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public event Action<bool> OnPause = null;
    public GameObject pauseAnchor = null;
    public event Action OnLoadScene = null;
    public GameObject loadingAnchor = null;
    public bool isPaused = false;
    public Image image;

    public static UIManager Instance = null;

    public void BackToMenu()
    {
        LoadScene(0);
    }

    public void LoadNextSceneWithDelay(int _delay)
    {
        StartCoroutine(LoadCoroutine(_delay));
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

    private IEnumerator LoadCoroutine(int _delay)
    {
        yield return new WaitForSeconds(_delay);
        if (image != null) image.DOColor(new Color(0, 0, 0, 0), 1);
        yield return new WaitForSeconds(1);
       LoadNextScene();
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
