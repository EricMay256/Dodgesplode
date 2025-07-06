using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance;
  //todo: currently starts active, will eventually have staging areas
  public GameState CurrentGameState { get; private set; } = GameState.Active;

  [SerializeField] Timer _timer;

  Camera _mainCam;
  Bounds _camBounds, _spawnBounds;
  public Bounds CamBounds => _camBounds;
  public Bounds SpawnBounds => _spawnBounds;
  float _previousCamSize;
  Vector3 _previousCamPos;

  public delegate void GameStateChange(GameState newState);
  public static event GameStateChange OnGameStateChanged;

  public void StartTransition()
  {
    SetGameState(GameState.Transition);
  }
  public void EndTransition()
  {
    SetGameState(GameState.Active);
  }

  public void GameOver()
  {
    if (_timer != null)
    {
      Debug.Log($"Player is dead after {_timer.TimeElapsed.ToString("0")} seconds!");
    }
    else
    {
      Debug.Log("Player is dead! (No timer found)");
    }

    SetGameState(GameState.GameOver);
  }

  public void PauseGame()
  {
    if (CurrentGameState == GameState.Active)
    {
      SetGameState(GameState.Paused);
      //_pauseCanvas.gameObject.SetActive(true);
    }
    else if (CurrentGameState == GameState.Paused)
    {
      SetGameState(GameState.Active);
      //_pauseCanvas.gameObject.SetActive(false);
    }
    //Todo: consider other screens like options, etc.
  }

  void Awake()
  {
    // Ensure only one instance of GameManager exists
    if (Instance != null && Instance != this)
    {
      Destroy(gameObject);
      return;
    }
    // Singleton pattern to ensure only one instance of GameManager exists
    if (Instance == null)
    {
      Instance = this;
      //DontDestroyOnLoad(gameObject);
    }
    else
    {
      Destroy(gameObject);
    }
    _mainCam = Camera.main;
  }
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    //Initial GameState
    SetGameState(GameState.GameSetup);

    _camBounds = new Bounds(_mainCam.transform.position,
      _mainCam.GetComponent<Camera>().orthographicSize * 2f *
      new Vector3(_mainCam.aspect, 1));
    _spawnBounds = new Bounds(_camBounds.center, CamBounds.size * 1.25f);
    _previousCamSize = _mainCam.orthographicSize;
    _previousCamPos = _mainCam.transform.position;
    SetGameState(GameState.Active);
  }

  // Update is called once per frame
  void Update()
  {
    if (PlayerInputManager.Instance.PausePressed)
    {
      Debug.Log("Pause pressed");
      PauseGame();
    }
    if (_previousCamPos != _mainCam.transform.position)
    {
      _spawnBounds.center = _camBounds.center = _previousCamPos = _mainCam.transform.position;
    }
    if (_previousCamSize != _mainCam.orthographicSize)
    {
      _camBounds.size = _mainCam.orthographicSize * 2f * new Vector3(_mainCam.aspect, 1);
      _spawnBounds.size = _camBounds.size * 1.25f;
      _previousCamSize = _mainCam.orthographicSize;
    }
  }

  public void ResetGame()
  {
    SetGameState(GameState.GameSetup);
    SetGameState(GameState.Active);
  }

  private void SetGameState(GameState newState)
  {
    CurrentGameState = newState;
    switch (newState)
    {
      case GameState.GameplayLobby:
        break;
      case GameState.GameSetup:
        _timer.ResetTimer();
        EnemyManager.Instance.ResetEnemies();
        Player.Instance.ResetPlayer();
        LevelManager.Instance.GenerateLevel();
        break;
      case GameState.Active:
        Time.timeScale = 1f;
        PlayerInputManager.Instance.SetGameplayControlsActive(true);
        Player.Instance.DoubleCheckMovementMultiplier();
        if (RoomManager.Instance.RoomBounds.Contains(Player.Instance.Position) == false)
        {
          //Player.Instance.transform.position = RoomManager.Instance.RoomBounds.center;
        }
        break;
      case GameState.Transition:
        Time.timeScale = 1f;
        PlayerInputManager.Instance.SetGameplayControlsActive(false);
        break;
      case GameState.Paused:
        Time.timeScale = 0f;
        PlayerInputManager.Instance.SetGameplayControlsActive(false);
        break;
      case GameState.GameOver:
        Time.timeScale = 0f;
        PlayerInputManager.Instance.SetGameplayControlsActive(false);
        break;
      case GameState.GameWon:
        break;
      case GameState.Options:
        break;
    }
    OnGameStateChanged?.Invoke(newState);
    Debug.Log($"Game state changed to: {newState}");
  }
}
