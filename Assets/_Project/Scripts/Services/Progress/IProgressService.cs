using System;
using _Project.Scripts.Core;

namespace _Project.Scripts.Services.Progress
{
    public interface IProgressService
    {
        event Action Changed;

        int GetStars(TypeLevel typeLevel, int levelId);
        bool IsCompleted(TypeLevel typeLevel, int levelId);
        bool IsUnlocked(TypeLevel typeLevel, int levelId);
        int GetCollectedStars(TypeLevel typeLevel);
        int GetCompletedLevelsCount(TypeLevel typeLevel);

        void RegisterResult(TypeLevel typeLevel, int levelId, int stars);

        void ResetProgress();
    }
}
