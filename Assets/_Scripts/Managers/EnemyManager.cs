using System.Collections.Generic;
using UnityEngine;

public enum SpawnedEdge
{
    Right = 0,
    Top = 1,
    Left = 2,
    Bottom = 3
}
public class EnemyManager : MonoBehaviour
{
    public static IEnumerable<SpawnedEdge> SpawnableEdges { get { return new SpawnedEdge [] 
    {SpawnedEdge.Right, SpawnedEdge.Top, SpawnedEdge.Left, SpawnedEdge.Bottom} ; } }

    public static EnemyManager Instance;
    
    [SerializeField]
    GameObject _enemyParent, _emptyPrefab;

    [SerializeField]
    private float _speedMultiplier = 1f;

    [SerializeField]
    EnemySpawnList _enemySpawnList;
    [SerializeField]
    private List<float> _spawnTimers = new List<float>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        //_spawnTimer = _spawnTime;
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        //UpdateSpawnList(_enemies);
        foreach(EnemySpawnData enemy in _enemySpawnList.EnemySpawns)
        {
            _spawnTimers.Add(0f);
            var v = Instantiate(_emptyPrefab, _enemyParent.transform);
            v.transform.name = enemy.EnemyPrefab.name + " Group";
        }
    }

    // Update is called once per frame
    void Update()
    {
        //If camera gets resized, update the bounds
        for(int i = 0; i < _spawnTimers.Count; i++)
        {
            _spawnTimers[i] -= Time.deltaTime;
            if(_spawnTimers[i] <= 0f)
            {
                _spawnTimers[i] += _enemySpawnList.EnemySpawns[i].SpawnTime;
                for(int j = 0; j < _enemySpawnList.EnemySpawns[i].SpawnsPerWave; j++)
                {
                    SpawnEnemy(i);
                }
            }
        }
    }

    public void ClearAllEnemies()
    {
        while(_enemyParent.transform.childCount > 0)
        {
            ///Todo: When pooling implemented, return all enemies to pool
            _enemyParent.transform.GetChild(0).GetComponent<Enemy>().DestroyEnemy();
        }
    }

    void SpawnEnemy(int index)
    {
        Enemy enemy = Instantiate(_enemySpawnList.EnemySpawns[index].EnemyPrefab, transform.position, Quaternion.identity);
        enemy.ChangeSpawnableEdges(_enemySpawnList.EnemySpawns[index].SpawnableEdges);
        enemy.SetUpEnemy(_enemySpawnList.EnemySpawns[index].SpeedModifier * _speedMultiplier);
        enemy.transform.SetParent(_enemyParent.transform.GetChild(index));
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
        UpdateSpawnList(enemies.EnemySpawns);
    }
    public void UpdateSpawnList(IEnumerable<EnemySpawnData> enemies)
    {
        _enemySpawnList.EnemySpawns.Clear();
        _spawnTimers.Clear();
        ClearAllEnemies();
        foreach(EnemySpawnData enemy in enemies)
        {
            _enemySpawnList.EnemySpawns.Add(enemy);
            _spawnTimers.Add(enemy.SpawnTime);
            Instantiate(_emptyPrefab, _enemyParent.transform);
        }
    }

    public void ResetEnemies()
    {
        foreach(Transform child in _enemyParent.transform)
        {
            foreach(Enemy enemy in child.GetComponentsInChildren<Enemy>())
            {
                enemy.DestroyEnemy();
            }
        }
    }
}
