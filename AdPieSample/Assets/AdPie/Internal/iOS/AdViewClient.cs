using System;
using AdPieUnityPlugin.Common;
using UnityEngine;

namespace AdPieUnityPlugin.iOS
{
    public class AdViewClient : IAdViewClient
    {
        public event Action OnAdLoaded = delegate { };
        public event Action<int> OnAdFailedToLoad = delegate { };
        public event Action OnAdClicked = delegate { };

        private int position;

        public AdViewClient(string slotId, int position)
        {
            FrameworkWrapper.adpie_sdk_createBanner(slotId, position);

            initialize();
        }

        public AdViewClient(string slotId, int position, int width, int height)
        {
            FrameworkWrapper.adpie_sdk_createBannerWithSize(slotId, position, width, height);

            initialize();
        }

        private void initialize()
        {
            var gameObject = GameObject.Find("AdViewEventHandler");
            if (gameObject != null) return;

            gameObject = new GameObject("AdViewEventHandler");

            if (UnityEngine.Application.isPlaying)
            {
                UnityEngine.Object.DontDestroyOnLoad(gameObject);
            }

            var eventHandler = gameObject.AddComponent<AdViewEventHandler>();

            eventHandler.OnAdLoaded += onAdLoaded;
            eventHandler.OnAdFailedToLoad += onAdFailedToLoad;
            eventHandler.OnAdClicked += onAdClicked;
            eventHandler.OnRotation += onRotation;
        }

        public void Destory()
        {
            FrameworkWrapper.adpie_sdk_destroyBanner();
        }

        public void Load()
        {
            FrameworkWrapper.adpie_sdk_loadBanner();
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

        private void onAdClicked()
        {
            if (OnAdClicked != null)
            {
                OnAdClicked();
            }

        }

        private void onRotation()
        {
            FrameworkWrapper.adpie_sdk_updateBanner();
        }
    }

    public class AdViewEventHandler : MonoBehaviour
    {
        private ScreenOrientation currentOrientation;

        public event Action OnAdLoaded = delegate { };
        public event Action<int> OnAdFailedToLoad = delegate { };
        public event Action OnAdClicked = delegate { };
        public event Action OnRotation = delegate { };

        void Start()
        {
            currentOrientation = Screen.orientation;
        }

        void Update()
        {
            if (currentOrientation != Screen.orientation)
            {
                currentOrientation = Screen.orientation;
                if (OnRotation != null)
                {
                    OnRotation();
                }
            }

        }

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

        private void AdClicked(string message)
        {
            if (OnAdClicked != null)
            {
                OnAdClicked();
            }
        }
    }
}
