using UnityEngine;

namespace _Project.Scripts.Services.Audio
{
    public sealed class AudioService : IAudioService
    {
        private readonly AudioSource _audioSource;
        private readonly AudioLibrary _audioLibrary;

        public AudioService(AudioSource audioSource, AudioLibrary audioLibrary)
        {
            _audioSource = audioSource;
            _audioLibrary = audioLibrary;
        }

        public void Play(SoundId soundId)
        {
            if (_audioSource == null || _audioLibrary == null)
                return;

            if (_audioLibrary.TryGetSound(soundId, out var sound))
                _audioSource.PlayOneShot(sound.Clip, sound.Volume);
        }
    }
}
