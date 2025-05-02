using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelOpening : MonoBehaviour
{
    [SerializeField] private MenuController _menuController;

    [SerializeField] private Image[] _stars;
    [SerializeField] private Sprite _star;
    [SerializeField] private PictureCraftConfig _craftConfig;
    [SerializeField] private int _numberScene;

    private void Start()
    {
        if (PlayerPrefs.HasKey($"StarsConfig{_craftConfig.name}"))
        {
            int countStars = PlayerPrefs.GetInt($"StarsConfig{_craftConfig.name}");
            for ( int i = 0; i < countStars; i++ )
                _stars[i].sprite = _star;
        }
    }

    public void OpenLevel()
    {
        ConfigController.Instance.SetConfig(_craftConfig);
        ConfigController.Instance.PlayButtonSound();

        SceneManager.LoadScene(_numberScene);

        AdsManager.ShowInterstitial();
    }
}
