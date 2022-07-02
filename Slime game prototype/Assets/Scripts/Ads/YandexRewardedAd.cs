using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YandexMobileAds;
using YandexMobileAds.Base;

public class YandexRewardedAd : MonoBehaviour
{
    RewardedAd _rewardedAd;
    [SerializeField] GameObject _resumeButton;
    [SerializeField] GameManager _gm;
    public void RequestRewardedAd()
    {
        if (this._rewardedAd != null)
        {
            this._rewardedAd.Destroy();
        }

        // Replace demo R-M-DEMO-rewarded-client-side-rtb with actual Ad Unit ID
        string adUnitId = "R-M-1652221-3";
        this._rewardedAd = new RewardedAd(adUnitId);

        this._rewardedAd.OnRewardedAdLoaded += this.HandleRewardedAdLoaded;
        this._rewardedAd.OnRewardedAdFailedToLoad += this.HandleRewardedAdFailedToLoad;
        this._rewardedAd.OnReturnedToApplication += this.HandleReturnedToApplication;
        this._rewardedAd.OnLeftApplication += this.HandleLeftApplication;
        this._rewardedAd.OnRewardedAdShown += this.HandleRewardedAdShown;
        this._rewardedAd.OnRewardedAdDismissed += this.HandleRewardedAdDismissed;
        this._rewardedAd.OnImpression += this.HandleImpression;
        this._rewardedAd.OnRewarded += this.HandleRewarded;

        this._rewardedAd.LoadAd(this.CreateAdRequest());
    }
    public void ShowRewardedAd()
    {
        if (this._rewardedAd.IsLoaded())
        {
            this._rewardedAd.Show();
        }
        else
        {
            MonoBehaviour.print("Rewarded Ad is not ready yet");
        }
    }

    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder().Build();
    }

    #region Rewarded Ad callback handlers

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        _resumeButton.SetActive(true);
        Debug.Log("RewardedAd is Load");

    }
    public void HandleRewardedAdFailedToLoad(object sender, AdFailureEventArgs args)
    {
        Debug.Log("HandleRewardedAdFailedToLoad event received with message: » + args.Message");
    }
    public void HandleReturnedToApplication(object sender, EventArgs args)
    {
        Debug.Log("HandleReturnedToApplication event received");
    }
    public void HandleLeftApplication(object sender, EventArgs args)
    {
        Debug.Log("HandleLeftApplication event received");
    }
    public void HandleRewardedAdShown(object sender, EventArgs args)
    {
        Debug.Log("HandleRewardedAdShown event received");
    }
    public void HandleRewardedAdDismissed(object sender, EventArgs args)
    {
        _gm.ResumePlay();
        _resumeButton.SetActive(false);
        Debug.Log("HandleRewardedAdDismissed event received");
    }
    public void HandleImpression(object sender, ImpressionData impressionData)
    {
        Debug.Log("HandleImpression event received with data: " + impressionData);
    }
    public void HandleRewarded(object sender, Reward args)
    {
        Debug.Log("HandleRewarded event received: amout = " + args.amount + ", type = " + args.type);
    }
    #endregion
}
