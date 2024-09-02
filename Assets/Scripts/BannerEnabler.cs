using Gley.MobileAds.Internal;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BannerEnabler : MonoBehaviour
{
    private void OnEnable()
    {
        AdController.ShowBannerTOP();
    }
    private void OnDisable()
    {
        AdController.HideBanner();
    }
}
