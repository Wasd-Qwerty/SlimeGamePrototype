using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player, mainCamera, gameOverCanvas, pauseCanvas, pauseButton, CountObj, BestCountObj, Tutorial;
    private GameObject _audioSource, _obj;
    private float _countText = 0, _bestCountText;
    public string audioTag;
    bool _tutorialIsActive, _timerIsDead, _timerIsResume;
    public InitializeAdsBanner IntAd;
    void Start()
    {
        _audioSource = GameObject.FindWithTag(audioTag);
        _bestCountText = PlayerPrefs.GetFloat("bestCount", _bestCountText);
        gameOverCanvas.SetActive(false);
        pauseCanvas.SetActive(false);
        Time.timeScale = 1;
        IntAd.Show();
        pauseButton.SetActive(true);
        _obj = GameObject.FindWithTag(audioTag);
        if (_obj != null)
        {
            Destroy(this.gameObject);
        }
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
        if (_obj != null)
        {
            if (_timerIsDead)
            {
                if (_audioSource.GetComponent<AudioSource>().pitch > 0)
                {
                    _audioSource.GetComponent<AudioSource>().pitch -= 0.001f;
                }
                else
                {
                    _audioSource.GetComponent<AudioSource>().pitch = 0;
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
    }
    public void GameOver()
    {
        _timerIsDead = true;
        Time.timeScale = 0;
        gameOverCanvas.SetActive(true);
        Tutorial.SetActive(false);
        pauseButton.SetActive(false);
        _timerIsResume = false;
    }
    public void GoToMenu(){
        mainCamera.GetComponent<InitializeAdSimple>().ShowAd();
    }
    public void Replay()
    {
        if (_obj != null)
        {
            _audioSource.GetComponent<AudioSource>().pitch = 1;
        }
        SceneManager.LoadScene(1);
    }
    public void Pause()
    {
        _tutorialIsActive = Tutorial.activeSelf;
        Time.timeScale = 0;
        Tutorial.SetActive(false);
        pauseCanvas.SetActive(true);
    }
    public void Resume()
    {
        pauseCanvas.SetActive(false);
        Tutorial.SetActive(_tutorialIsActive);
        Time.timeScale = 1;
    }
    public void ResumePlay()
    {
        _timerIsResume = true;
        pauseButton.SetActive(true);
        player.transform.position = new Vector2(-7, 7);
        Time.timeScale = 1;
        gameOverCanvas.SetActive(false);

    }
}
