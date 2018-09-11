using UnityEngine;
using UnityEngine.Advertisements;
using SBK.Unity;

public class UnityAdServices : PSingle<UnityAdServices>
{
    public bool StartBannerAds = false;
    public bool TestAds = false;
    public string PlacementID = "video";

    protected override void PAwake()
    {
        
    }

    protected override void PDestroy()
    {
        
    }   

    // Use this for initialization
    void Start ()
    {
        if (!StartBannerAds || !Advertisement.isSupported) return;
#if UNITY_ANDROID
            Advertisement.Initialize(AdConfiguration.UnityAds.GooglePlayStoreGameID,TestAds);
#elif UNITY_IPHONE
        Advertisement.Initialize(AdConfiguration.UnityAds.AppleAppStoreGameID,TestAds);
#endif
        
	}
   
    public void ShowAd()
    {
        if (!StartBannerAds || !Advertisement.isSupported) return;
        Advertisement.IsReady(PlacementID);
        var options = new ShowOptions
        {
            resultCallback = HandleShowResult           
        };
        Advertisement.Show(PlacementID, options);
    }
     

    public void CloseAd()
    {
      
    }

    void HandleShowResult(ShowResult result)
    {
        switch(result)
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

}
