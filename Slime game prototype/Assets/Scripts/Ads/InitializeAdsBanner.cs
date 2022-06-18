using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YandexMobileAds;
using YandexMobileAds.Base;

public class InitializeAdsBanner : MonoBehaviour
{
    private Banner banner;
    string adUnitId = "R-M-1652221-1";
    public void RequestBanner()
    {
        banner = new Banner(adUnitId, AdSize.BANNER_320x50, AdPosition.BottomCenter);
        AdRequest request = new AdRequest.Builder().Build();
        banner.LoadAd(request);
    }
}
