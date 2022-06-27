using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public float bestScoreText;
    private float _scoreText = 0;
    [SerializeField] private Text _scoreObj, _bestScoreObj;
    [Header("Save Config")]
    [SerializeField] private string _savePath;
    [SerializeField] private string _saveFileName = "score.json";


    private void Awake()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        _savePath = Path.Combine(Application.persistentDataPath, _saveFileName);
#else
        _savePath = Path.Combine(Application.dataPath, _saveFileName);
#endif

    }
    private void Start()
    {
        LoadFromFile();
        this._bestScoreObj.text = Math.Round(bestScoreText, 2).ToString();
    }
    void Update()
    {
        this._scoreObj.text = Math.Round(_scoreText, 2).ToString();

        if (Time.timeScale == 1)
        {
            this._scoreText += Time.deltaTime;
        }

        if (_scoreText > bestScoreText)
        {
            _bestScoreObj.text = Math.Round(_scoreText, 2).ToString();
        }
    }
    public void CheckRecord()
    {
        if (this._scoreText > this.bestScoreText)
        {
            bestScoreText = this._scoreText;
            SaveToFile();
        }
    }
    public void ReceiveDate(float bscore)
    {
        bestScoreText = bscore;
        SaveToFile();
    }
    void SaveToFile()
    {
        ScoreSave scoreSave = new ScoreSave
        {
            _bestScoreText = this.bestScoreText
        };

        string json = JsonUtility.ToJson(scoreSave, prettyPrint: true);

        try
        {
            File.WriteAllText(_savePath, contents: json);
            Debug.Log(message: "{GameLog} => [ScoreSave] - (<color=green>Succefull</color>)");
        }
        catch (Exception e)
        {
            Debug.Log(message: "{GameLog} => [ScoreSave] - (<color=red>Error</color>) - SaveToFile ->" + e.Message);
        }
    }

    void LoadFromFile()
    {
        if (!File.Exists(_savePath))
        {
            Debug.Log(message: "{GameLog} => [ScoreSave] - LoadFromFile -> File not found!");
            return;
        }
        try
        {
            string json = File.ReadAllText(_savePath);

            ScoreSave scoreSaveJson = JsonUtility.FromJson<ScoreSave>(json);
            this.bestScoreText = scoreSaveJson._bestScoreText;
        }
        catch (Exception e)
        {
            Debug.Log(message: "{GameLog} => [ScoreSave] - (<color=red>Error</color>) - LoadFromFile ->" + e.Message);
        }
    }
}
