using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject[] _windows;
    private int _selectedIndex = 0;

    [SerializeField] private Button[] _levelButtons;
    [SerializeField] private Sprite _completeLevel;

    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        ActivateLevels();
    }

    private void ActivateLevels()
    {
        int maxLevel = PlayerPrefs.GetInt("MaxLevel");

        for (int i = 0; i < _levelButtons.Length; i++)
        {
            if (i < maxLevel)
            {
                _levelButtons[i].interactable = true;
                _levelButtons[i].GetComponent<Image>().sprite = _completeLevel;
            }
            else if (i == maxLevel)
            {
                _levelButtons[i].interactable = true;
            }
        }
    }

    public void SwitchWindow(int direction)
    {
        _windows[_selectedIndex].SetActive(false);
        _selectedIndex += direction;

        if (_selectedIndex >= _windows.Length)
            _selectedIndex = 0;
        else if(_selectedIndex < 0)
            _selectedIndex = _windows.Length - 1;

        _windows[_selectedIndex].SetActive(true);

        ConfigController.Instance.PlayButtonSound();
    }

    public void OpenLevel(int level)
    {
        if (level >= 1 && level <= 7)
            SceneManager.LoadScene(1);
        else if (level >= 8 && level <= 14)
            SceneManager.LoadScene(2);
        else if (level >= 14 && level <= 21)
            SceneManager.LoadScene(3);
    }
}
