using System.Collections.Generic;
using _Project.Scripts.Core;

namespace _Project.Scripts.Services.Progress
{
    public sealed class ProgressData
    {
        public const int CurrentVersion = 1;

        public int Version { get; set; } = CurrentVersion;
        public Dictionary<TypeLevel, Dictionary<int, int>> StarsByLevel { get; set; } = new();

        public int GetStars(TypeLevel typeLevel, int levelId) =>
            StarsByLevel.TryGetValue(typeLevel, out var levels) && levels.TryGetValue(levelId, out int stars)
                ? stars
                : 0;

        public void SetStars(TypeLevel typeLevel, int levelId, int stars)
        {
            if (!StarsByLevel.TryGetValue(typeLevel, out var levels))
            {
                levels = new Dictionary<int, int>();
                StarsByLevel[typeLevel] = levels;
            }

            levels[levelId] = stars;
        }
    }
}
