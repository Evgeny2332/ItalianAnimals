using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Stopwatch : MonoBehaviour
{
    [SerializeField] private Text stopwatchText;
    private float timeElapsed = 0f;
    private bool isRunning = false;

    [SerializeField] private PictureCraft _pictureCraft;

    private void OnEnable() => _pictureCraft.OnCraftComplete += StopTimer;
    private void OnDisable() => _pictureCraft.OnCraftComplete -= StopTimer;

    private void Start()
    {
        stopwatchText.text = FormatTime(timeElapsed);
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
        if (!isRunning)
        {
            isRunning = true;
            StartCoroutine(TimerCoroutine());
        }
    }

    public void StopTimer()
    {
        if (isRunning)
        {
            isRunning = false;
            StopCoroutine(TimerCoroutine());
        }
    }

    public void ResetTimer()
    {
        timeElapsed = 0f;
        stopwatchText.text = FormatTime(timeElapsed);
        if (isRunning)
        {
            StopCoroutine(TimerCoroutine());
            isRunning = false;
        }
    }

    private IEnumerator TimerCoroutine()
    {
        while (isRunning)
        {
            timeElapsed += Time.deltaTime;
            stopwatchText.text = FormatTime(timeElapsed);
            yield return null;
        }
    }
}
