using UnityEngine;
using UnityEngine.UI;

public class LevelOpening : MonoBehaviour
{
    [SerializeField] private MenuController _menuController;
    [SerializeField] private Image[] _stars;
    [SerializeField] private Sprite _star, _shadowStar;

    private void Start()
    {
        if (PlayerPrefs.HasKey($"StarsConfig"))
        {

        }
    }

    public void OpenLevel(int level)
    {
        _menuController.OpenLevel(level);
    }
}
