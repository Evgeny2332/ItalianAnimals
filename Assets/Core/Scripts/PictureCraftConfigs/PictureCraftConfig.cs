using UnityEngine;

[CreateAssetMenu(fileName = "NewPictureConfig", menuName = "CustomConfigs/PictureConfig")]
public class PictureCraftConfig : ScriptableObject
{
    [SerializeField] private Sprite[] _pieceIcons;

    public Sprite[] PieceIcons => _pieceIcons;
}
