using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Sprite newSpritePlay, newSpriteQuit;
    public Image play, quit;
    private void Start() {
        Time.timeScale = 1;
    }
    public void Play(){
        play.sprite = newSpritePlay;
        SceneManager.LoadScene(1);
    }
    public void Quit(){
        quit.sprite = newSpriteQuit;
        Application.Quit();
    }
}
