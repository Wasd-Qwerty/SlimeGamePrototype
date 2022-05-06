using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
public class InitializeAdSimple : MonoBehaviour
{
    private InterstitialAd interstitialAd;

#if UNITY_ANDROID
    private const string interstitialUnitId = "ca-app-pub-4625792341181156/1341758799"; //тестовый айди
#elif UNITY_IPHONE
    private const string interstitialUnitId = "ca-app-pub-4625792341181156/1341758799";
#else
    private const string interstitialUnitId = "ca-app-pub-4625792341181156/1341758799";
#endif
    void OnEnable()
    {
        interstitialAd = new InterstitialAd(interstitialUnitId);
        AdRequest adRequest = new AdRequest.Builder().Build();
        interstitialAd.LoadAd(adRequest);
    }

    public void ShowAd()
    {
        if (interstitialAd.IsLoaded())
        {
            interstitialAd.Show();
        }
    }
}
