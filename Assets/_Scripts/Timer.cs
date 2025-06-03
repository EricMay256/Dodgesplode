using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Timer : MonoBehaviour
{
  float _timeElapsed = 0f;
  public float TimeElapsed => _timeElapsed;
  UIDocument _uiDocument;
  Label _timerLabel;
  private void OnEnable()
  {
  }

  void Awake()
  {
    _uiDocument = GetComponent<UIDocument>();
  }

  void Start()
  {
    _timerLabel = _uiDocument.rootVisualElement.Q<Label>("TimerLbl");
    if (_timerLabel == null)
    {
      Debug.LogError("TimerLabel not found in the UIDocument.");
      enabled = false; // Disable this script if the label is not found
    }
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
