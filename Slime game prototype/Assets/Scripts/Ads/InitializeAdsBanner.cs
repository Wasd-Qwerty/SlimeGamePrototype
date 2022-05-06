using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class InitializeAdsBanner : MonoBehaviour
{
    private BannerView bannerView;

#if UNITY_ANDROID
    string bannerUnitId = "ca-app-pub-4625792341181156/1890685023";
#elif UNITY_IPHONE
    string bannerUnitId = "ca-app-pub-4625792341181156/1890685023";
#else
    string bannerUnitId = "ca-app-pub-4625792341181156/1890685023";
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
