using UnityEngine;

public enum GameState
{
    GameplayLobby = 0,
    GameSetup = 1,
    Active = 2,
    BetweenRounds = 3,
    Paused = 4,
    GameOver = 5,
    GameWon = 6,

}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    //todo: currently starts active, will eventually have staging areas
    public GameState CurrentGameState { get; private set; } = GameState.Active;
    
    [SerializeField] Timer _timer;
    [SerializeField] Canvas _gameOverCanvas, _pauseCanvas;

    public void GameOver()
    {
        if(_timer != null)
        {
            Debug.Log($"Player is dead after {_timer.TimeElapsed.ToString("0")} seconds!");
        }
        else
        {
            Debug.Log("Player is dead! (No timer found)");
        }

        CurrentGameState = GameState.GameOver;
        _gameOverCanvas.gameObject.SetActive(true);
        Time.timeScale = 0f;
        PlayerInputManager.Instance.SetGameplayControlsActive(false);
    }

    public void PauseGame()
    {
        if(CurrentGameState == GameState.Active)
        {
            CurrentGameState = GameState.Paused;
            Time.timeScale = 0f;
            _pauseCanvas.gameObject.SetActive(true);
            PlayerInputManager.Instance.SetGameplayControlsActive(false);
        }
        else if(CurrentGameState == GameState.Paused)
        {
            CurrentGameState = GameState.Active;
            Time.timeScale = 1f;
            _pauseCanvas.gameObject.SetActive(false);
            PlayerInputManager.Instance.SetGameplayControlsActive(true);
        }
    }

    void Awake()
    {
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
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerInputManager.Instance.PausePressed)
        {
            Debug.Log("Pause pressed");
            PauseGame();
        }
    }

}
