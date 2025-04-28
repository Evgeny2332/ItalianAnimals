using System;
using UnityEngine;

public class PictureCraft : MonoBehaviour
{
    [SerializeField] private PictureCraftConfig[] _configs;
    [SerializeField] private PiecePicture[] _piecesPicture;

    [SerializeField] private GameObject _barrier, _particle;

    public event Action OnCraftComplete;

    public void Init(int idConfig)
    {
        SetIcons(idConfig);
    }

    private void SetIcons(int idConfig)
    {
        for (int i = 0; i < _piecesPicture.Length; i++)
        {
            _piecesPicture[i].SetIcon(_configs[idConfig].PieceIcons[i]);
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
}
