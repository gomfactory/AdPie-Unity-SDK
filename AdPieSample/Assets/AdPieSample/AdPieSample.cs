using AdPieUnityPlugin;
using UnityEngine;

public class AdPieSample : MonoBehaviour
{
    private AdView adView;
    private InterstitialAd interstitialAd;
    private RewardedVideoAd rewardedVideoAd;

    void Start()
    {

#if UNITY_ANDROID
        string mediaId = "57342d1b7174ea39844cac10";
#elif UNITY_IOS || UNITY_IPHONE
        string mediaId = "57342d787174ea39844cac11";
#else
        string mediaId = "";
#endif
        // 디버깅 적용
        AdPieSDK.Instance.DebugEnabled(true);
        // SDK 초기화
        AdPieSDK.Instance.Initialize(mediaId);
    }

    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }

    void OnGUI()
    {
        #if UNITY_ANDROID || UNITY_IOS || UNITY_IPHONE
    
        var fontSize = (int)(0.035f * Screen.width);
        
        GUI.skin.box.fontSize = fontSize;
        GUI.skin.button.fontSize = fontSize;
        
        var buttonWidth = 0.35f * Screen.width;
        var buttonHeight = 0.15f * Screen.height;
        var buttonRowCount = 3;
        
        var groupWidth = buttonWidth * 2 + 30;
        var groupHeight = fontSize + (buttonHeight * buttonRowCount) + (buttonRowCount * 10) + 10;

        var screenWidth = Screen.width;
        var screenHeight = Screen.height;

        var groupX = ( screenWidth - groupWidth ) / 2;
        var groupY = ( screenHeight - groupHeight ) / 2;

        GUI.BeginGroup(new Rect( groupX, groupY, groupWidth, groupHeight ) );
        GUI.Box(new Rect( 0, 0, groupWidth, groupHeight ), "Select AdPie AD function" );

        if ( GUI.Button(new Rect( 10, fontSize + 10, buttonWidth, buttonHeight ), "Load Banner" ) )
        {
            LoadBanner();
        }
        if ( GUI.Button(new Rect( 10, fontSize + 20 + buttonHeight, buttonWidth, buttonHeight ), "Load Interstitial" ) )
        {
            LoadInterstitial();
        }
        if ( GUI.Button(new Rect( 10, fontSize + 30 + buttonHeight * 2, buttonWidth, buttonHeight ), "Load RV" ) )
        {
            LoadRV();
        }
        if ( GUI.Button(new Rect( 20 + buttonWidth, fontSize + 10, buttonWidth, buttonHeight ), "Destroy Banner" ) )
        {
            DestroyBanner();
        }
        if ( GUI.Button(new Rect( 20 + buttonWidth, fontSize + 20 + buttonHeight, buttonWidth, buttonHeight ), "Show Interstitial" ) )
        {
            ShowInterstitial();
        }
        if ( GUI.Button(new Rect( 20 + buttonWidth, fontSize + 30 + buttonHeight * 2, buttonWidth, buttonHeight ), "Show RV" ) )
        {
            ShowRV();
        }

        GUI.EndGroup();
        
        #endif
    }
    
    void LoadBanner() {

#if UNITY_ANDROID
        string slotId = "57342e0d7174ea39844cac13";
#elif UNITY_IOS || UNITY_IPHONE
        string slotId = "57342fdd7174ea39844cac15";
#else
        return;
#endif
        if (adView != null)
        {
            adView.Destory();
            adView = null;
        }

        adView = new AdView(slotId, AdView.POSITION_TOP);
        adView.OnAdLoaded += AdView_OnAdLoaded;
        adView.OnAdFailedToLoad += AdView_OnAdFailedToLoad;
        adView.OnAdClicked += AdView_OnAdClicked;
        adView.Load();
    }
    
    void DestroyBanner() {
        if (adView != null)
        {
            adView.Destory();
            adView = null;
        }
    }
    
    void LoadInterstitial() {
    
#if UNITY_ANDROID
        string slotId = "57342e3d7174ea39844cac14";
#elif UNITY_IOS || UNITY_IPHONE
        string slotId = "573430057174ea39844cac16";
#else
        return;
#endif
        if (interstitialAd != null)
        {
            interstitialAd.Destory();
            interstitialAd = null;
        }

        interstitialAd = new InterstitialAd(slotId);
        interstitialAd.OnAdLoaded += InterstitialAd_OnAdLoaded;
        interstitialAd.OnAdFailedToLoad += InterstitialAd_OnAdFailedToLoad;
        interstitialAd.OnAdClicked += InterstitialAd_OnAdClicked;
        interstitialAd.OnAdShown += InterstitialAd_OnAdShown;
        interstitialAd.OnAdDismissed += InterstitialAd_OnAdDismissed;

        interstitialAd.Load();
    }
    
    void ShowInterstitial() {
        if (interstitialAd != null && interstitialAd.IsLoaded())
        {
            interstitialAd.Show();
        }
    }
    
    void LoadRV() {

#if UNITY_ANDROID
        string slotId = "58f99962affeaa4201970fa6";
#elif UNITY_IOS || UNITY_IPHONE
        string slotId = ""; // 현재 미지원
        return;
#else
        return;
#endif

        if (rewardedVideoAd != null)
        {
            rewardedVideoAd.Destory();
            rewardedVideoAd = null;
        }

        rewardedVideoAd = new RewardedVideoAd(slotId);
        rewardedVideoAd.OnRewardedVideoLoaded += RewardedVideoAd_OnRewardedVideoLoaded;
        rewardedVideoAd.OnRewardedVideoFailedToLoad += RewardedVideoAd_OnRewardedVideoFailedToLoad;
        rewardedVideoAd.OnRewardedVideoClicked += RewardedVideoAd_OnRewardedVideoClicked;
        rewardedVideoAd.OnRewardedVideoStarted += RewardedVideoAd_OnRewardedVideoStarted;
        rewardedVideoAd.OnRewardedVideoFinished += RewardedVideoAd_OnRewardedVideoFinished;

        rewardedVideoAd.Load();
    }
    
    void ShowRV() {
    if (rewardedVideoAd != null && rewardedVideoAd.IsLoaded())
        {
            rewardedVideoAd.Show();
        }
    }

    void AdView_OnAdLoaded()
    {
        Debug.Log("::AdPieSample::AdView_OnAdLoaded");
    }

    void AdView_OnAdFailedToLoad(int errorCode)
    {
        Debug.Log("::AdPieSample::AdView_OnAdFailedToLoad : " + errorCode);
    }

    void AdView_OnAdClicked()
    {
        Debug.Log("::AdPieSample::AdView_OnAdClicked");
    }

    void InterstitialAd_OnAdLoaded()
    {
        Debug.Log("::AdPieSample::InterstitialAd_OnAdLoaded");
    }

    void InterstitialAd_OnAdFailedToLoad(int errorCode)
    {
        Debug.Log("::AdPieSample::InterstitialAd_OnAdFailedToLoad : " + errorCode);
    }

    void InterstitialAd_OnAdClicked()
    {
        Debug.Log("::AdPieSample::InterstitialAd_OnAdClicked");
    }

    void InterstitialAd_OnAdShown()
    {
        Debug.Log("::AdPieSample::InterstitialAd_OnAdShown");
    }

    void InterstitialAd_OnAdDismissed()
    {
        Debug.Log("::AdPieSample::InterstitialAd_OnAdDismissed");
    }
    
    void RewardedVideoAd_OnRewardedVideoLoaded()
    {
        Debug.Log("::AdPieSample::RewardedVideoAd_OnRewardedVideoLoaded");
    }

    void RewardedVideoAd_OnRewardedVideoFailedToLoad(int errorCode)
    {
        Debug.Log("::AdPieSample::RewardedVideoAd_OnRewardedVideoFailedToLoad : " + errorCode);
    }

    void RewardedVideoAd_OnRewardedVideoClicked()
    {
        Debug.Log("::AdPieSample::RewardedVideoAd_OnRewardedVideoClicked");
    }

    void RewardedVideoAd_OnRewardedVideoStarted()
    {
        Debug.Log("::AdPieSample::RewardedVideoAd_OnRewardedVideoStarted");
    }

    void RewardedVideoAd_OnRewardedVideoFinished(int finishState)
    {
        string state = "";
        switch(finishState) {
            case 0:
                state = "UNKNOWN";
                break;
            case 1:
                state = "COMPLETED";
                break;
            case 2:
                state = "ERROR";
                break;
            case 3:
                state = "SKIPPED";
                break;
        }
        Debug.Log("::AdPieSample::RewardedVideoAd_OnRewardedVideoFinished : " + state);
    }

    public void loadBanner()
    {
        // put your logic to open the scene here
    }
}