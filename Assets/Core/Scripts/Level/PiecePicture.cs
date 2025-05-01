using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class PiecePicture : MonoBehaviour
{
    private bool _isActiveRotate, _isFirst = true;
    private Image _icon;
    private AudioSource _clickSound;

    public event Action OnPieceMoved;

    private void Awake()
    {
        _clickSound = GetComponent<AudioSource>();
        _icon = GetComponent<Image>();
    }

    private void Start()
    {
        RotatePiece(RandomAngle());
        _isFirst = false;
    }

    private int RandomAngle()
    {
        int x = UnityEngine.Random.Range(0, 3);
        if (x == 0)
            return 90;
        else if (x == 1)
            return -90;
        else
            return 180;
    }

    public void SetIcon(Sprite sprite) => _icon.sprite = sprite;
    public bool IsCorrect() => Quaternion.Angle(transform.rotation, Quaternion.Euler(0, 0, 0)) < 1f;
    public void RotatePiece(int value)
    {
        if (_isActiveRotate) return;

        _isActiveRotate = true;

        float rotateDuration = 0.3f;
        float halfDuration = rotateDuration / 2f;

        Sequence sequence = DOTween.Sequence();

        sequence.Join(
            transform.DORotate(
                new Vector3(0, 0, transform.eulerAngles.z + value),
                rotateDuration
            ).SetEase(Ease.Linear)
        );

        sequence.Join(
            transform.DOScale(1.1f, halfDuration).SetEase(Ease.OutQuad)
        );

        sequence.Append(
            transform.DOScale(1f, halfDuration).SetEase(Ease.InQuad)
        );

        sequence.OnComplete(() =>
        {
            _isActiveRotate = false;
            OnPieceMoved?.Invoke();
        });

        if(!_isFirst)
            _clickSound.Play();
    }
}
