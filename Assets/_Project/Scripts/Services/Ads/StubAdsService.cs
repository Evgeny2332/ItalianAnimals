using System;
using UnityEngine;

namespace _Project.Scripts.Services.Ads
{
    public sealed class StubAdsService : IAdsService
    {
        public void ShowInterstitial(Action onClosed = null)
        {
            Debug.Log("[Ads] Interstitial (stub)");
            onClosed?.Invoke();
        }
    }
}
