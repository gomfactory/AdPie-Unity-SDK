using System.Runtime.InteropServices;

namespace AdPieUnityPlugin.iOS
{
    internal class FrameworkWrapper
    {
        [DllImport("__Internal")]
        internal static extern void adpie_sdk_debug_enabled();

        [DllImport("__Internal")]
        internal static extern void adpie_sdk_init(string mid);

        [DllImport("__Internal")]
        internal static extern void adpie_sdk_createBanner(string slotId, int position);
        [DllImport("__Internal")]
        internal static extern void adpie_sdk_createBannerWithSize(string slotId, int position, int width, int height);
        [DllImport("__Internal")]
        internal static extern void adpie_sdk_loadBanner();
        [DllImport("__Internal")]
        internal static extern void adpie_sdk_destroyBanner();
        [DllImport("__Internal")]
        internal static extern void adpie_sdk_updateBanner();

        [DllImport("__Internal")]
        internal static extern void adpie_sdk_createInterstitial(string sid);
        [DllImport("__Internal")]
        internal static extern void adpie_sdk_loadInterstitial();
        [DllImport("__Internal")]
        internal static extern bool adpie_sdk_isLoadedInterstitial();
        [DllImport("__Internal")]
        internal static extern void adpie_sdk_showInterstitial();
    }
}
