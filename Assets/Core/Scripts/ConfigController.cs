using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ConfigController : MonoBehaviour
{
    public static ConfigController Instance { get; private set; }

    [SerializeField] private PictureCraftConfig[] _pictureCraftConfigs;
    private PictureCraft _pictureCraft;
    private int _configIndex;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
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

        if (_pictureCraft != null && _configIndex >= 0 && _configIndex < _pictureCraftConfigs.Length)
            _pictureCraft.Init(_pictureCraftConfigs[_configIndex]);
    }

    public void OpenLevel(int level)
    {
        _configIndex = level - 1;

        if (level >= 1 && level <= 7)
            SceneManager.LoadScene(1);
        else if (level >= 8 && level <= 14)
            SceneManager.LoadScene(2);
        else if (level >= 15 && level <= 21)
            SceneManager.LoadScene(3);

        Debug.Log(level);
    }
}
