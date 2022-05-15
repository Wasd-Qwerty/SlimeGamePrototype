using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;
public class InitializeAdSimple : MonoBehaviour
{
    public InterstitialAd interstitialAd;

#if UNITY_ANDROID
    private const string _interstitialUnitId = "ca-app-pub-3940256099942544/1033173712"; //тестовый айди
#elif UNITY_IPHONE
    private const string _interstitialUnitId = "ca-app-pub-3940256099942544/1033173712";
#else
    private const string _interstitialUnitId = "ca-app-pub-3940256099942544/1033173712";
#endif
    void OnEnable()
    {
        interstitialAd = new InterstitialAd(_interstitialUnitId);
        AdRequest adRequest = new AdRequest.Builder().Build();
        interstitialAd.LoadAd(adRequest);
    }

    public void ShowAd()
    {
        if (interstitialAd.IsLoaded())
        {
            interstitialAd.Show();
        }
        else
        {
            SceneManager.LoadScene(0);
        }
        interstitialAd.OnAdClosed += HandleOnAdClosed;
    }
    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        SceneManager.LoadScene(0);
    }
}
