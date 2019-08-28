using System;
using AdPieUnityPlugin.Common;
using UnityEngine;

namespace AdPieUnityPlugin.Android
{
    public class InterstitialAdClient : AndroidJavaProxy, IInterstitialAdClient
    {
        public event Action OnAdLoaded = delegate { };
        public event Action<int> OnAdFailedToLoad = delegate { };
        public event Action OnAdClicked = delegate { };
        public event Action OnAdShown = delegate { };
        public event Action OnAdDismissed = delegate { };

        private static AndroidJavaClass pluginClass;
        private AndroidJavaObject interstitialAd;
        private VideoAdPlaybackListener videoAdPlaybackListener;

        public InterstitialAdClient(string slotId) : base("com.gomfactory.adpie.sdk.InterstitialAd$InterstitialAdListener")
        {
            interstitialAd = new AndroidJavaObject("com.gomfactory.adpie.unity.UnityInterstitialAd", slotId, this);
        }

        public void Load()
        {
            if (interstitialAd != null)
            {
                interstitialAd.Call("load");
            }
        }

        public void Destory()
        {
            if (interstitialAd != null)
            {
                interstitialAd.Call("destroy");
                interstitialAd = null;
            }
        }

        public bool IsLoaded()
        {
            if (interstitialAd != null)
            {
                bool isLoaded = interstitialAd.Call<bool>("isLoaded");
                return isLoaded;
            }

            return false;
        }

        public void setVideoAdPlaybackListener(AdPieUnityPlugin.VideoAdPlaybackListener videoAdPlaybackListener)
        {
            if (interstitialAd != null && videoAdPlaybackListener != null)
            {
                VideoAdListener videoAdListener = new VideoAdListener();
                videoAdListener.OnVideoAdStarted += videoAdPlaybackListener.VideoAdStarted;
                videoAdListener.OnVideoAdPaused += videoAdPlaybackListener.VideoAdPaused;
                videoAdListener.OnVideoAdStopped += videoAdPlaybackListener.VideoAdStopped;
                videoAdListener.OnVideoAdSkipped += videoAdPlaybackListener.VideoAdSkipped;
                videoAdListener.OnVideoAdError += videoAdPlaybackListener.VideoAdError;
                videoAdListener.OnVideoAdCompleted += videoAdPlaybackListener.VideoAdCompleted;

                interstitialAd.Call("setVideoAdPlaybackListener", videoAdListener);
            }
        }

        public void Show()
        {
            if (interstitialAd != null)
            {
                interstitialAd.Call("show");
            }
        }

        private void onAdLoaded()
        {
            if (OnAdLoaded != null)
            {
                OnAdLoaded();
            }
        }

        private void onAdFailedToLoad(int errorCode)
        {
            if (OnAdFailedToLoad != null)
            {
                OnAdFailedToLoad(errorCode);
            }
        }

        private void onAdShown()
        {
            if (OnAdShown != null)
            {
                OnAdShown();
            }
        }

        private void onAdClicked()
        {
            if (OnAdClicked != null)
            {
                OnAdClicked();
            }
        }

        private void onAdDismissed()
        {
            if (OnAdDismissed != null)
            {
                OnAdDismissed();
            }
        }
    }
}