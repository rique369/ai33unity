using UnityEngine;
namespace Gley.MobileAds.Internal
{
    public class AdController : MonoBehaviour
    {
        private static AdController instance;

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        #region GameCallbacks
        private string bonusName;
        private int bonuseNumber;
        private bool generated;
        private static bool freecoin;
        private int BookCount, EmeraldCount, PipeCount, GoldCount;
        private int coinsc2;
        private void InitReward()
        {
            if (ProtectedPrefs.HasKey("Coins"))
            {
                coinsc2 = ProtectedPrefs.GetInt("Coins");
            }
            else
            {
                coinsc2 = 0;
            }
        }

        #endregion


        void Start()
        {
            API.Initialize();
            InitReward();
        }
        #region IsRewarded
        public static void ShowRewardedAD()
        {
            if (IsRewardedComplite())
            {
                Gley.MobileAds.API.ShowRewardedVideo(CompleteMethod);
            }
        }

        /// <summary>
        /// Callback called when a rewarded video or interstitial is complete
        /// </summary>
        /// <param name="completed"></param>
        private static void CompleteMethod(bool completed)
        {
            if (completed)
            {
                ProtectedPrefs.SetInt("Coins", ProtectedPrefs.GetInt("Coins") + 500);
            }
            GleyLogger.AddLog($"Completed: {completed}");
        }

        public static bool IsRewardedComplite()
        {
            return Gley.MobileAds.API.IsRewardedVideoAvailable();
        }
        #endregion
        #region Interstitilar
        public static void ShowInterstitilar()
        {
            if (Gley.MobileAds.API.IsInterstitialAvailable())
                Gley.MobileAds.API.ShowInterstitial();
        }
        #endregion
        #region Banner
        public static void ShowBanner()
        {
            API.ShowBanner(BannerPosition.Bottom, BannerType.Adaptive);
        }
        public static void ShowBannerTOP()
        {
            API.ShowBanner(BannerPosition.Top, BannerType.Adaptive);
        }

        public static void HideBanner()
        {
            API.HideBanner();
        }
        #endregion

    }
}
