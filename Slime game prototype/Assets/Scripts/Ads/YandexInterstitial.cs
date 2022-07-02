using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YandexMobileAds;
using YandexMobileAds.Base;
using UnityEngine.SceneManagement;

public class YandexInterstitial : MonoBehaviour
{
    private Interstitial _interstitial;
    // Start is called before the first frame update
    public void RequestInterstitial()
    {
        string adUnitId = "R-M-1652221-4";

        if (this._interstitial != null)
        {
            this._interstitial.Destroy();
        }
        this._interstitial = new Interstitial(adUnitId);
        this._interstitial.OnInterstitialLoaded += this.HandleInterstitialLoaded;
        this._interstitial.OnInterstitialFailedToLoad += this.HandleInterstitialFailedToLoad;
        this._interstitial.OnReturnedToApplication += this.HandleReturnedToApplication;
        this._interstitial.OnLeftApplication += this.HandleLeftApplication;
        this._interstitial.OnAdClicked += this.HandleAdClicked;
        this._interstitial.OnInterstitialShown += this.HandleInterstitialShown;
        this._interstitial.OnInterstitialDismissed += this.HandleInterstitialDismissed;
        this._interstitial.OnImpression += this.HandleImpression;
        this._interstitial.OnInterstitialFailedToShow += this.HandleInterstitialFailedToShow;

        this._interstitial.LoadAd(this.CreateAdRequest());
    }
    public void ShowInterstitial()
    {
        if (this._interstitial.IsLoaded())
        {
            this._interstitial.Show();
        }
        else
        {
            MonoBehaviour.print("Interstitial is not ready yet");
        }
    }

    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder().Build();
    }

    #region Interstitial callback handlers

    public void HandleInterstitialLoaded(object sender, EventArgs args)
    {
        this.ShowInterstitial();
        SceneManager.LoadScene(0);
        MonoBehaviour.print("HandleInterstitialLoaded event received");
    }

    public void HandleInterstitialFailedToLoad(object sender, AdFailureEventArgs args)
    {
        SceneManager.LoadScene(0);
        MonoBehaviour.print(
            "HandleInterstitialFailedToLoad event received with message: " + args.Message);
    }

    public void HandleReturnedToApplication(object sender, EventArgs args)
    {
        SceneManager.LoadScene(0);
        MonoBehaviour.print("HandleReturnedToApplication event received");
    }

    public void HandleLeftApplication(object sender, EventArgs args)
    {
        SceneManager.LoadScene(0);
        MonoBehaviour.print("HandleLeftApplication event received");
    }

    public void HandleAdClicked(object sender, EventArgs args)
    {
        SceneManager.LoadScene(0);
        MonoBehaviour.print("HandleAdClicked event received");
    }

    public void HandleInterstitialShown(object sender, EventArgs args)
    {
        SceneManager.LoadScene(0);
        MonoBehaviour.print("HandleInterstitialShown event received");
    }

    public void HandleInterstitialDismissed(object sender, EventArgs args)
    {
        SceneManager.LoadScene(0);
        MonoBehaviour.print("HandleInterstitialDismissed event received");
        this._interstitial.Destroy();
    }

    public void HandleImpression(object sender, ImpressionData impressionData)
    {
        SceneManager.LoadScene(0);
    }

    public void HandleInterstitialFailedToShow(object sender, AdFailureEventArgs args)
    {
        SceneManager.LoadScene(0);
        MonoBehaviour.print(
            "HandleInterstitialFailedToShow event received with message: " + args.Message);
    }

    #endregion
}
