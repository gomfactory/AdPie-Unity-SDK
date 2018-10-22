using System;
using AdPieUnityPlugin.Common;
using UnityEngine;

namespace AdPieUnityPlugin.Android
{
    public class AdViewClient : AndroidJavaProxy, IAdViewClient
    {
        public event Action OnAdLoaded = delegate { };
        public event Action<int> OnAdFailedToLoad = delegate { };
        public event Action OnAdClicked = delegate { };

        private static AndroidJavaClass pluginClass;
        private AndroidJavaObject adView;

        public AdViewClient(string slotId, int position) : base("com.gomfactory.adpie.sdk.AdView$AdListener")
        {
            adView = new AndroidJavaObject("com.gomfactory.adpie.unity.UnityAdView", slotId, position, this);
        }

        public AdViewClient(string slotId, int position, int width, int height) : base("com.gomfactory.adpie.sdk.AdView$AdListener")
        {
            adView = new AndroidJavaObject("com.gomfactory.adpie.unity.UnityAdView", slotId, position, width, height, this);
        }

        public void Load()
        {
            if (adView != null)
            {
                adView.Call("load");
            }
        }

        public void Destory()
        {
            if (adView != null)
            {
                adView.Call("destroy");
                adView = null;
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

        private void onAdClicked()
        {
            if (OnAdClicked != null)
            {
                OnAdClicked();
            }

        }
    }
}