using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BearFalls
{
  public class Timer : MonoBehaviour
  {
    #region Declarations
    float _timeElapsed = 0f;
    public float TimeElapsed => _timeElapsed;
    TMP_Text _timerLabel;
    #endregion
    #region Public Methods
    public void ResetTimer()
    {
      _timeElapsed = 0f;
      _timerLabel.text = "0";
    }
    #endregion

    #region MonoBehaviours
    private void OnEnable()
    {
      ResetTimer();
    }

    void Awake()
    {
      _timerLabel = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
      if (GameManager.Instance.CurrentGameState != GameState.Active)
        return;
      _timeElapsed += Time.deltaTime;
      _timerLabel.text = _timeElapsed.ToString("0");

    }
    #endregion
  }
}