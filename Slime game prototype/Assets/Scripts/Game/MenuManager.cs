using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Sprite newSpritePlay, newSpriteQuit, AudioOn, AudioOff;
    private GameObject AudioSource;
    public string audioTag;
    public Image play, quit, AudioController;
    private void Start() {
        Time.timeScale = 1;
        AudioSource = GameObject.FindWithTag(audioTag);
        if (AudioSource.GetComponent<AudioSource>().enabled)
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
        if (AudioSource.GetComponent<AudioSource>().enabled)
        {
            AudioController.sprite = AudioOff;
        }
        else
        {
            AudioController.sprite = AudioOn;
        }
        AudioSource.GetComponent<AudioSource>().enabled = !AudioSource.GetComponent<AudioSource>().enabled;
    }
    public void Quit(){
        quit.sprite = newSpriteQuit;
        Application.Quit();
    }
}
