using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    [SerializeField] private PictureCraft _pictureCraft;

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
        StartCoroutine(TimerToExit());
    }
    private IEnumerator TimerToExit()
    {
        yield return new WaitForSeconds(5);
        ExitToMenu();
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
