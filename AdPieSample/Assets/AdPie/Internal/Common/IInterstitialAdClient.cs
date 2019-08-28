using System;
namespace AdPieUnityPlugin.Common
{
    public interface IInterstitialAdClient
    {
        event Action OnAdLoaded;
        event Action<int> OnAdFailedToLoad;
        event Action OnAdClicked;
        event Action OnAdShown;
        event Action OnAdDismissed;

        void Load();
        void Destory();
        bool IsLoaded();
        void setVideoAdPlaybackListener(AdPieUnityPlugin.VideoAdPlaybackListener videoAdPlaybackListener);
        void Show();
    }
}
