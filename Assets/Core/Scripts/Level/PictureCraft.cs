using System;
using UnityEngine;
using UnityEngine.UI;

public class PictureCraft : MonoBehaviour
{
    private PictureCraftConfig _config;
    [SerializeField] private PiecePicture[] _piecesPicture;
    [SerializeField] private Image _icon;

    [SerializeField] private GameObject _barrier, _particle;

    //[SerializeField] private int[]

    public event Action OnCraftComplete;

    public void Init(PictureCraftConfig config)
    {
        _config = config;
        SetIcons();
    }

    private void SetIcons()
    {
        _icon.sprite = _config.Icon;
        for (int i = 0; i < _piecesPicture.Length; i++)
        {
            _piecesPicture[i].SetIcon(_config.PieceIcons[i]);
            _piecesPicture[i].OnPieceMoved += CheckAllPiecesCorrect;
        }
    }

    private void CheckAllPiecesCorrect()
    {
        if (AreAllPiecesCorrect())
        {
            _barrier.SetActive(true);
            _particle.SetActive(true);
            OnCraftComplete?.Invoke();

            SaveData();
        }
    }

    private bool AreAllPiecesCorrect()
    {
        foreach (var piece in _piecesPicture)
        {
            if (!piece.IsCorrect())
                return false;
        }
        return true;
    } 

    private void SaveData()
    {


        //PlayerPrefs.SetInt($"StarsConfig{_config.name}", 1);
    }
}
