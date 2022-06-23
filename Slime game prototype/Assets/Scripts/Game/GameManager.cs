using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _player, _mainCamera, CountObj, BestCountObj, Tutorial;
    [SerializeField] private GameObject _audioSource;
    [SerializeField] private CanvasManager _canvas;
    private float _countText = 0, _bestCountText;
    public string audioTag;
    [SerializeField] bool _tutorialIsActive, _timerIsDead, _timerIsResume, _isPaused;
    public InitializeAdsBanner IntAd;
    void Start()
    {
        _audioSource = GameObject.FindWithTag(audioTag);
        _bestCountText = PlayerPrefs.GetFloat("bestCount", _bestCountText);
        Time.timeScale = 1;
        IntAd.Show();
    }
    void Update()
    {
        BestCountObj.GetComponent<Text>().text = Math.Round(_bestCountText, 2).ToString();
        if (Time.timeScale == 1)
        {
            _countText += Time.deltaTime;
        }
        CountObj.GetComponent<Text>().text = Math.Round(_countText, 2).ToString();

        if (_countText > _bestCountText)
        {
            _bestCountText = _countText;
            PlayerPrefs.SetFloat("bestCount", _bestCountText);
            PlayerPrefs.Save();
        }
        if (_audioSource != null)
        {
            if (_timerIsDead)
            {
                if (_audioSource.GetComponent<AudioSource>().pitch > 0)
                {
                    _audioSource.GetComponent<AudioSource>().pitch -= 0.01f;
                }
                else
                {
                    _audioSource.GetComponent<AudioSource>().pitch = 0f;
                    _timerIsDead = false;
                }
            }
            if (_timerIsResume)
            {
                if (_audioSource.GetComponent<AudioSource>().pitch > -5)
                {
                    _audioSource.GetComponent<AudioSource>().pitch -= 0.01f;
                }
                else
                {
                    _audioSource.GetComponent<AudioSource>().pitch = 1;
                    _timerIsResume = false;
                }
            }
        }
        if (_isPaused)
        {
            Pause();
        }
    }
    public void GameOver()
    {
        _canvas.GameOver();
        _timerIsDead = true;
        Time.timeScale = 0;
        Tutorial.SetActive(false);
        _timerIsResume = false;
    }
    public void GoToMenu(){
        _mainCamera.GetComponent<InitializeAdSimple>().ShowAd();
    }
    public void Replay()
    {
        if (_audioSource != null)
        {
            _audioSource.GetComponent<AudioSource>().pitch = 1;
        }
        SceneManager.LoadScene(1);
    }
    public void Pause()
    {
        _canvas.Pause();
        _tutorialIsActive = Tutorial.activeSelf;
        Time.timeScale = 0;
        Tutorial.SetActive(false);
    }
    public void Resume()
    {
        _canvas.Resume();
        Tutorial.SetActive(_tutorialIsActive);
        Time.timeScale = 1;
    }
    public void ResumePlay()
    {
        _canvas.ResumePlay();
        _timerIsResume = true;
        _player.transform.position = new Vector2(-7, 7);
        Time.timeScale = 1;
    }
    void OnApplicationQuit()
    {
        IntAd.DestroyAd();
        _mainCamera.GetComponent<InitializeAdSimple>().DestroyAd();
    }
    void OnApplicationPause(bool _pauseStatus)
    {
        _isPaused = _pauseStatus;
    }

}
