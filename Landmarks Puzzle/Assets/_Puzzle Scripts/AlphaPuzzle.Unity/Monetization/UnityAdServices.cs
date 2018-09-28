using UnityEngine;
using UnityEngine.Advertisements;
using SBK.Unity;
using System.Collections.Generic;

public class UnityAdServices : PSingle<UnityAdServices>
{
    public bool StartBannerAds = false;
    public bool TestAds = false;
    public string PlacementID = "video";
    [Space(10)]
    public bool EnableTimedAds = false;
    [Range(15, 1000)]
    public float AdWaitTimeSec = 60f;
    [Space(10)]
    public string ProjectID = "5c9e966c-440e-4fb8-b447-8c643a724ffe";
    public string AppleAppStoreGameID = "2767634";
    public string GooglePlayStoreGameID = "2767635";

    private float _timer;
    private bool _paused = false;

    protected override void PAwake()
    {
        if (!StartBannerAds || !Advertisement.isSupported) return;
#if UNITY_ANDROID
        Advertisement.Initialize(GooglePlayStoreGameID, TestAds);
#elif UNITY_IPHONE
            Advertisement.Initialize(AppleAppStoreGameID,TestAds);
#endif
    }

    protected override void PDestroy()
    {

    }

    void Update()
    {
        if (!EnableTimedAds || !StartBannerAds || _paused) return;
        _timer += Time.deltaTime;
        if (_timer > AdWaitTimeSec)
        {
            ShowAd();
            _timer = 0f;
        }
    }

    public void ShowAd()
    {
        if (!StartBannerAds || !Advertisement.isSupported) return;
        if (Advertisement.IsReady(PlacementID))
        {
            var options = new ShowOptions
            {
                resultCallback = HandleShowResult
            };
            Advertisement.Show(PlacementID, options);
        }
    }


    public void CloseAd()
    {

    }

    void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("Video completed - Offer a reward to the player");
                break;
            case ShowResult.Skipped:
                Debug.LogWarning("Video was skipped - Do NOT reward the player");
                break;
            case ShowResult.Failed:
                Debug.LogError("Video failed to show");
                break;
        }
    }

    public void PauseAd()
    {
        _paused = true;
    }

    public void UnPauseAd()
    {
        _paused = false;
    }

}
