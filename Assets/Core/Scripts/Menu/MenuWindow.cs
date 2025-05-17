using UnityEngine;
using UnityEngine.UI;

public class MenuWindow : MonoBehaviour
{
    [SerializeField] private Text[] _stars;
    [SerializeField] private string[] _saveKeys;

    private void OnEnable()
    {
        UpdateStars();
    }

    private void UpdateStars()
    {
        for (int i = 0; i < _stars.Length; i++)
        {
            _stars[i].text = $"{PlayerPrefs.GetInt(_saveKeys[i])}/30";
        }
    }
}
