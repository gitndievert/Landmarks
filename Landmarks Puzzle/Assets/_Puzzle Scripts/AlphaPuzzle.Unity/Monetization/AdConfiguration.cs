using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public static class AdConfiguration
{
    public class AdMob
    {
        public const string AndroidAdMobId = "ca-app-pub-2108930618992299~2508656537";
        public const string IOSAdMobId = "ca-app-pub-2108930618992299~1349394202";
        public const string TestDeviceId = "990004838402349";
    }

    public class UnityAds
    {
        public const string ProjectID = "5c9e966c-440e-4fb8-b447-8c643a724ffe";
        public const string AppleAppStoreGameID = "2767634";
        public const string GooglePlayStoreGameID = "2767635";
    }    
    
}
