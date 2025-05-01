using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PictureCraft : MonoBehaviour
{
    private PictureCraftConfig _config;
    [SerializeField] private PiecePicture[] _piecesPicture;
    [SerializeField] private Image _icon;

    [SerializeField] private GameObject _barrier, _particle;

    [SerializeField] private Stopwatch _stopwatch;
    [SerializeField] private int[] _records;

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
        int stars = 0;
        float playerTime = _stopwatch.GetTime();

        for (int i = 0; i < _records.Length; i++)
        {
            if (playerTime <= _records[i])
            {
                stars = _records.Length - i;
                break;
            }
            else
            {
                stars = 1;
            }
        }

        if(stars > PlayerPrefs.GetInt($"StarsConfig{_config.name}"))
            PlayerPrefs.SetInt($"StarsConfig{_config.name}", stars);

        if (PlayerPrefs.GetInt("MaxLevel") < int.Parse(_config.name))
            PlayerPrefs.SetInt("MaxLevel", int.Parse(_config.name));
    }
}
