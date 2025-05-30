using System;
using GoogleMobileAds.Api;
using UnityEngine;
using Zenject;

namespace AdsModule
{
    public class AdsManager : IInitializable
    {
#if UNITY_ANDROID
        private string _adUnitId = "ca-app-pub-8755299537312287/8411905075";
#elif UNITY_IPHONE
        private string _adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
        private string _adUnitId = "unused";
#endif
        
        private RewardedAd _rewardedAd;
        
        public void Initialize()
        {
            MobileAds.Initialize(AdsInitializationComplete);
        }

        public void ShowRewardedAd(Action onRewarded, Action onFailed)
        {
            if (_rewardedAd != null && _rewardedAd.CanShowAd())
            {
                _rewardedAd.Show((reward) =>
                {
                    onRewarded?.Invoke();
                    Debug.Log($"Rewarded ad rewarded the user. Type: {reward.Type}, amount: {reward.Amount}");
                });
            }
            else
            {
                onFailed?.Invoke();
                Debug.Log("Failed to load ad");
            }
            
            LoadRewardedAd();
        }
        
        private void AdsInitializationComplete(InitializationStatus status)
        {
            LoadRewardedAd();
            RegisterReloadHandler(_rewardedAd);
        }

        private void LoadRewardedAd()
        {
            if (_rewardedAd != null)
            {
                _rewardedAd.Destroy();
                _rewardedAd = null;
            }

            var adRequest = new AdRequest();
            
            RewardedAd.Load(_adUnitId, adRequest, (ad, error) =>
            {
                if (error != null || ad == null)
                {
                    Debug.LogError($"Rewarded ad failed to load an ad with error: {error}");
                    return;
                }

                Debug.Log($"Rewarded ad loaded with response: {ad.GetResponseInfo()}");
                
                _rewardedAd = ad;
            });
        }
        
        private void RegisterReloadHandler(RewardedAd ad)
        {
            // Raised when the ad closed full screen content.
            ad.OnAdFullScreenContentClosed += () =>
            {
                Debug.Log("Rewarded Ad full screen content closed.");

                // Reload the ad so that we can show another as soon as possible.
                LoadRewardedAd();
            };
            // Raised when the ad failed to open full screen content.
            ad.OnAdFullScreenContentFailed += (AdError error) =>
            {
                Debug.LogError("Rewarded ad failed to open full screen content " +
                               "with error : " + error);

                // Reload the ad so that we can show another as soon as possible.
                LoadRewardedAd();
            };
        }
    }
}