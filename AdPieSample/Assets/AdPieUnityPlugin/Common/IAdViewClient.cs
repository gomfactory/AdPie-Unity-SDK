using System;
namespace AdPieUnityPlugin.Common
{
    public interface IAdViewClient
    {
        event Action OnAdLoaded;
        event Action<int> OnAdFailedToLoad;
        event Action OnAdClicked;

        void Load();
        void Destory();
    }
}
