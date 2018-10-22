using System;
using AdPieUnityPlugin.Common;
using UnityEngine;

namespace AdPieUnityPlugin.Android
{
    public class VideoAdListener : AndroidJavaProxy, IVideoAdListener
    {
        public event Action OnVideoAdStarted = delegate { };
        public event Action OnVideoAdPaused = delegate { };
        public event Action OnVideoAdStopped = delegate { };
        public event Action OnVideoAdSkipped = delegate { };
        public event Action OnVideoAdError = delegate { };
        public event Action OnVideoAdCompleted = delegate { };

        public VideoAdListener() : base("com.gomfactory.adpie.sdk.videoads.VideoAdPlaybackListener")
        {
        }

        public void onVideoAdStarted()
        {
            if (OnVideoAdStarted != null)
            {
                OnVideoAdStarted();
            }
        }

        public void onVideoAdPaused()
        {
            if (OnVideoAdPaused != null)
            {
                OnVideoAdPaused();
            }

        }

        public void onVideoAdStopped()
        {
            if (OnVideoAdStopped != null)
            {
                OnVideoAdStopped();
            }

        }

        public void onVideoAdSkipped()
        {
            if (OnVideoAdSkipped != null)
            {
                OnVideoAdSkipped();
            }

        }

        public void onVideoAdError()
        {
            if (OnVideoAdError != null)
            {
                OnVideoAdError();
            }

        }

        public void onVideoAdCompleted()
        {
            if (OnVideoAdCompleted != null)
            {
                OnVideoAdCompleted();
            }

        }
    }
}
