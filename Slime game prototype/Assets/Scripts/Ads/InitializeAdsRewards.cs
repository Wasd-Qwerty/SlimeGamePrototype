using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
public class InitializeAdsRewards : MonoBehaviour
{
    private RewardedAd rewardedAd;
    public GameObject player, resumeButton;
#if UNITY_ANDROID
    private const string rewardedUnitId = "ca-app-pub-4625792341181156/3698041971"; //тестовый айди
#elif UNITY_IPHONE
    private const string rewardedUnitId = "ca-app-pub-4625792341181156/3698041971";
#else
    private const string rewardedUnitId = "ca-app-pub-4625792341181156/3698041971";
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
