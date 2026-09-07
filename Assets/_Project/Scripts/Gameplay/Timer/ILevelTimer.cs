using System;

namespace _Project.Scripts.Gameplay.Timer
{
    public interface ILevelTimer
    {
        int ElapsedSeconds { get; }
        event Action<int> Ticked;

        void Start();
        void Stop();
    }
}
