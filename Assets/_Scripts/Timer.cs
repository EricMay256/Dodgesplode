using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    TextMeshProUGUI _timerText;
    float _timeElapsed = 0f;
    public float TimeElapsed => _timeElapsed;

    void Awake()
    {
        _timerText = GetComponent<TextMeshProUGUI>();
        _timerText.text = "00";
    }

    // Update is called once per frame
    void Update()
    {
        _timeElapsed += Time.deltaTime;
        _timerText.text = _timeElapsed.ToString("0");
    }

    public void ResetTimer()
    {
        _timeElapsed = 0f;
        _timerText.text = "00";
    }
}
