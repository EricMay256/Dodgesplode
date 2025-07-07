using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
  float _timeElapsed = 0f;
  public float TimeElapsed => _timeElapsed;
  // UIDocument _uiDocument;
  TMP_Text _timerLabel;
  private void OnEnable()
  {
  }

  void Awake()
  {
    _timerLabel = GetComponent<TMP_Text>();
  }

  void Start()
  {
  }

  // Update is called once per frame
  void Update()
  {
    if (GameManager.Instance.CurrentGameState != GameState.Active)
      return;
    _timeElapsed += Time.deltaTime;
    _timerLabel.text = _timeElapsed.ToString("0");

  }

  public void ResetTimer()
  {
    _timeElapsed = 0f;
    _timerLabel.text = "0";
  }
}
