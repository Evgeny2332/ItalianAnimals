using System;
using _Project.Scripts.Core;
using _Project.Scripts.Services.Config;
using _Project.Scripts.Services.Save;

namespace _Project.Scripts.Services.Progress
{
    public sealed class ProgressService : IProgressService
    {
        private const string SaveKey = "progress";
        private const int FirstLevelId = 1;

        private readonly ISaveLoadService _saveLoadService;
        private readonly IConfigProvider _configProvider;

        private ProgressData _data;

        public event Action Changed;

        public ProgressService(ISaveLoadService saveLoadService, IConfigProvider configProvider)
        {
            _saveLoadService = saveLoadService;
            _configProvider = configProvider;

            Load();
        }

        public int GetStars(TypeLevel typeLevel, int levelId) =>
            _data.GetStars(typeLevel, levelId);

        public bool IsCompleted(TypeLevel typeLevel, int levelId) =>
            GetStars(typeLevel, levelId) > 0;

        public bool IsUnlocked(TypeLevel typeLevel, int levelId) =>
            levelId <= FirstLevelId || IsCompleted(typeLevel, levelId - 1);

        public int GetCollectedStars(TypeLevel typeLevel)
        {
            int total = 0;
            int levelsCount = _configProvider.GetLevelsCount(typeLevel);

            for (int levelId = FirstLevelId; levelId <= levelsCount; levelId++)
                total += GetStars(typeLevel, levelId);

            return total;
        }

        public int GetCompletedLevelsCount(TypeLevel typeLevel)
        {
            int count = 0;
            int levelsCount = _configProvider.GetLevelsCount(typeLevel);

            for (int levelId = FirstLevelId; levelId <= levelsCount; levelId++)
            {
                if (IsCompleted(typeLevel, levelId))
                    count++;
            }

            return count;
        }

        public void RegisterResult(TypeLevel typeLevel, int levelId, int stars)
        {
            if (stars <= GetStars(typeLevel, levelId))
                return;

            _data.SetStars(typeLevel, levelId, stars);
            Save();
        }

        public void ResetProgress()
        {
            _data = new ProgressData();
            _saveLoadService.Delete(SaveKey);
            Changed?.Invoke();
        }

        private void Load()
        {
            if (!_saveLoadService.TryLoad(SaveKey, out ProgressData data) || data.Version != ProgressData.CurrentVersion)
                data = new ProgressData();

            _data = data;
            _data.StarsByLevel ??= new();
        }

        private void Save()
        {
            _saveLoadService.Save(SaveKey, _data);
            Changed?.Invoke();
        }
    }
}
