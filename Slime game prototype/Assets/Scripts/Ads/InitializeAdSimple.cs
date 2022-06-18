using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using YandexMobileAds;
using YandexMobileAds.Base;

public class InitializeAdSimple : MonoBehaviour
{
    private Interstitial interstitial;
    public void RequestInterstitial()
    {
        string adUnitId = "R-M-1652221-4";
        interstitial = new Interstitial(adUnitId);
        AdRequest request = new AdRequest.Builder().Build();
        interstitial.LoadAd(request);
    }
    private void ShowInterstitial()
    {
        if (this.interstitial.IsLoaded())
        {
            interstitial.Show();
        }
        else
        {
            Debug.Log("Interstitial is not ready yet");
        }
    }
}
