using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Services.Audio
{
    [CreateAssetMenu(fileName = "AudioLibrary", menuName = "PuzzleGame/Audio Library", order = 2)]
    public class AudioLibrary : ScriptableObject
    {
        [SerializeField] private SoundEntry[] _sounds;

        private Dictionary<SoundId, SoundEntry> _map;

        public bool TryGetSound(SoundId soundId, out SoundEntry entry)
        {
            _map ??= BuildMap();
            return _map.TryGetValue(soundId, out entry) && entry.Clip != null;
        }

        private Dictionary<SoundId, SoundEntry> BuildMap()
        {
            var map = new Dictionary<SoundId, SoundEntry>();

            foreach (var sound in _sounds)
                map[sound.SoundId] = sound;

            return map;
        }

        [Serializable]
        public class SoundEntry
        {
            [SerializeField] private SoundId _soundId;
            [SerializeField] private AudioClip _clip;
            [SerializeField, Range(0f, 1f)] private float _volume = 1f;

            public SoundId SoundId => _soundId;
            public AudioClip Clip => _clip;
            public float Volume => _volume;
        }
    }
}
