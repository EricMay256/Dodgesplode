using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BearFalls
{
  public class TriggerArea : MonoBehaviour
  {
    #region Declarations
    Collider2D _collider;
    [SerializeField]
    bool _instantTrigger = true;
    bool _triggered = false;
    [SerializeField]
    float _timeToTrigger = 0.5f;
    float _triggerTimer = 0f;
    [SerializeField]
    int enemyLevel = 0;
    [SerializeField]
    EnemySpawnData _enemySpawnData;
    [SerializeField]
    List<Direction> _spawnedDirections = EnumUtilities.AllDirections;
    [SerializeField]
    EnemySpawningPattern _spawningPattern = EnemySpawningPattern.All;
    [SerializeField]
    UnityEvent<Direction> _whenTriggered;
    #endregion
    #region Public Methods
    public void StartTriggered()
    {
      if (_instantTrigger)
      {
        Triggered();
      }
      else
      {
        _triggerTimer = 0;
        _triggered = true;
      }
    }
    public void StopTriggered()
    {
      _triggered = false;
      _triggerTimer = 0f;
    }
    public void Triggered()
    {
      switch (_spawningPattern)
      {
        case EnemySpawningPattern.Random:
          _whenTriggered.Invoke(_spawnedDirections[Random.Range(0, _spawnedDirections.Count)]);
          break;
        case EnemySpawningPattern.All:
          foreach (Direction dir in _spawnedDirections)
          {
            _whenTriggered.Invoke(dir);
          }
          break;
      }
    }
    public void SpawnOnEdge(Direction edge)
    {
      // Create a new enemy instance
      Enemy enemy = EnemyManager.Instance.InstantiateTriggeredEnemy(_enemySpawnData); //EnemyManager

      // Set up the enemy with the provided spawn data and level stats
      enemy.SetUpEnemy(_enemySpawnData.EnemyLevels[enemyLevel]);
      enemy.PlaceOnSpawningBounds(edge);
    }
    public void SpawnFacingPlayer(Direction edge)
    {
      // Create a new enemy instance
      Enemy enemy = EnemyManager.Instance.InstantiateTriggeredEnemy(_enemySpawnData);

      // Set up the enemy with the provided spawn data and level stats
      enemy.SetUpEnemy(_enemySpawnData.EnemyLevels[enemyLevel]);
      enemy.PlaceClosestToPlayer(edge);
    }
    #endregion
    #region MonoBehaviours
    // Update is called once per frame
    void Update()
    {
      if (_triggered)
      {
        _triggerTimer += Time.deltaTime;
        if (_triggerTimer >= _timeToTrigger)
        {
          StopTriggered();
          Triggered();
        }
      }
    }
    //Spawn enemy
    //Spawn enemy targeting player
    #endregion
  }
}