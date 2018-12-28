using AdPieUnityPlugin;
using UnityEngine;

public class AdPieSample : MonoBehaviour
{
    private AdView adView;
    private InterstitialAd interstitialAd;

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
        GUI.skin.button.fontSize = (int)(0.04f * Screen.width);

        Rect rect = new Rect();
        rect.x = 20;
        rect.y = 150;

        rect.width = Screen.width * 0.3f;
        rect.height = Screen.height * 0.1f * 2 / 3;

        rect.y = rect.y + rect.height + 15;
        if (GUI.Button(rect, "Load\nBanner Ad"))
        {
            if (adView != null)
            {
                adView.Destory();
                adView = null;
            }

#if UNITY_ANDROID
            string slotId = "57342e0d7174ea39844cac13";
#elif UNITY_IOS || UNITY_IPHONE
            string slotId = "57342fdd7174ea39844cac15";
#else
            string slotId = "";
#endif

            adView = new AdView(slotId, AdView.POSITION_TOP);
            adView.OnAdLoaded += AdView_OnAdLoaded;
            adView.OnAdFailedToLoad += AdView_OnAdFailedToLoad;
            adView.OnAdClicked += AdView_OnAdClicked;
            adView.Load();
        }

        rect.x = rect.x + rect.width + 15;
        if (GUI.Button(rect, "Request\nInterstitial Ad"))
        {
            if (interstitialAd != null)
            {
                interstitialAd.Destory();
                interstitialAd = null;
            }

#if UNITY_ANDROID
            string slotId = "57342e3d7174ea39844cac14";
#elif UNITY_IOS || UNITY_IPHONE
            string slotId = "573430057174ea39844cac16";
#else
        string slotId = "";
#endif

            ////58f99962affeaa4201970fa6 (video)
            interstitialAd = new InterstitialAd(slotId);
            interstitialAd.OnAdLoaded += InterstitialAd_OnAdLoaded;
            interstitialAd.OnAdFailedToLoad += InterstitialAd_OnAdFailedToLoad;
            interstitialAd.OnAdClicked += InterstitialAd_OnAdClicked;
            interstitialAd.OnAdShown += InterstitialAd_OnAdShown;
            interstitialAd.OnAdDismissed += InterstitialAd_OnAdDismissed;

            // optional
            VideoAdPlaybackListener videoAdPlaybackListener = new VideoAdPlaybackListener();
            videoAdPlaybackListener.OnVideoAdStarted += OnVideoAdStarted;
            videoAdPlaybackListener.OnVideoAdPaused += OnVideoAdPaused;
            videoAdPlaybackListener.OnVideoAdStopped += OnVideoAdStopped;
            videoAdPlaybackListener.OnVideoAdSkipped += OnVideoAdSkipped;
            videoAdPlaybackListener.OnVideoAdError += OnVideoAdError;
            videoAdPlaybackListener.OnVideoAdCompleted += OnVideoAdCompleted;
            interstitialAd.setVideoAdPlaybackListener(videoAdPlaybackListener);

            interstitialAd.Load();
        }

        rect.x = rect.x + rect.width + 15;
        if (GUI.Button(rect, "Show\nInterstitial Ad"))
        {
            if (interstitialAd != null && interstitialAd.IsLoaded())
            {
                interstitialAd.Show();
            }
        }
#endif
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

    void OnVideoAdStarted()
    {
        Debug.Log("::AdPieSample::OnVideoAdStarted");
    }

    void OnVideoAdPaused()
    {
        Debug.Log("::AdPieSample::OnVideoAdPaused");
    }

    void OnVideoAdStopped()
    {
        Debug.Log("::AdPieSample::OnVideoAdStopped");
    }

    void OnVideoAdSkipped()
    {
        Debug.Log("::AdPieSample::OnVideoAdSkipped");
    }

    void OnVideoAdError()
    {
        Debug.Log("::AdPieSample::OnVideoAdError");
    }

    void OnVideoAdCompleted()
    {
        Debug.Log("::AdPieSample::OnVideoAdCompleted");
    }

    public void loadBanner()
    {
        // put your logic to open the scene here
    }
}