using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YandexMobileAds;
using YandexMobileAds.Base;

public class YandexBanner : MonoBehaviour
{
    private Banner _banner;
    private void Awake()
    {
        this.RequestBanner();
    }
    private void RequestBanner()
    {

        string adUnitId = "R-M-1652221-5";


        if (this._banner != null)
        {
            this._banner.Destroy();
        }

        this._banner = new Banner(adUnitId, AdSize.BANNER_320x50, AdPosition.TopCenter);

        this._banner.OnAdLoaded += this.HandleAdLoaded;
        this._banner.OnAdFailedToLoad += this.HandleAdFailedToLoad;
        this._banner.OnReturnedToApplication += this.HandleReturnedToApplication;
        this._banner.OnLeftApplication += this.HandleLeftApplication;
        this._banner.OnAdClicked += this.HandleAdClicked;
        this._banner.OnImpression += this.HandleImpression;

        this._banner.LoadAd(this.CreateAdRequest());
    }
    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder().Build();
    }
    #region Banner callback handlers

    public void HandleAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
        this._banner.Show();
    }

    public void HandleAdFailedToLoad(object sender, AdFailureEventArgs args)
    {
        MonoBehaviour.print("HandleAdFailedToLoad event received with message: " + args.Message);
    }

    public void HandleLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleLeftApplication event received");
    }

    public void HandleReturnedToApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleReturnedToApplication event received");
    }

    public void HandleAdLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeftApplication event received");
    }

    public void HandleAdClicked(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClicked event received");
    }

    public void HandleImpression(object sender, ImpressionData impressionData)
    {
        var data = impressionData == null ? "null" : impressionData.rawData;
        MonoBehaviour.print("HandleImpression event received with data: " + data);
    }

    #endregion
}
