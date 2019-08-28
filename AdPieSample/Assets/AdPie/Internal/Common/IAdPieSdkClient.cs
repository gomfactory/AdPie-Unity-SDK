namespace AdPieUnityPlugin.Common
{
    public interface IAdPieSdkClient
    {
        void DebugEnabled(bool isDebugEnabled);
        void Initialize(string mid);
    }
}
