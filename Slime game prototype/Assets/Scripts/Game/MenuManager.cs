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

    private void Start() {
        Time.timeScale = 1;
        _audioSource = GameObject.FindWithTag(audioTag);
        if (_audioSource.GetComponent<AudioSource>().enabled)
        {
            AudioController.sprite = AudioOn;
        }
        else
        {
            AudioController.sprite = AudioOff;
        }
    }
    public void Play(){
        play.sprite = newSpritePlay;
        SceneManager.LoadScene(1);
    }
    public void AudioControl()
    {
        if (_audioSource.GetComponent<AudioSource>().enabled)
        {
            AudioController.sprite = AudioOff;
        }
        else
        {
            AudioController.sprite = AudioOn;
        }
        _audioSource.GetComponent<AudioSource>().enabled = !_audioSource.GetComponent<AudioSource>().enabled;
    }
    public void Quit(){
        quit.sprite = newSpriteQuit;
        Application.Quit();
    }
}
