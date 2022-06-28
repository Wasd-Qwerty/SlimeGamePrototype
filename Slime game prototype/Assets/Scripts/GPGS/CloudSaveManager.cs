using GooglePlayGames.BasicApi.SavedGame;
using GooglePlayGames.BasicApi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.IO;

public class CloudSaveManager : MonoBehaviour
{
    public static CloudSaveManager Instance { get; private set; }

    public static SaveDate.PlayerProfile SaveDate { get; private set; }

    [SerializeField]
    private DataSource _dataSource;
    [SerializeField]
    private ConflictResolutionStrategy _conflict;
    [SerializeField] private ScoreManager _sm;
    private string saveName = "save_0";

    private BinaryFormatter _formatter;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        _formatter = new BinaryFormatter();
        SaveDate = new SaveDate.PlayerProfile();
    }
    public void Save()
    {
        if (Authentication.Autenticated)
        {
            SaveToCloud();
        }
    }
    public void Load()
    {
        if (Authentication.Autenticated)
        {
            LoadFormCloud();
        }
    }
    public void UseLocalData()
    {
        _sm.Load(null);
    }
    public void ApplyCloudDate(SaveDate.CloudSaveDate data, bool dataExists)
    {
        if (!dataExists || data == null)
        {
            UseLocalData();
            return;
        }
        _sm.Load(data.Profile);
    }

    private SaveDate.CloudSaveDate CollectAllData()
    {
        var data = new SaveDate.CloudSaveDate()
        {
            Profile = _sm.GetSaveSnapshot(),
        };
        return data;
    }

    private void SaveToCloud()
    {
        OpenCloudSave(OnSaveResponse);
    }

    private void OnSaveResponse(SavedGameRequestStatus status, ISavedGameMetadata metadata)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            var ramData = CollectAllData();
            if (ramData == null)
            {
                return;
            }
            var data = SerializeSaveData(ramData);
            if (data == null)
            {
                return;
            }
            var update = new SavedGameMetadataUpdate.Builder().Build();
            Authentication.Platform.SavedGame.CommitUpdate(metadata, update, data, SaveCallback);
        }
        else
        {
            Debug.LogError("OnSaveResponse error!");
        }
    }
    private void SaveCallback(SavedGameRequestStatus status, ISavedGameMetadata metadata)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            Debug.Log("Data saved succefully!");
        }
        else
        {
            Debug.Log("Data is not saved because of some error!");
        }
    }
    private void LoadFormCloud()
    {
        OpenCloudSave(OnLoadResponse);
    }
    private void OnLoadResponse(SavedGameRequestStatus status, ISavedGameMetadata metadata)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            Authentication.Platform.SavedGame.ReadBinaryData(metadata, LoadCallback);
        }
        else
        {
            UseLocalData();
        }
    }
    private void LoadCallback(SavedGameRequestStatus status, byte[] data)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            ApplyCloudDate(DeserializeSaveData(data), data.Length > 0);
        }
        else
        {
            UseLocalData();
        }
    }
    private void OpenCloudSave(Action<SavedGameRequestStatus, ISavedGameMetadata> callback)
    {
        if (!Social.localUser.authenticated || !PlayGamesClientConfiguration.DefaultConfiguration.EnableSavedGames || string.IsNullOrEmpty(saveName))
        {
            Debug.LogError("OpenCloud Save Error!");
        } 
        Authentication.Platform.SavedGame.OpenWithAutomaticConflictResolution(saveName, _dataSource, _conflict, callback);
    }

    private byte[] SerializeSaveData(SaveDate.CloudSaveDate data)
    {
        try
        {
            using (MemoryStream ms = new MemoryStream())
            {
                _formatter.Serialize(ms, data);
                return ms.GetBuffer();
            }
        }
        catch (Exception e)
        {

            Debug.LogError(e);
            return null;
        }
    }
    private SaveDate.CloudSaveDate DeserializeSaveData(byte[] bytes)
    {
        if (bytes == null || bytes.Length == 0)
        {
            return null;
        }
        try
        {
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                return (SaveDate.CloudSaveDate)_formatter.Deserialize(ms);
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            return null;
        }
    }
}
