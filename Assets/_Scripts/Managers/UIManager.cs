using UnityEngine;
using UnityEngine.UIElements;

namespace BearFalls
{
  public class UIManager : MonoBehaviour
  {
    [SerializeField]
    Canvas _gamePlayCanvas, _pauseCanvas, _gameOverCanvas;

    void OnEnable()
    {
      GameManager.OnGameStateChanged += HandleGameStateChange;
    }

    void OnDisable()
    {
      GameManager.OnGameStateChanged -= HandleGameStateChange;
    }

    public void HandleGameStateChange(GameState newState)
    {
      switch (newState)
      {
        case GameState.Active:
          _gamePlayCanvas.gameObject.SetActive(true);
          _pauseCanvas.gameObject.SetActive(false);
          _gameOverCanvas.gameObject.SetActive(false);
          break;
        case GameState.Paused:
          _gamePlayCanvas.gameObject.SetActive(true);
          _pauseCanvas.gameObject.SetActive(true);
          _gameOverCanvas.gameObject.SetActive(false);
          break;
        case GameState.GameOver:
          _gamePlayCanvas.gameObject.SetActive(false);
          _pauseCanvas.gameObject.SetActive(false);
          _gameOverCanvas.gameObject.SetActive(true);
          break;
        default:
          break;
      }
    }
  }
}