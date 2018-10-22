using System;
using AdPieUnityPlugin.Common;
using UnityEngine;

namespace AdPieUnityPlugin.iOS
{
    public class InterstitialAdClient : IInterstitialAdClient
    {
        public event Action OnAdLoaded = delegate { };
        public event Action<int> OnAdFailedToLoad = delegate { };
        public event Action OnAdClicked = delegate { };
        public event Action OnAdShown = delegate { };
        public event Action OnAdDismissed = delegate { };

        public InterstitialAdClient(string slotId)
        {
            FrameworkWrapper.adpie_sdk_createInterstitial(slotId);

            var gameObject = GameObject.Find("InterstitialEventHandler");
            if (gameObject != null) return;

            gameObject = new GameObject("InterstitialEventHandler");

            if (UnityEngine.Application.isPlaying)
            {
                UnityEngine.Object.DontDestroyOnLoad(gameObject);
            }

            var eventHandler = gameObject.AddComponent<InterstitialEventHandler>();

            eventHandler.OnAdLoaded += onAdLoaded;
            eventHandler.OnAdFailedToLoad += onAdFailedToLoad;
            eventHandler.OnAdShown += onAdShown;
            eventHandler.OnAdClicked += onAdClicked;
            eventHandler.OnAdDismissed += onAdDismissed;
        }

        public void Load()
        {
            FrameworkWrapper.adpie_sdk_loadInterstitial();
        }

        public void Destory()
        {
        }

        public bool IsLoaded()
        {
            return FrameworkWrapper.adpie_sdk_isLoadedInterstitial();
        }

        public void setVideoAdPlaybackListener(AdPieUnityPlugin.VideoAdPlaybackListener videoAdPlaybackListener)
        {
        }

        public void Show()
        {
            FrameworkWrapper.adpie_sdk_showInterstitial();
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

    public class InterstitialEventHandler : MonoBehaviour
    {
        public event Action OnAdLoaded = delegate { };
        public event Action<int> OnAdFailedToLoad = delegate { };
        public event Action OnAdClicked = delegate { };
        public event Action OnAdShown = delegate { };
        public event Action OnAdDismissed = delegate { };

        private void AdLoaded(string message)
        {
            if (OnAdLoaded != null)
            {
                OnAdLoaded();
            }
        }

        private void AdFailedToLoad(string message)
        {
            if (OnAdFailedToLoad != null)
            {
                OnAdFailedToLoad(int.Parse(message));
            }
        }

        private void AdShown(string message)
        {
            if (OnAdShown != null)
            {
                OnAdShown();
            }
        }

        private void AdClicked(string message)
        {
            if (OnAdClicked != null)
            {
                OnAdClicked();
            }
        }

        private void AdDismissed(string message)
        {
            if (OnAdDismissed != null)
            {
                OnAdDismissed();
            }
        }
    }
}
