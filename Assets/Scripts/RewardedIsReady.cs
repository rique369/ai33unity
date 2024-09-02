using Gley.MobileAds;
using Gley.MobileAds.Internal;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardedIsReady : MonoBehaviour
{
    private Button button;
    private void Start()
    {
        button = GetComponent<Button>();
    }

    void FixedUpdate()
    {
        button.interactable = API.IsRewardedVideoAvailable();
    }

    public void Rewarded()
    {
        AdController.ShowRewardedAD();
    }
}
