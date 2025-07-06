using System;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using Random = UnityEngine.Random;


public class Enemy : MonoBehaviour
{
    protected SpriteRenderer _sr;
    protected Direction _spawnedEdge;
    public float Scale = 1f;
    float _speedModifier = 1f;
    public float SpeedModifier => _speedModifier;
    [SerializeField]
    bool _usePooling = false;
    public bool UsePooling => _usePooling;
    protected List<Direction> _spawnableEdges = new List<Direction>();

    public virtual void SetUpEnemy(float speedModifier = 1f, float scale = 1f) {
      transform.localScale = new Vector3(Scale * scale, Scale * scale, 1f);
        _speedModifier = speedModifier;
    }

    public void ChangeSpawnableEdges(IEnumerable<Direction> edges)
    {
        _spawnableEdges.Clear();
        foreach (Direction edge in edges)
        {
            _spawnableEdges.Add(edge);
        }
    }

    public virtual void DestroyEnemy() {
        Destroy(gameObject);
    }

    protected virtual void PlaceOnSpawningBounds()
    {
      _spawnedEdge = _spawnableEdges[Random.Range(0, _spawnableEdges.Count)];
      transform.position = RoomManager.Instance.GetSpawnLocation(_spawnedEdge);
    }
}

