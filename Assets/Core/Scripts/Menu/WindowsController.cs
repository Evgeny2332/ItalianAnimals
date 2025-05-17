using UnityEngine;

public class WindowsController : MonoBehaviour
{
    [SerializeField] private GameObject _lastWindow;

    public void OpenWindow(GameObject window)
    {
        _lastWindow.SetActive(false);
        window.SetActive(true);
        _lastWindow = window;
    }
}
