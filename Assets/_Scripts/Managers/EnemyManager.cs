using System.Collections.Generic;
using UnityEngine;


public class EnemyManager : MonoBehaviour
{
  #region Declarations
  public static IEnumerable<Direction> SpawnableEdges
  {
    get
    {
      return new Direction[]
  {Direction.Right, Direction.Top, Direction.Left, Direction.Bottom};
    }
  }

  public static EnemyManager Instance;

  [SerializeField]
  Transform _timerEnemyParent, _triggerEnemyParent, _emptyPrefab;
  public Transform TimerEnemyParent => _timerEnemyParent;
  public Transform TriggerEnemyParent => _triggerEnemyParent;

  [SerializeField]
  public float SpeedMultiplier { get; private set; } = 1f;

  [SerializeField]
  EnemySpawnList _timerEnemySpawnList = null;
  [SerializeField]
  List<EnemySpawnData> _triggeredEnemySpawnList = null;
  [SerializeField]
  private List<float> _spawnTimers = new List<float>();
  #endregion
  #region Helper Methods
  void SpawnEnemyOnTimer(int index)
  {
    Enemy enemy = Instantiate(_timerEnemySpawnList.EnemySpawns[index].EnemyData.EnemyPrefab, transform.position, Quaternion.identity);
    enemy.transform.SetParent(_timerEnemyParent.GetChild(index));

    enemy.ChangeSpawnableEdges(_timerEnemySpawnList.EnemySpawns[index].SpawnableEdges);

    enemy.SetUpEnemy(_timerEnemySpawnList.EnemySpawns[index].CurrentLevelStats);
    enemy.PlaceOnSpawningBounds();
  }
  #endregion
  #region Public Methods
  public Enemy InstantiateTriggeredEnemy(EnemySpawnData e)
  {
    return Instantiate(e.EnemyPrefab.gameObject, _triggerEnemyParent.GetChild(_triggeredEnemySpawnList.IndexOf(e))).GetComponent<Enemy>();
  }

  public void SetSpeedMultiplier(float newSpeedMultiplier)
  {
    SpeedMultiplier = newSpeedMultiplier;
  }

  public void ClearAllEnemies()
  {
    foreach (Transform child in _timerEnemyParent.transform)
    {
      foreach (Enemy enemy in child.GetComponentsInChildren<Enemy>())
      {
        enemy.DestroyEnemy();
      }
    }
    foreach (Transform child in _triggerEnemyParent.transform)
    {
      foreach (Enemy enemy in child.GetComponentsInChildren<Enemy>())
      {
        enemy.DestroyEnemy();
      }
    }
  }

  public void UpdateSpawnList(EnemySpawnList timerEnemies)
  {
    ClearAllEnemies();
    _timerEnemySpawnList.EnemySpawns.Clear();
    _spawnTimers.Clear();
    foreach (Transform child in _timerEnemyParent)
    {
      Destroy(child.gameObject);
    }
    foreach (EnemySpawnEntry enemy in timerEnemies.EnemySpawns)
    {
      enemy.GetCurLevelStats();
      _timerEnemySpawnList.EnemySpawns.Add(enemy);
      _spawnTimers.Add(enemy.CurrentLevelStats.SpawnTime);
      var v = Instantiate(_emptyPrefab, _timerEnemyParent);
      v.name = enemy.EnemyData.EnemyPrefab.name;
    }
  }
  #endregion
  #region Monobehaviours
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Awake()
  {
    //_spawnTimer = _spawnTime;
    if (Instance == null)
    {
      Instance = this;
    }
    else
    {
      Destroy(gameObject);
    }
    _timerEnemySpawnList.Clear();
    foreach (EnemySpawnData e in _triggeredEnemySpawnList)
    {
      var v = Instantiate(_emptyPrefab, _triggerEnemyParent);
      v.name = e.EnemyPrefab.name;
    }
  }

  // Update is called once per frame
  void Update()
  {
    if (GameManager.Instance.CurrentGameState != GameState.Active)
      return;
    for (int i = 0; i < _spawnTimers.Count; i++)
    {
      _spawnTimers[i] -= Time.deltaTime;
      if (_spawnTimers[i] <= 0f)
      {
        _spawnTimers[i] += _timerEnemySpawnList.EnemySpawns[i].CurrentLevelStats.SpawnTime;

        float numSpawns = _timerEnemySpawnList.EnemySpawns[i].CurrentLevelStats.SpawnsPerWave;
        switch (_timerEnemySpawnList.EnemySpawns[i].EnemyData.EnemyScaling)
        {
          case EnemyRoomSizeScaling.None:
            break;
          case EnemyRoomSizeScaling.Horizontal:
            numSpawns *= RoomManager.Instance.GetCurRoomSize().x;
            break;
          case EnemyRoomSizeScaling.Vertical:
            numSpawns *= RoomManager.Instance.GetCurRoomSize().y;
            break;
          case EnemyRoomSizeScaling.Magnitude:
            numSpawns = numSpawns * Mathf.Sqrt(RoomManager.Instance.GetCurRoomSize().x * RoomManager.Instance.GetCurRoomSize().y);
            float extraChance = numSpawns % 1f;
            if (Random.Range(0f, 1f) < extraChance)
            {
              numSpawns = Mathf.Floor(numSpawns + 1f);
            }
            break;
          case EnemyRoomSizeScaling.FullPerimeter:
            numSpawns *= RoomManager.Instance.GetCurRoomSize().x * RoomManager.Instance.GetCurRoomSize().y;
            break;
        }
        for (int j = 0; j < numSpawns; j++)
        {
          SpawnEnemyOnTimer(i);
        }
      }
    }
  }
  #endregion
}