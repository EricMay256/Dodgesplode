using System;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using Random = UnityEngine.Random;


public class Enemy : MonoBehaviour
{
    protected SpriteRenderer _sr;
    protected SpawnedEdge _spawnedEdge;
    public float Scale = 1f;
    float _speedModifier = 1f;
    public float SpeedModifier => _speedModifier;
    [SerializeField]
    bool _usePooling = false;
    public bool UsePooling => _usePooling;
    protected List<SpawnedEdge> _spawnableEdges = new List<SpawnedEdge>();

    public virtual void SetUpEnemy(float speedModifier = 1f) {
        transform.localScale = new Vector3(Scale, Scale, 1f);
        _speedModifier = speedModifier;
    }

    public virtual void ChangeSpawnableEdges(IEnumerable<SpawnedEdge> edges)
    {
        _spawnableEdges.Clear();
        foreach (SpawnedEdge edge in edges)
        {
            _spawnableEdges.Add(edge);
        }
    }

    public virtual void DestroyEnemy() {
        Destroy(gameObject);
    }

    protected virtual void PlaceOnSpawningBounds()
    {
      Bounds spawnBounds = GameManager.Instance.SpawnBounds;
      _spawnedEdge = _spawnableEdges[Random.Range(0, _spawnableEdges.Count)];
      transform.position = RoomManager.Instance.GetSpawnLocation(_spawnedEdge);
    }
}

