using UnityEngine;

public class WindowsController : MonoBehaviour
{
    [SerializeField] private GameObject _lastWindow;
    [SerializeField] private AudioSource _clickSound;

    public void OpenWindow(GameObject window)
    {
        _lastWindow.SetActive(false);
        window.SetActive(true);
        _lastWindow = window;
        _clickSound.Play();
    }
}
