using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject[] _windows;
    private int _selectedIndex = 0;

    [SerializeField] private Button[] _levelButtons;

    public void SwitchWindow(int direction)
    {
        _windows[_selectedIndex].SetActive(false);
        _selectedIndex += direction;

        if (_selectedIndex >= _windows.Length)
            _selectedIndex = 0;
        else if(_selectedIndex < 0)
            _selectedIndex = _windows.Length - 1;

        _windows[_selectedIndex].SetActive(true);
    }

    public void OpenLevel(int level)
    {
        ConfigController.Instance.SetConfigIndex(level - 1);

        if (level >= 1 && level <= 7)
            SceneManager.LoadScene(1);
        else if (level >= 8 && level <= 14)
            SceneManager.LoadScene(2);
        else if (level >= 14 && level <= 21)
            SceneManager.LoadScene(3);
    }
}
