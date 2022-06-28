using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    private float _scoreText = 0, _bestScoreText;
    [SerializeField] private Text _scoreObj, _bestScoreObj;
    private const string key = "bestScore";

    private void Start()
    {
        _bestScoreObj.text = Math.Round(_bestScoreText, 2).ToString();
    }
    void Update()
    {
        _scoreObj.text = Math.Round(_scoreText, 2).ToString();

        if (Time.timeScale == 1)
        {
            _scoreText += Time.deltaTime;
        }

        if (_scoreText > _bestScoreText)
        {
            _bestScoreObj.text = Math.Round(_scoreText, 2).ToString();
        }
    }
    public void CheckRecord()
    {
        if (_scoreText > _bestScoreText)
        {
            _bestScoreText = _scoreText;
            Save();
        }
    }
    public void Load(SaveDate.PlayerProfile data)
    {
        if (data == null)
        {
            data = SaveManager.Load<SaveDate.PlayerProfile>(key);
        }
        _bestScoreText = data.bestScoreText;
    }
    private void Save()
    {
        CloudSaveManager.Instance.Save();
        SaveManager.Save(key, GetSaveSnapshot());
    }
    public SaveDate.PlayerProfile GetSaveSnapshot()
    {
        var data = new SaveDate.PlayerProfile()
        {
            bestScoreText = _bestScoreText,
        };
        return data;
    }
}
