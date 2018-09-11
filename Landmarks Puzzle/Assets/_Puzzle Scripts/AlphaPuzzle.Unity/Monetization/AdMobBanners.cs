using UnityEngine;
using System;
using SBK.Unity;
using GoogleMobileAds.Api;

public class AdMobBanners : PSingle<AdMobBanners>
{
    public string AdUnitId { get; private set; }    
    public bool StartBannerAds = false;
    public bool TestAds = false;
    public float AdLoadTimer = 0.3f;
    public AdPosition AdPosition = AdPosition.BottomRight;

    private AdSize _adSize;
    private BannerView _bannerView;

    protected override void PAwake()
    {
        if (!StartBannerAds) return;
#if UNITY_EDITOR
        AdUnitId = "editor_platform";
#elif UNITY_ANDROID
                                        AdUnitId = AdConfiguration.AdMob.AndroidAdMobId;
#elif UNITY_IPHONE
                                        AdUnitId = AdConfiguration.AdMob.IOSAdMobId;
#else
                                        AdUnitId = "unexpected_platform";
#endif

        _adSize = AdSize.Banner;
        _bannerView = new BannerView(AdUnitId, _adSize, AdPosition);
        LoadEvents();
    }

    protected override void PDestroy()
    {
        CloseAd();
    }
    
    void Start()
    {
        if (StartBannerAds)
        {
            if (TestAds)
            {
                Invoke("RequestTestBanner", AdLoadTimer);
            }
            else
            {
                Invoke("RequestLiveBanner", AdLoadTimer);
            }
        }
    }

    void OnApplicationQuit()
    {
        if(_bannerView != null)
            _bannerView.Destroy();
    }

    public void ResetBanner(AdSize size, AdPosition position)
    {
        _bannerView = new BannerView(AdUnitId, size, position);
        LoadEvents();
    }

    private void RequestLiveBanner()
    {
        var request = new AdRequest.Builder()
             .TagForChildDirectedTreatment(true)
             .Build();
        _bannerView.LoadAd(request);
    }

    private void RequestTestBanner()
    {
        var request = new AdRequest.Builder()
             .TagForChildDirectedTreatment(true)
             .AddTestDevice(AdRequest.TestDeviceSimulator)
             .AddTestDevice(AdConfiguration.AdMob.TestDeviceId)
             .Build();
        _bannerView.LoadAd(request);
    }
    
    public void CloseAd()
    {
       if(_bannerView != null)
            _bannerView.Destroy();
    }

    public void ShowAdBanner()
    {
        if (_bannerView != null)
            _bannerView.Show();
    }

    public void HideAdBanner()
    {
        if (_bannerView != null)
            _bannerView.Hide();
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {

    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.Log(args.Message);
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {

    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {

    }

    private void LoadEvents()
    {
        _bannerView.OnAdLoaded += HandleOnAdLoaded;
        _bannerView.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        _bannerView.OnAdOpening += HandleOnAdOpened;
        _bannerView.OnAdClosed += HandleOnAdClosed;
    }

}


