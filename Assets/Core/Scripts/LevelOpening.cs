using UnityEngine;
using UnityEngine.UI;

public class LevelOpening : MonoBehaviour
{
    [SerializeField] private MenuController _menuController;

    [SerializeField] private Image[] _stars;
    [SerializeField] private Sprite _star;
    [SerializeField] private PictureCraftConfig _craftConfig;

    private void Start()
    {
        if (PlayerPrefs.HasKey($"StarsConfig{_craftConfig.name}"))
        {
            int countStars = PlayerPrefs.GetInt($"StarsConfig{_craftConfig.name}");
            for ( int i = 0; i < countStars; i++ )
                _stars[i].sprite = _star;
        }
    }

    public void OpenLevel(int level)
    {
        ConfigController.Instance.SetConfig(_craftConfig);
        ConfigController.Instance.PlayButtonSound();
        _menuController.OpenLevel(level);
    }
}
