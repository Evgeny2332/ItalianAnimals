using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class PiecePicture : MonoBehaviour
{
    private bool _isActiveRotate;
    private Image _icon;

    public event Action OnPieceMoved;

    private void Awake()
    {
        _icon = GetComponent<Image>();
    }

    private void Start()
    {
        RotatePiece(RandomAngle());
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
        transform.DORotate(new Vector3(0, 0, transform.eulerAngles.z + value), 0.3f).SetEase(Ease.Linear).OnComplete(() =>
        {
            _isActiveRotate = false;
            OnPieceMoved?.Invoke();
        });
    }

}
