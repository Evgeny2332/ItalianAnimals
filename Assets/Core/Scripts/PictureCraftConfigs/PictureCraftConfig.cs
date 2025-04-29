using UnityEngine;

[CreateAssetMenu(fileName = "NewPictureConfig", menuName = "CustomConfigs/PictureConfig")]
public class PictureCraftConfig : ScriptableObject
{
    [SerializeField] private Sprite _icon;
    [SerializeField] private Sprite[] _pieceIcons;

    public Sprite Icon => _icon;
    public Sprite[] PieceIcons => _pieceIcons;
}
