using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GroundManager _groundManager;
    [SerializeField] private CanvasManager _canvas;
    [SerializeField] private GameObject _player, _mainCamera, _tutorial, _audioSource, _resumeButton;
    [SerializeField] private ScoreManager _sm;
    public string audioTag;
    [SerializeField] bool _tutorialIsActive, _timerIsDead, _timerIsResume, _isPaused;
    /*[SerializeField] YandexInterstitial _interstitial;*/
    /*[SerializeField] YandexRewardedAd _rewardedAd;*/
    void Start()
    {
        _audioSource = GameObject.FindWithTag(audioTag);
        Time.timeScale = 1;
    }
    void Update()
    {
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
        Time.timeScale = 0;
        _tutorial.SetActive(false);
        _timerIsDead = true;
        _timerIsResume = false;
        _sm.CheckRecord();
    }
    public void GoToMenu(){
        /*_interstitial.RequestInterstitial();*/
        SceneManager.LoadScene(0);
        _sm.CheckRecord();
    }
    public void Replay()
    {
        _sm.CheckRecord();
        if (_audioSource != null)
        {
            _audioSource.GetComponent<AudioSource>().pitch = 1;
        }
        _timerIsDead = false;
        SceneManager.LoadScene(1);
    }
    public void Pause()
    {
        _canvas.Pause();
        _tutorialIsActive = _tutorial.activeSelf;
        Time.timeScale = 0;
        _tutorial.SetActive(false);
        _sm.CheckRecord();
    }
    public void Resume()
    {
        _canvas.Resume();
        _tutorial.SetActive(_tutorialIsActive);
        Time.timeScale = 1;
    }
    public void ResumePlay()
    {
        Destroy(_resumeButton.gameObject);
        /*_rewardedAd.RequestRewardedAd();*/
        _canvas.ResumePlay();
        _timerIsResume = true;
        _timerIsDead = false;
        _player.transform.position = new Vector2(-7, 7);
        _groundManager.ResetSpeed();
        Time.timeScale = 1f;
    }
    void OnApplicationPause(bool _pauseStatus)
    {
        _isPaused = _pauseStatus;
        _sm.CheckRecord();
    }
    private void OnApplicationQuit()
    {
        _sm.CheckRecord();
        /*_gpgs.OpenSavedGame(true);*/
    }
}
