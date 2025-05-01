using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Stopwatch : MonoBehaviour
{
    [SerializeField] private Text _stopwatchText;
    private float _timeElapsed = 0f;
    private bool _isRunning = false;

    [SerializeField] private PictureCraft _pictureCraft;

    private void OnEnable() => _pictureCraft.OnCraftComplete += StopTimer;
    private void OnDisable() => _pictureCraft.OnCraftComplete -= StopTimer;

    private void Start()
    {
        _stopwatchText.text = FormatTime(_timeElapsed);
        StartTimer();
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0:0}:{1:00}", minutes, seconds);
    }

    public void StartTimer()
    {
        if (!_isRunning)
        {
            _isRunning = true;
            StartCoroutine(TimerCoroutine());
        }
    }

    public void StopTimer()
    {
        if (_isRunning)
        {
            _isRunning = false;
            StopCoroutine(TimerCoroutine());
        }
    }

    public void ResetTimer()
    {
        _timeElapsed = 0f;
        _stopwatchText.text = FormatTime(_timeElapsed);
        if (_isRunning)
        {
            StopCoroutine(TimerCoroutine());
            _isRunning = false;
        }
    }

    public int GetTime() => (int)_timeElapsed;

    private IEnumerator TimerCoroutine()
    {
        while (_isRunning)
        {
            _timeElapsed += Time.deltaTime;
            _stopwatchText.text = FormatTime(_timeElapsed);
            yield return null;
        }
    }
}
