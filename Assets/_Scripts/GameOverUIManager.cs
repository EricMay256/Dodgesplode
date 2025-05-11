using TMPro;
using UnityEngine;

public class GameOverUIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _timerText;
    [SerializeField] TextMeshProUGUI _finalTimerText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        _finalTimerText.text = $"Survived {_timerText.text} seconds";
    }
}
