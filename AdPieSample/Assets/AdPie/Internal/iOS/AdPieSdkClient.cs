using AdPieUnityPlugin.Common;

namespace AdPieUnityPlugin.iOS
{
    public class AdPieSdkClient : IAdPieSdkClient
    {
        private static AdPieSdkClient instance = new AdPieSdkClient();

        private AdPieSdkClient()
        {
        }

        public static AdPieSdkClient Instance
        {
            get
            {
                return instance;
            }
        }

        public void DebugEnabled(bool isDebugEnabled)
        {
            if (isDebugEnabled)
            {
                FrameworkWrapper.adpie_sdk_debug_enabled();
            }
        }

        public void Initialize(string mid)
        {
            FrameworkWrapper.adpie_sdk_init(mid);
        }
    }
}