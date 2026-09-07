using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class Window : MonoBehaviour, IWindow
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private bool _isOpenOnStart;

        private CanvasGroup _canvasGroup;

        public event Action<IWindow> CloseRequested;

        protected virtual void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();

            if (_closeButton != null)
                _closeButton.onClick.AddListener(OnCloseClicked);

            SetVisible(_isOpenOnStart);
        }

        protected virtual void OnDestroy()
        {
            if (_closeButton != null)
                _closeButton.onClick.RemoveListener(OnCloseClicked);
        }

        public void Show() =>
            SetVisible(true);

        public void Close() =>
            SetVisible(false);

        private void SetVisible(bool isVisible)
        {
            _canvasGroup.alpha = isVisible ? 1f : 0f;
            _canvasGroup.interactable = isVisible;
            _canvasGroup.blocksRaycasts = isVisible;
        }

        private void OnCloseClicked() =>
            CloseRequested?.Invoke(this);
    }
}
