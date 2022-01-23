using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CanvasRenderer))]
public class PauseMenu : MonoBehaviour
{
    public bool IsOpen { get; private set; }

    private float _targetCanvasAlpha = 0f;

    private CanvasGroup _canvas;

    private void Awake()
    {
        ReturnToGame();
    }

    private void Start()
    {
        _canvas = GetComponent<CanvasGroup>();
        _canvas.alpha = _targetCanvasAlpha;
    }

    private void Update()
    {
        _canvas.alpha = Mathf.Lerp(_canvas.alpha, _targetCanvasAlpha, Time.deltaTime * 7f);
    }

    public void ShowMenu()
    {
        IsOpen = true;

        _targetCanvasAlpha = 1f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ReturnToGame()
    {
        IsOpen = false;

        _targetCanvasAlpha = 0f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void BackToMenu()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
