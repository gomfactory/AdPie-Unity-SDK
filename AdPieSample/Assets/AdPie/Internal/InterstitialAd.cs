using System;
using AdPieUnityPlugin.Common;
using UnityEngine;

namespace AdPieUnityPlugin
{
    public class InterstitialAd
    {

        public event Action OnAdLoaded = delegate { };
        public event Action<int> OnAdFailedToLoad = delegate { };
        public event Action OnAdClicked = delegate { };
        public event Action OnAdShown = delegate { };
        public event Action OnAdDismissed = delegate { };

        private IInterstitialAdClient client;

        public InterstitialAd()
        {
        }

        public InterstitialAd(string slotId)
        {
#if UNITY_EDITOR
            client = null;
#elif UNITY_ANDROID
            client = new AdPieUnityPlugin.Android.InterstitialAdClient(slotId);
#elif UNITY_IOS || UNITY_IPHONE
            client = new AdPieUnityPlugin.iOS.InterstitialAdClient(slotId);
#else
            client = null;
#endif
            configureEvent();
        }

        public void Load()
        {
            if (client != null)
            {
                client.Load();
            }
        }

        public void Destory()
        {
            if (client != null)
            {
                client.Destory();
                client = null;
            }
        }

        public bool IsLoaded()
        {
            if (client != null)
            {
                return client.IsLoaded();
            }
            return false;
        }

        public void setVideoAdPlaybackListener(AdPieUnityPlugin.VideoAdPlaybackListener videoAdPlaybackListener)
        {
            if (client != null)
            {
                client.setVideoAdPlaybackListener(videoAdPlaybackListener);
            }
        }

        public void Show()
        {
            if (client != null)
            {
                client.Show();
            }
        }

        private void configureEvent()
        {
            if (client != null)
            {
                client.OnAdLoaded += onAdLoaded;
            }

            if (client != null)
            {
                client.OnAdFailedToLoad += onAdFailedToLoad;
            }

            if (client != null)
            {
                client.OnAdShown += onAdShown;
            }

            if (client != null)
            {
                client.OnAdClicked += onAdClicked;
            }

            if (client != null)
            {
                client.OnAdDismissed += onAdDismissed;
            }
        }

        private void onAdLoaded()
        {
            OnAdLoaded();
        }

        private void onAdFailedToLoad(int errorCode)
        {
            OnAdFailedToLoad(errorCode);
        }

        private void onAdShown()
        {
            OnAdShown();
        }

        private void onAdClicked()
        {
            OnAdClicked();
        }

        private void onAdDismissed()
        {
            OnAdDismissed();
        }
    }
}
