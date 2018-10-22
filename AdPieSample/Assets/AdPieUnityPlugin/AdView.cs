using System;
using AdPieUnityPlugin.Common;

namespace AdPieUnityPlugin
{
    public class AdView
    {
        public static int POSITION_TOP = 0;
        public static int POSITION_BOTTOM = 1;
        public static int POSITION_TOP_LEFT = 2;
        public static int POSITION_TOP_RIGHT = 3;
        public static int POSITION_BOTTOM_LEFT = 4;
        public static int POSITION_BOTTOM_RIGHT = 5;
        public static int POSITION_CENTER = 6;

        public event Action OnAdLoaded = delegate { };
        public event Action<int> OnAdFailedToLoad = delegate { };
        public event Action OnAdClicked = delegate { };

        private IAdViewClient client;

        public AdView()
        {
        }

        public AdView(string slotId, int position)
        {

#if UNITY_EDITOR
            client = null;
#elif UNITY_ANDROID
            client = new AdPieUnityPlugin.Android.AdViewClient(slotId, position);
#elif UNITY_IOS || UNITY_IPHONE
            client = new AdPieUnityPlugin.iOS.AdViewClient(slotId, position);
#else
            client = null;
#endif
            configureEvent();

        }

        public AdView(string slotId, int position, int width, int height)
        {

#if UNITY_EDITOR
            client = null;
#elif UNITY_ANDROID
            client = new AdPieUnityPlugin.Android.AdViewClient(slotId, position, width, height);
#elif UNITY_IOS || UNITY_IPHONE
            client = new AdPieUnityPlugin.iOS.AdViewClient(slotId, position, width, height);
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
                client.OnAdClicked += onAdClicked;
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

        private void onAdClicked()
        {
            OnAdClicked();
        }
    }
}
