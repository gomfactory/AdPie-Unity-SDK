namespace AdPieUnityPlugin
{
    public class AdPieSDK
    {
        private static AdPieSDK instance = new AdPieSDK();

        private AdPieSDK() { }

        public static AdPieSDK Instance
        {
            get
            {
                return instance;
            }
        }

        public void DebugEnabled(bool isDebugEnabled)
        {
#if UNITY_EDITOR

#elif UNITY_ANDROID
            AdPieUnityPlugin.Android.AdPieSdkClient.Instance.DebugEnabled(true);
#elif UNITY_IOS || UNITY_IPHONE
            AdPieUnityPlugin.iOS.AdPieSdkClient.Instance.DebugEnabled(true);
#endif
        }

        public void Initialize(string mid)
        {

#if UNITY_EDITOR

#elif UNITY_ANDROID
            AdPieUnityPlugin.Android.AdPieSdkClient.Instance.Initialize(mid);
#elif UNITY_IOS || UNITY_IPHONE
            AdPieUnityPlugin.iOS.AdPieSdkClient.Instance.Initialize(mid);
#endif

        }
    }
}
