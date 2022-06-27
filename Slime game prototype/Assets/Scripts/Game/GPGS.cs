using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using System;
using System.Text;
using UnityEngine.UI;

public class GPGS : MonoBehaviour
{
    private bool _isSaving;

    [SerializeField] ScoreManager _sm;
    private float _bScoreText;
    private DateTime startDateTime;

    public void Start()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    internal void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            startDateTime = DateTime.Now;
            OpenSavedGame(false);
        }
        else
        {
            // Disable your integration with Play Games Services or show a login button
            // to ask users to sign-in. Clicking it should call
            // PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication).
        }
    }

    public void OpenSavedGame(bool saving)
    {
        _isSaving = saving;
        OpenSavedGame("Score");
    }

    void OpenSavedGame(string filename)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.OpenWithAutomaticConflictResolution(filename, DataSource.ReadCacheOrNetwork,
            ConflictResolutionStrategy.UseLongestPlaytime, OnSavedGameOpened);
    }

    public void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            if (_isSaving)
            {
                string data = Math.Round(_sm.bestScoreText, 2).ToString();
                byte[] saveData = Encoding.UTF8.GetBytes(data);
                SaveGame(game, saveData);
            }
            else
            {
                LoadGameData(game);
            }
        }
        else
        {
            // handle error
        }
    }

    void SaveGame(ISavedGameMetadata game, byte[] savedData)
    {
        TimeSpan currentSpan = DateTime.Now - startDateTime;
        TimeSpan totalPlaytime = game.TotalTimePlayed + currentSpan;
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

        SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();
        builder = builder
            .WithUpdatedPlayedTime(totalPlaytime)
            .WithUpdatedDescription("Saved game at " + DateTime.Now);

        SavedGameMetadataUpdate updatedMetadata = builder.Build();
        savedGameClient.CommitUpdate(game, updatedMetadata, savedData, OnSavedGameWritten);
    }

    void OnSavedGameWritten(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            Debug.Log("Save is completed!");
        }
        else
        {
            // handle error
        }
    }
    void LoadGameData(ISavedGameMetadata game)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.ReadBinaryData(game, OnSavedGameDataRead);
    }

    void OnSavedGameDataRead(SavedGameRequestStatus status, byte[] data)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            if (data.Length > 0)
            {
                string dataGoogle = Encoding.ASCII.GetString(data);

                _bScoreText = float.Parse(dataGoogle);
                _sm.ReceiveDate(_bScoreText);
            }
            else
            {
                Debug.Log("Date is empty");
            }
        }
        else
        {
            // handle error
        }
    }
}
