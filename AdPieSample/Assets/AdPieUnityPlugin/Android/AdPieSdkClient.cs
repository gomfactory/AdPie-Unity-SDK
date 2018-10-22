using AdPieUnityPlugin.Common;
using UnityEngine;

namespace AdPieUnityPlugin.Android
{
    public class AdPieSdkClient : IAdPieSdkClient
    {
        private static AdPieSdkClient instance = new AdPieSdkClient();
        private static AndroidJavaObject adpieSDK;

        private AdPieSdkClient()
        {
            adpieSDK = GetAdPieSDK();
        }

        public static AdPieSdkClient Instance
        {
            get
            {
                return instance;
            }
        }

        public void Initialize(string mid)
        {
            if (adpieSDK != null)
            {
                adpieSDK.Call("initialize", mid);
            }
        }

        public void DebugEnabled(bool isDebugEnabled)
        {
            if (adpieSDK != null)
            {
                adpieSDK.Call("setIsDebugEnabled", isDebugEnabled);
            }
        }

        public static AndroidJavaObject GetAdPieSDK()
        {
            using (AndroidJavaClass pluginClass = new AndroidJavaClass("com.gomfactory.adpie.unity.UnityAdPieSDK"))
            {
                adpieSDK = pluginClass.CallStatic<AndroidJavaObject>("getInstance");
            }
            return adpieSDK;
        }
    }
}