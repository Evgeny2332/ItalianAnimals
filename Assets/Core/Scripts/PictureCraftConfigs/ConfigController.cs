using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ConfigController : MonoBehaviour
{
    public static ConfigController Instance;

    [SerializeField] private PictureCraftConfig _pictureCraftConfig;
    private PictureCraft _pictureCraft;

    [SerializeField] private AudioSource _buttonClick;

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

    private void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;
    private void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(FindPictureCraftCoroutine());
    }

    private IEnumerator FindPictureCraftCoroutine()
    {
        yield return null; 

        _pictureCraft = FindObjectOfType<PictureCraft>();

        if (_pictureCraft != null)
            _pictureCraft.Init(_pictureCraftConfig);
    }

    public void SetConfig(PictureCraftConfig config) => _pictureCraftConfig = config;
    public void PlayButtonSound() => _buttonClick.Play();
}
