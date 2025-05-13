using System;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using Random = UnityEngine.Random;

public enum SpawnedEdge
{
    Right = 0,
    Top = 1,
    Left = 2,
    Bottom = 3

}
public class Enemy : MonoBehaviour
{
    public static IEnumerable<SpawnedEdge> SpawnableEdges => _allSpawnableEdges;
    static IEnumerable<SpawnedEdge> _allSpawnableEdges = new SpawnedEdge [] {SpawnedEdge.Right, SpawnedEdge.Top, SpawnedEdge.Left, SpawnedEdge.Bottom};

    protected SpriteRenderer _sr;
    protected SpawnedEdge _spawnedEdge;
    public float Scale = 1f;
    float _speedModifier = 1f;
    public float SpeedModifier => _speedModifier;
    [SerializeField]
    bool _usePooling = false;
    public bool UsePooling => _usePooling;
    [SerializeField]
    protected List<SpawnedEdge> _spawnableEdges = new List<SpawnedEdge>(_allSpawnableEdges);


    public virtual void SetUpEnemy(float speedModifier = 1f) {
        transform.localScale = new Vector3(Scale, Scale, 1f);
        _speedModifier = speedModifier;
    }

    public virtual void DestroyEnemy() {
        DestroyImmediate(gameObject);
    }

    protected virtual void PlaceOnSpawningBounds()
    {
        Bounds spawnBounds = GameManager.Instance.SpawnBounds;
        _spawnedEdge = _spawnableEdges[Random.Range(0, _spawnableEdges.Count)];
        switch (_spawnedEdge)//Select a random edge to spawn from
        {
            //Place transform on a random point on the selected edge
            case SpawnedEdge.Right:
                transform.position = new Vector3(spawnBounds.max.x, Random.Range(spawnBounds.min.y, spawnBounds.max.y), 0f);
                break;
            case SpawnedEdge.Top:
                transform.position = new Vector3(Random.Range(spawnBounds.min.x, spawnBounds.max.x), spawnBounds.max.y, 0f);
                break;
            case SpawnedEdge.Left:
                transform.position = new Vector3(spawnBounds.min.x, Random.Range(spawnBounds.min.y, spawnBounds.max.y), 0f);
                break;
            case SpawnedEdge.Bottom:
                transform.position = new Vector3(Random.Range(spawnBounds.min.x, spawnBounds.max.x), spawnBounds.min.y, 0f);
                break;
        }
    }
}

