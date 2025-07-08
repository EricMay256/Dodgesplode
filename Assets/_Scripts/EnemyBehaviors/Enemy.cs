using System;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using Random = UnityEngine.Random;


public class Enemy : MonoBehaviour
{
  protected SpriteRenderer _sr;
  protected Direction _spawnedEdge;
  float _maxHealth = 1f, _health = 1f;
  public float Damage { get; private set; } = 10f;
  public float Scale = 1f;
  float _speedModifier = 1f;
  public float SpeedModifier => _speedModifier;
  [SerializeField]
  bool _usePooling = false;
  public bool UsePooling => _usePooling;
  protected List<Direction> _spawnableEdges = new List<Direction>();

  public virtual void SetUpEnemy(EnemyLevelStats levelStats)
  {
    transform.localScale = new Vector3(Scale * levelStats.Scale, Scale * levelStats.Scale, 1f);
    _speedModifier = levelStats.SpeedModifier1 * EnemyManager.Instance.SpeedMultiplier;
    Damage = levelStats.Damage;
    _maxHealth = levelStats.MaxHealth;
    _health = _maxHealth;
  }

  public void ChangeSpawnableEdges(IEnumerable<Direction> edges)
  {
    _spawnableEdges.Clear();
    foreach (Direction edge in edges)
    {
      _spawnableEdges.Add(edge);
    }
  }

  public virtual void DestroyEnemy()
  {
    if (UsePooling)
    {
      Debug.Log("Pooling not implemented!");
    }
    else
    {
      Destroy(gameObject);
    }
  }

  public virtual void PlaceOnSpawningBounds()
  {
    PlaceOnSpawningBounds(_spawnableEdges[Random.Range(0, _spawnableEdges.Count)]);
  }
  public virtual void PlaceOnSpawningBounds(Direction edge)
  {
    _spawnedEdge = edge;
    transform.position = RoomManager.Instance.GetSpawnLocation(_spawnedEdge);
  }
  public virtual void PlaceClosestToPlayer()
  {
    PlaceClosestToPlayer(_spawnableEdges[Random.Range(0, _spawnableEdges.Count)]);
    
  }
  public virtual void PlaceClosestToPlayer(Direction edge)
  {
    _spawnedEdge = edge;
    transform.position = RoomManager.Instance.MinimumDistanceToPlayerPoint(_spawnedEdge);
  }
}


