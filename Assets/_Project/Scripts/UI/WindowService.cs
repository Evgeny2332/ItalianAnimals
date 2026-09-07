using System.Collections.Generic;
using _Project.Scripts.Services.Audio;

namespace _Project.Scripts.UI
{
    public sealed class WindowService : IWindowService
    {
        private readonly Stack<IWindow> _opened = new();
        private readonly IAudioService _audioService;

        public WindowService(IAudioService audioService) =>
            _audioService = audioService;

        public void Open(IWindow window)
        {
            if (window == null || _opened.Contains(window))
                return;

            window.CloseRequested += OnCloseRequested;
            window.Show();

            _opened.Push(window);
            _audioService.Play(SoundId.ButtonClick);
        }

        public void CloseTop()
        {
            if (_opened.Count == 0)
                return;

            var window = _opened.Pop();
            window.CloseRequested -= OnCloseRequested;
            window.Close();

            _audioService.Play(SoundId.ButtonClick);
        }

        private void OnCloseRequested(IWindow window)
        {
            while (_opened.Count > 0)
            {
                bool isRequested = _opened.Peek() == window;
                CloseTop();

                if (isRequested)
                    return;
            }
        }
    }
}
