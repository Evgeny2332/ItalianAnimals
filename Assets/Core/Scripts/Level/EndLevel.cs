using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    [SerializeField] private PictureCraft _pictureCraft;
    [SerializeField] private AudioSource _audioSource;

    private void OnEnable()
    {
        _pictureCraft.OnCraftComplete += StartExitToMenu;
    }
    private void OnDisable()
    {
        _pictureCraft.OnCraftComplete -= StartExitToMenu;
    }

    private void StartExitToMenu()
    {
        _audioSource.Play();
        StartCoroutine(TimerToExit());
    }
    private IEnumerator TimerToExit()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(0);
    }

    public void ExitToMenu()
    {
        ConfigController.Instance.PlayButtonSound();
        SceneManager.LoadScene(0);
    }
}
