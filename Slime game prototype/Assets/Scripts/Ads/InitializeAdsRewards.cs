using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class InitializeAdsRewards : MonoBehaviour
{
    private RewardedAd rewardedAd;
    public GameObject GameManager, resumeButton;
#if UNITY_ANDROID
    private const string rewardedUnitId = "ca-app-pub-3940256099942544/5224354917"; //тестовый айди
#elif UNITY_IPHONE
    private const string rewardedUnitId = "ca-app-pub-3940256099942544/5224354917";
#else
    private const string rewardedUnitId = "ca-app-pub-3940256099942544/5224354917";
#endif
    void OnEnable()
    {
        rewardedAd = new RewardedAd(rewardedUnitId);
        AdRequest adRequest = new AdRequest.Builder().Build();
        rewardedAd.LoadAd(adRequest);
        if (rewardedAd.IsLoaded())
        {
            resumeButton.SetActive(true);
        }
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
    }

    void OnDisable()
    {
        rewardedAd.OnUserEarnedReward -= HandleUserEarnedReward;
    }

    public void ShowRewardedAd()
    {
        if (rewardedAd.IsLoaded())
        {
            rewardedAd.Show();
        }
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        Destroy(resumeButton.gameObject);
        GameManager.GetComponent<GameManager>().ResumePlay();
        rewardedAd.Destroy();
    }
    public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Destroy(resumeButton.gameObject);
    }
}
