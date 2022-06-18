using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YandexMobileAds;
using YandexMobileAds.Base;

public class InitializeAdsRewards : MonoBehaviour
{
    private RewardedAd rewardedAd;
    public GameManager gm;
    public void RequestRewardedAd()
    {
        string adUnitId = "R-M-1652221-3";
        rewardedAd = new RewardedAd(adUnitId);
        AdRequest request = new AdRequest.Builder().Build();
        rewardedAd.LoadAd(request);
        rewardedAd.OnRewardedAdShown += this.HandleRewardedAdShown;
    }

    private void HandleRewardedAdShown(object sender, EventArgs e)
    {
        gm.ResumePlay();
    }

    private void ShowRewardedAd()
    {
        if (this.rewardedAd.IsLoaded())
        {
            rewardedAd.Show();
        }
        else
        {
            Debug.Log("Rewarded Ad is not ready yet");
        }
    }
}
