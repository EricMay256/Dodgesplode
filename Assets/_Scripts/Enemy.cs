using UnityEngine;

public enum SpawnedEdge
{
    Right = 0,
    Top = 1,
    Left = 2,
    Bottom = 3
}
public class Enemy : MonoBehaviour
{
    protected SpriteRenderer _sr;
    protected SpawnedEdge _spawnedEdge;
    public float Scale = 1f;
    float _speedModifier = 1f;
    public float SpeedModifier => _speedModifier;
    public virtual void SetUpEnemy(float speedModifier = 1f) {
        transform.localScale = new Vector3(Scale, Scale, 1f);
        _speedModifier = speedModifier;
    }
    public virtual void DestroyEnemy() {
        Destroy(gameObject);
    }
    protected virtual void PlaceOnSpawningBounds()
    {
        Bounds spawnBounds = EnemyManager.Instance.SpawnBounds;
        switch (Random.Range(0, 4))//Select a random edge to spawn from
        {
            //Place transform on a random point on the selected edge
            case 0:
                _spawnedEdge = SpawnedEdge.Right;
                transform.position = new Vector3(spawnBounds.max.x, Random.Range(spawnBounds.min.y, spawnBounds.max.y), 0f);
                break;
            case 1:
                _spawnedEdge = SpawnedEdge.Top;
                transform.position = new Vector3(Random.Range(spawnBounds.min.x, spawnBounds.max.x), spawnBounds.max.y, 0f);
                break;
            case 2:
                _spawnedEdge = SpawnedEdge.Left;
                transform.position = new Vector3(spawnBounds.min.x, Random.Range(spawnBounds.min.y, spawnBounds.max.y), 0f);
                break;
            case 3:
                _spawnedEdge = SpawnedEdge.Bottom;
                transform.position = new Vector3(Random.Range(spawnBounds.min.x, spawnBounds.max.x), spawnBounds.min.y, 0f);
                break;
        }
    }
}

