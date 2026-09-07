using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Gameplay.Piece
{
    [RequireComponent(typeof(Image), typeof(Button))]
    public class PuzzlePieceView : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;

        private float _rotationDuration = 0.3f;
        private float _punchScale = 1.1f;
        private Sequence _sequence;

        private bool IsAnimating => _sequence != null && _sequence.IsActive() && _sequence.IsPlaying();

        public event Action Clicked;

        private void Reset()
        {
            _button = GetComponent<Button>();
            _image = GetComponent<Image>();
        }

        private void Awake() =>
            _button.onClick.AddListener(OnButtonClicked);

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnButtonClicked);

            _sequence?.Kill();
            transform.DOKill();
        }

        public void SetAnimationSettings(float rotationDuration, float punchScale)
        {
            _rotationDuration = rotationDuration;
            _punchScale = punchScale;
        }

        public void SetIcon(Sprite sprite) =>
            _image.sprite = sprite;

        public void SetAngle(int angle) =>
            transform.localEulerAngles = new Vector3(0f, 0f, angle);

        public void PlayRotation(int deltaAngle)
        {
            _sequence?.Kill(true);

            float halfDuration = _rotationDuration / 2f;
            float targetAngle = transform.localEulerAngles.z + deltaAngle;

            _sequence = DOTween.Sequence();
            _sequence.Join(transform
                .DOLocalRotate(new Vector3(0f, 0f, targetAngle), _rotationDuration, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear));
            _sequence.Join(transform.DOScale(_punchScale, halfDuration).SetEase(Ease.OutQuad));
            _sequence.Append(transform.DOScale(1f, halfDuration).SetEase(Ease.InQuad));
            _sequence.SetLink(gameObject);
        }

        private void OnButtonClicked()
        {
            if (IsAnimating)
                return;

            Clicked?.Invoke();
        }
    }
}
