using System.Collections.Generic;
using UnityEngine;

public class SpawningBehavior : MonoBehaviour
{
  [SerializeField]
  EnemySpawningType _spawningType = EnemySpawningType.Child;
  [SerializeField]
  int _spawnCount = 3, _spawnRadius; // -1 indicates infinite spawning
  [SerializeField]
  float _spawnInterval = 2.0f; // Time in seconds between spawns
  float _spawnTimer = 0.0f;
  [SerializeField]
  EnemySpawnData _spawnedEnemyData; // Reference to the enemy prefab to spawn
  List<Enemy> _spawnedEnemies;

  Vector3 GetRadiusSpawnPosition()
  {
    if (_spawnRadius == 0)
    {
      return transform.position;
    }
    // Calculate a random position within the spawn radius
    float randomAngle = Random.Range(0f, 360f);
    float radius = Random.Range(0f, _spawnRadius);
    return new Vector3(Mathf.Cos(randomAngle) * radius, Mathf.Sin(randomAngle) * radius, 0);
  }

  float GetAngleFromVector(Vector3 position)
  {
    if (position == Vector3.zero)
      return Random.Range(0f, 360f);
    // Calculate the angle from the center of the spawn area to the position
    Vector2 direction = position - transform.position;
    return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
  }

  void SpawnEnemy(Vector3 position, float angle = 0.0f, bool useWorldSpace = false)
  {
    Debug.Log($"Spawning enemy at {position} with angle {angle} (World Space: {useWorldSpace})");
    if (_spawningType == EnemySpawningType.ReplenishingChild || _spawningType == EnemySpawningType.Child)
    {
      var obj = Instantiate(_spawnedEnemyData.EnemyPrefab, transform);
      if (!useWorldSpace)
      {
        obj.transform.localPosition = position;
        obj.transform.localRotation = Quaternion.Euler(0, 0, angle);
      }
      else
      {
        obj.transform.position = position;
        obj.transform.rotation = Quaternion.Euler(0, 0, angle);
      }
      if (_spawningType == EnemySpawningType.ReplenishingChild)
      {
        obj.OnEnemyDestroyed += IncrementSpawnCount;
      }
    }
    else
    {
      if (!useWorldSpace)
      {
        Debug.LogWarning("Triggered spawning type requires world space positioning!");
        return;
      }
      var obj = Instantiate(_spawnedEnemyData.EnemyPrefab, position, Quaternion.Euler(0, 0, angle));
      if (_spawningType == EnemySpawningType.ReplenishingTriggeredRadius)
      {
        obj.OnEnemyDestroyed += IncrementSpawnCount;
        _spawnedEnemies.Add(obj);
      }
    }
  }

  void SpawnEnemy(Direction edge, bool useClosestPoint = false)
  {
    // This method can be used to spawn an enemy at a random position on the specified edge

    // todo: implement logic for following cases
    
      // case EnemySpawningType.TriggeredEdges:
    //   SpawnEnemy(_spawnedEnemyData.GetSpawnableEdge(), false);
    //   break;
    // case EnemySpawningType.TriggeredClosestPointToSelf:
    // case EnemySpawningType.TriggeredClosestPointToPlayer:
    //   SpawnEnemy(_spawnedEnemyData.GetSpawnableEdge(), true);
    //   break;
  }

  void SpawnEnemy()
  {
    Vector3 spawnPosition;
    float spawnAngle = 0f;

    switch (_spawningType)
    {
      case EnemySpawningType.Child:
      case EnemySpawningType.ReplenishingChild:
      case EnemySpawningType.ReplenishingTriggeredRadius:
      case EnemySpawningType.TriggeredRadius:
        spawnPosition = GetRadiusSpawnPosition();
        spawnAngle = GetAngleFromVector(spawnPosition);
        SpawnEnemy(spawnPosition, spawnAngle, false);
        break;
      case EnemySpawningType.TriggeredEdges:
        SpawnEnemy(_spawnedEnemyData.GetSpawnableEdge(), false);
        break;
      case EnemySpawningType.TriggeredClosestPointToSelf:
      case EnemySpawningType.TriggeredClosestPointToPlayer:
        SpawnEnemy(_spawnedEnemyData.GetSpawnableEdge(), true);
        break;
      default:
        Debug.LogError("Unsupported spawning type!");
        break;
    }
  }

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void OnEnable()
  {
    _spawnTimer = 0.0f; // Reset the spawn timer
    if (_spawnCount == 0)
    {
      Debug.LogWarning("No enemies will be spawned as _spawnCount is set to 0.");
    }
    if (_spawningType == EnemySpawningType.ReplenishingTriggeredRadius)
    {
      _spawnedEnemies = new List<Enemy>();
    }
  }

  void OnDisable()
  {
    if (_spawningType == EnemySpawningType.ReplenishingTriggeredRadius)
    {
      foreach (var enemy in _spawnedEnemies)
      {
        enemy.OnEnemyDestroyed -= IncrementSpawnCount;
      }
      _spawnedEnemies.Clear();
    }
  }

  // Update is called once per frame
  void Update()
  {
    _spawnTimer += Time.deltaTime;
    if (_spawningType == EnemySpawningType.ReplenishingChild || _spawningType == EnemySpawningType.ReplenishingTriggeredRadius)
    {
      _spawnTimer = 0;
    }
    if (_spawnTimer >= _spawnInterval)
    {
      _spawnTimer = 0.0f;
      if (_spawnCount > 0 || _spawnCount == -1)
      {
        SpawnEnemy();
        if (_spawnCount > 0)
        {
          _spawnCount--;
        }
      }
    }
      
  }
  
  public void IncrementSpawnCount()
  {
    _spawnCount ++;
  }
}
