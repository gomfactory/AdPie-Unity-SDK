using System;
using AdPieUnityPlugin.Common;

namespace AdPieUnityPlugin
{
    public class VideoAdPlaybackListener : IVideoAdListener
    {

        public event Action OnVideoAdStarted = delegate { };
        public event Action OnVideoAdPaused = delegate { };
        public event Action OnVideoAdStopped = delegate { };
        public event Action OnVideoAdSkipped = delegate { };
        public event Action OnVideoAdError = delegate { };
        public event Action OnVideoAdCompleted = delegate { };

        public VideoAdPlaybackListener()
        {
        }

        public void VideoAdStarted()
        {
            if (OnVideoAdStarted != null)
            {
                OnVideoAdStarted();
            }
        }

        public void VideoAdPaused()
        {
            if (OnVideoAdPaused != null)
            {
                OnVideoAdPaused();
            }

        }

        public void VideoAdStopped()
        {
            if (OnVideoAdStopped != null)
            {
                OnVideoAdStopped();
            }

        }

        public void VideoAdSkipped()
        {
            if (OnVideoAdSkipped != null)
            {
                OnVideoAdSkipped();
            }

        }

        public void VideoAdError()
        {
            if (OnVideoAdError != null)
            {
                OnVideoAdError();
            }

        }

        public void VideoAdCompleted()
        {
            if (OnVideoAdCompleted != null)
            {
                OnVideoAdCompleted();
            }

        }
    }
}
