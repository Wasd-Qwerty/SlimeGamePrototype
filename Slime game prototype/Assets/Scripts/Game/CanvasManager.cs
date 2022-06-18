using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public GameObject _gameOverCanvas, _pauseCanvas, _pauseButton;
    void Awake()
    {
        _gameOverCanvas.SetActive(false);
        _pauseCanvas.SetActive(false);
        _pauseButton.SetActive(true);
    }
    public void GameOver()
    {
        _gameOverCanvas.SetActive(true);
        _pauseButton.SetActive(false);
    }
    public void Pause()
    {
        _pauseCanvas.SetActive(true);
    }
    public void Resume()
    {
        _pauseCanvas.SetActive(false);
    }
    public void ResumePlay()
    {
        _pauseButton.SetActive(true);
        _gameOverCanvas.SetActive(false);
    }
}
