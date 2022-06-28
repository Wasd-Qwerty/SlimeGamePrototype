using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;


public class Authentication : MonoBehaviour
{
    public static bool Autenticated { get; private set; }
    public static PlayGamesPlatform Platform { get; private set; }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);        
    }
    private void Start()
    {
        Login();
    }
    private void Login()
    {
        if (Platform == null)
        {
            Platform = BuildPlatform();
        }
        PlayGamesPlatform.Instance.Authenticate(success =>
        {
            Autenticated = success;
            OnAutentificationSucceded();
        });
    }

    private void OnAutentificationSucceded()
    {
        if (Autenticated)
        {
            CloudSaveManager.Instance.Load();
        }
        else
        {
            CloudSaveManager.Instance.UseLocalData();
        }
    }

    private PlayGamesPlatform BuildPlatform()
    {
        var builder = new PlayGamesClientConfiguration.Builder();
        builder.EnableSavedGames();

        PlayGamesPlatform.InitializeInstance(builder.Build());
        PlayGamesPlatform.DebugLogEnabled = true;

        return PlayGamesPlatform.Activate();
    }
}
