using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ConfigController : MonoBehaviour
{
    [SerializeField] private int _configIndex;
    private PictureCraft _pictureCraft;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(FindPictureCraftCoroutine());
    }

    private IEnumerator FindPictureCraftCoroutine()
    {
        yield return null;

        _pictureCraft = FindObjectOfType<PictureCraft>();

        if (_pictureCraft != null)
        {
            _pictureCraft.Init(_configIndex);
        }
    }

    public void OpenLevel(int level)
    {
        _configIndex = level - 1;
        SceneManager.LoadScene(1);
    }
}
