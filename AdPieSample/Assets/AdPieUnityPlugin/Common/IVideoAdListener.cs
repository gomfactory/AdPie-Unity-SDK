using System;
namespace AdPieUnityPlugin.Common
{
    public interface IVideoAdListener
    {
        event Action OnVideoAdStarted;
        event Action OnVideoAdPaused;
        event Action OnVideoAdStopped;
        event Action OnVideoAdSkipped;
        event Action OnVideoAdError;
        event Action OnVideoAdCompleted;
    }
}
