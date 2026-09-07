using System;

namespace _Project.Scripts.Services.Ads
{
    public interface IAdsService
    {
        void ShowInterstitial(Action onClosed = null);
    }
}
