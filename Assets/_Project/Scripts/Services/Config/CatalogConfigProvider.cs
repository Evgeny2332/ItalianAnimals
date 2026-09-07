using System.Collections.Generic;
using _Project.Scripts.Core;
using UnityEngine;

namespace _Project.Scripts.Services.Config
{
    public sealed class CatalogConfigProvider : IConfigProvider
    {
        private const int MenuSceneBuildIndex = 0;

        private static readonly LevelConfig[] Empty = new LevelConfig[0];

        private readonly Dictionary<TypeLevel, LevelCatalog.LevelGroup> _groups = new();

        public CatalogConfigProvider(LevelCatalog catalog)
        {
            if (catalog == null)
            {
                Debug.LogError($"{nameof(LevelCatalog)} не назначен в RootLifetimeScope.");
                return;
            }

            foreach (var group in catalog.Groups)
                _groups[group.TypeLevel] = group;
        }

        public IReadOnlyList<LevelConfig> GetLevels(TypeLevel typeLevel) =>
            _groups.TryGetValue(typeLevel, out var group) ? group.Levels : Empty;

        public LevelConfig GetLevel(TypeLevel typeLevel, int levelId)
        {
            var levels = GetLevels(typeLevel);

            if (levelId < 1 || levelId > levels.Count)
            {
                Debug.LogError($"Уровень {typeLevel}/{levelId} отсутствует в каталоге.");
                return null;
            }

            return levels[levelId - 1];
        }

        public int GetLevelsCount(TypeLevel typeLevel) =>
            GetLevels(typeLevel).Count;

        public int GetMaxStars(TypeLevel typeLevel)
        {
            int maxStars = 0;

            foreach (var level in GetLevels(typeLevel))
                maxStars += level.MaxStars;

            return maxStars;
        }

        public int GetSceneBuildIndex(TypeLevel typeLevel)
        {
            if (_groups.TryGetValue(typeLevel, out var group))
                return group.SceneBuildIndex;

            Debug.LogError($"Для сложности {typeLevel} нет группы в каталоге — сцена уровня неизвестна.");
            return MenuSceneBuildIndex;
        }
    }
}
