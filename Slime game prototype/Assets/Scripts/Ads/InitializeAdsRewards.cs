using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
public class InitializeAdsRewards : MonoBehaviour
{
    private RewardedAd rewardedAd;
    public GameObject player, resumeButton;
#if UNITY_ANDROID
    private const string rewardedUnitId = "ca-app-pub-3940256099942544/5224354917"; //тестовый айди
#elif UNITY_IPHONE
    private const string rewardedUnitId = "";
#else
    private const string rewardedUnitId = "unexpected_platform";
#endif
    void OnEnable()
    {
        rewardedAd = new RewardedAd(rewardedUnitId);
        AdRequest adRequest = new AdRequest.Builder().Build();
        rewardedAd.LoadAd(adRequest);
        
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
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
        player.GetComponent<PlayerControl>().ResumePlay();
    }
}
