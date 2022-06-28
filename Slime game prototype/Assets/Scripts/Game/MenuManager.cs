using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Sprite newSpritePlay, newSpriteQuit, AudioOn, AudioOff;
    private GameObject _audioSource;
    public string audioTag;
    public Image play, quit, AudioController;
    [SerializeField] private int _audioIsOn;

    private void Start() {
        Time.timeScale = 1;
        _audioSource = GameObject.FindWithTag(audioTag);
        _audioSource.GetComponent<AudioSource>().pitch = 1;
        
        _audioIsOn = PlayerPrefs.GetInt("audioIsOn", _audioIsOn);
        
        if (_audioIsOn == 0)
        {
            _audioSource.GetComponent<AudioSource>().enabled = false;
            AudioController.sprite = AudioOff;
        }
        else
        {
            _audioSource.GetComponent<AudioSource>().enabled = true;
            AudioController.sprite = AudioOn;
        }
    }
    public void Play(){
        play.sprite = newSpritePlay;
        SceneManager.LoadScene(1);
    }
    public void AudioControl()
    {
        _audioSource.GetComponent<AudioSource>().enabled = !_audioSource.GetComponent<AudioSource>().enabled;
        if (_audioSource.GetComponent<AudioSource>().enabled)
        {
            _audioIsOn = 1;
            AudioController.sprite = AudioOn;
        }
        else
        {
            _audioIsOn = 0;
            AudioController.sprite = AudioOff;
        }
        PlayerPrefs.SetInt("audioIsOn", _audioIsOn);
        PlayerPrefs.Save();
    }
    public void Quit(){
        quit.sprite = newSpriteQuit;
        Application.Quit();
    }
}
