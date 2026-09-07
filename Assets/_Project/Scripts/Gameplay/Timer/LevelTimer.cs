using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Timer
{
    public sealed class LevelTimer : ILevelTimer, IDisposable
    {
        private float _elapsed;
        private int _lastReportedSecond = -1;
        private CancellationTokenSource _cancellationTokenSource;

        public int ElapsedSeconds => Mathf.FloorToInt(_elapsed);
        public bool IsRunning => _cancellationTokenSource != null;

        public event Action<int> Ticked;

        public void Start()
        {
            if (IsRunning)
                return;

            _cancellationTokenSource = new CancellationTokenSource();
            RunAsync(_cancellationTokenSource.Token).Forget();
        }

        public void Stop()
        {
            if (!IsRunning)
                return;

            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = null;
        }

        public void Dispose() =>
            Stop();

        private async UniTaskVoid RunAsync(CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested)
                {
                    await UniTask.Yield(PlayerLoopTiming.Update, token);

                    _elapsed += Time.deltaTime;
                    ReportIfSecondChanged();
                }
            }
            catch (OperationCanceledException)
            {
            }
        }

        private void ReportIfSecondChanged()
        {
            int seconds = ElapsedSeconds;

            if (seconds == _lastReportedSecond)
                return;

            _lastReportedSecond = seconds;
            Ticked?.Invoke(seconds);
        }
    }
}
