using System;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using Random = UnityEngine.Random;


namespace BearFalls
{
  public class Enemy : MonoBehaviour
  {
    #region Declarations
    protected SpriteRenderer _sr;
    protected Direction _spawnedEdge;
    [field: SerializeField] public AimType AimType { get; protected set; } = AimType.Random;
    float _maxHealth = 1f, _health = 1f;
    public float Damage { get; private set; } = 10f;
    public float Scale = 1f;
    float _speedModifier = 1f;
    public float SpeedModifier => _speedModifier;
    [SerializeField]
    bool _usePooling = false;
    public bool UsePooling => _usePooling;
    protected List<Direction> _spawnableEdges = new List<Direction>();
    private float _lifeSpan = -1f; // Time after which the enemy will be destroyed if not destroyed earlier. -1 means infinite lifespan
    protected float _timeAlive { get; private set; } = 0f; // Timer to track chase duration
    public delegate void EnemyDestroyed();
    public event EnemyDestroyed OnEnemyDestroyed;
    #endregion
    #region Public Methods
    public virtual void SetUpEnemy(EnemyLevelStats levelStats)
    {
      transform.localScale = new Vector3(Scale * levelStats.Scale, Scale * levelStats.Scale, 1f);
      _speedModifier = levelStats.SpeedModifier1 * EnemyManager.Instance.SpeedMultiplier;
      Damage = levelStats.Damage;
      _maxHealth = levelStats.MaxHealth;
      _health = _maxHealth;
      _lifeSpan = levelStats.LifeSpan;
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
      OnEnemyDestroyed?.Invoke();
      if (UsePooling)
      {
        Debug.Log("Pooling not implemented!");
        OnEnemyDestroyed = null;
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
      transform.position = RoomManager.Instance.MinimumDistanceToPoint(Player.Instance.transform.position, _spawnedEdge);
    }
    #endregion
    #region Monobehaviours
    protected virtual void Update()
    {
      _timeAlive += Time.deltaTime;
      if (_lifeSpan > 0f && _timeAlive >= _lifeSpan)
      {
        DestroyEnemy();
        return;
      }
    }
    #endregion
  }
}