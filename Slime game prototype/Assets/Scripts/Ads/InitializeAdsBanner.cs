using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class InitializeAdsBanner : MonoBehaviour
{
    private BannerView bannerView;

#if UNITY_ANDROID
    string bannerUnitId = "ca-app-pub-3940256099942544/6300978111";
#elif UNITY_IPHONE
    string bannerUnitId = "ca-app-pub-3940256099942544/6300978111";
#else
    string bannerUnitId = "ca-app-pub-3940256099942544/6300978111";
#endif
    private void OnEnable()
    {
        bannerView = new BannerView(bannerUnitId, AdSize.Banner, AdPosition.Top);
        AdRequest request = new AdRequest.Builder().Build();
        bannerView.LoadAd(request);
    }
    public void Show()
    {
        bannerView.Show();
    }
}
