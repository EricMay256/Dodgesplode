using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
  //UIElements implementation
  /*[SerializeField]
  UIDocument _gamePlayDocument, _pauseDocument, _gameOverDocument;
  VisualElement _gameplayRoot, _pauseRoot, _gameOverRoot;
  */
  [SerializeField]
  Canvas _gamePlayCanvas, _pauseCanvas, _gameOverCanvas;
  void Awake()
  {


  }

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    
  }

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

  // Update is called once per frame
  void Update()
  {

  }
    
}
