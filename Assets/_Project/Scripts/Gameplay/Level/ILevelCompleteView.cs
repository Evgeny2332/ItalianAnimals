using System;

namespace _Project.Scripts.Gameplay.Level
{
    public interface ILevelCompleteView
    {
        event Action RestartRequested;
        event Action MenuRequested;

        void Show(LevelResult result);
    }
}
