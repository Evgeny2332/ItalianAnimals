using UnityEngine;
using UnityEngine.UI;

public class LevelsController : MonoBehaviour
{
    [SerializeField] private int _indexSceneLevels;

    [SerializeField] private Button[] _levelButtons;
    [SerializeField] private Sprite _completeLevel;

    [SerializeField] private Text _countStars;

    private void OnEnable()
    {
        LoadLevels();   
    }

    private void UpdateCountStarts()
    {
        int countStars = 0;
        foreach (var level in _levelButtons)
        {
            countStars += level.GetComponentInParent<LevelOpening>().GetCountStars();
        }

        _countStars.text = $"{countStars}/30";
    }

    private void LoadLevels()
    {
        int maxLevel = PlayerPrefs.GetInt($"MaxLevel{_indexSceneLevels}");

        for (int i = 0; i < _levelButtons.Length; i++)
        {
            if (i < maxLevel)
            {
                _levelButtons[i].interactable = true;
                _levelButtons[i].GetComponent<Image>().sprite = _completeLevel;
            }
            else if (i == maxLevel)
                _levelButtons[i].interactable = true;
        }

        UpdateCountStarts();
    }
}
