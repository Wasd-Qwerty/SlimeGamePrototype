using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverCanvas;
    public GameObject CountObj;
    public GameObject BestCountObj;
    private float _countText = 0;
    private float _bestCountText;
    // Update is called once per frame
    // Start is called before the first frame update
    void Start()
    {
        _bestCountText = PlayerPrefs.GetFloat("bestCount", _bestCountText);
        gameOverCanvas.SetActive(false);
        Time.timeScale = 1;
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

    }
    public void GameOver()
    {
        Time.timeScale = 0;
        gameOverCanvas.SetActive(true);
    }
    public void GoToMenu(){
        SceneManager.LoadScene(0);
    }
    public void Replay()
    {
        SceneManager.LoadScene(1);
    }
}
