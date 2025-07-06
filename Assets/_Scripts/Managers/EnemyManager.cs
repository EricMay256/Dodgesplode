using System.Collections.Generic;
using UnityEngine;


public class EnemyManager : MonoBehaviour
{
  public static IEnumerable<Direction> SpawnableEdges { get { return new Direction [] 
  {Direction.Right, Direction.Top, Direction.Left, Direction.Bottom} ; } }

  public static EnemyManager Instance;
  
  [SerializeField]
  GameObject _timerEnemyParent, _triggerEnemyParent, _emptyPrefab;

  [SerializeField]
  private float _speedMultiplier = 1f;

  [SerializeField]
  EnemySpawnList _enemySpawnList = null;
  [SerializeField]
  private List<float> _spawnTimers = new List<float>();

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
    _enemySpawnList.Clear();
  }

  void Start()
  {
    
  }
  // Update is called once per frame
  void Update()
  {
    if(GameManager.Instance.CurrentGameState != GameState.Active)
      return;
    for (int i = 0; i < _spawnTimers.Count; i++)
    {
      _spawnTimers[i] -= Time.deltaTime;
      if (_spawnTimers[i] <= 0f)
      {
        _spawnTimers[i] += _enemySpawnList.TimerEnemySpawns[i].CurrentLevelStats.SpawnTime;

        float numSpawns = _enemySpawnList.TimerEnemySpawns[i].CurrentLevelStats.SpawnsPerWave;
        switch (_enemySpawnList.TimerEnemySpawns[i].EnemyData.enemyScaling)
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

  public void ClearAllEnemies()
  {
    foreach (Transform child in _timerEnemyParent.transform)
    {
      foreach (Enemy enemy in child.GetComponentsInChildren<Enemy>())
      {
        enemy.DestroyEnemy();
      }
    }
  }

  void SpawnEnemyOnTimer(int index)
  {
      Enemy enemy = Instantiate(_enemySpawnList.TimerEnemySpawns[index].EnemyData.EnemyPrefab, transform.position, Quaternion.identity);
      enemy.ChangeSpawnableEdges(_enemySpawnList.TimerEnemySpawns[index].SpawnableEdges);
    enemy.SetUpEnemy(_enemySpawnList.TimerEnemySpawns[index].CurrentLevelStats.SpeedModifier1 * _speedMultiplier,
    _enemySpawnList.TimerEnemySpawns[index].CurrentLevelStats.Scale);

      enemy.transform.SetParent(_timerEnemyParent.transform.GetChild(index));
  }

  // public void UpdateSpawnList(List<EnemySpawning> enemies)
  // {
  //     _enemySpawnList.EnemySpawns.Clear();
  //     _spawnTimers.Clear();
  //     ClearAllEnemies();
  //     foreach(EnemySpawning enemy in enemies)
  //     {
  //         _enemySpawnList.EnemySpawns.Add(new EnemySpawnData(enemy));
  //         _spawnTimers.Add(enemy.SpawnTime);
  //         Instantiate(_emptyPrefab, _enemyParent.transform);
  //     }
  // }
  public void UpdateSpawnList(EnemySpawnList enemies)
  {
    ClearAllEnemies();
    _enemySpawnList.TimerEnemySpawns.Clear();
    _spawnTimers.Clear();
    foreach (Transform child in _timerEnemyParent.transform)
    {
      Destroy(child.gameObject);
    }
    foreach (EnemySpawnEntry enemy in enemies.TimerEnemySpawns)
    {
      enemy.GetCurLevelStats();
      _enemySpawnList.TimerEnemySpawns.Add(enemy);
      _spawnTimers.Add(enemy.CurrentLevelStats.SpawnTime);
      Instantiate(_emptyPrefab, _timerEnemyParent.transform);
    }
  }

  public void ResetEnemies()
  {
    foreach (Transform child in _timerEnemyParent.transform)
    {
      foreach (Enemy enemy in child.GetComponentsInChildren<Enemy>())
      {
        enemy.DestroyEnemy();
      }
    }
  }
}