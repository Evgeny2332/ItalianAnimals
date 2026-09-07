using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Core
{
    [CreateAssetMenu(fileName = "LevelCatalog", menuName = "PuzzleGame/Level Catalog", order = 1)]
    public class LevelCatalog : ScriptableObject
    {
        [SerializeField] private LevelGroup[] _groups;

        public IReadOnlyList<LevelGroup> Groups => _groups;

        [Serializable]
        public class LevelGroup
        {
            [SerializeField] private TypeLevel _typeLevel;
            [SerializeField] private int _sceneBuildIndex;
            [SerializeField] private LevelConfig[] _levels;

            public TypeLevel TypeLevel => _typeLevel;
            public int SceneBuildIndex => _sceneBuildIndex;
            public IReadOnlyList<LevelConfig> Levels => _levels;
        }
    }
}
