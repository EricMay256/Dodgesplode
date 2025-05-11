using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    public Bounds Bounds => _bounds;
    public Bounds SpawnBounds => _spawnBounds;
    private Camera _cam;
    private Bounds _bounds, _spawnBounds;
    
    [SerializeField]
    GameObject _enemyParent, _emptyPrefab;

    [SerializeField]
    private float _speedMultiplier = 1f;

    [SerializeField]
    List<EnemySpawning> _enemies = new List<EnemySpawning>();
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
        _cam = Camera.main;
        _bounds = new Bounds(transform.position,
        _cam.GetComponent<Camera>().orthographicSize * 2f * new Vector3(_cam.aspect, 1));
        _spawnBounds = new Bounds(transform.position, _bounds.size * 1.25f);
        //UpdateSpawnList(_enemies);
        foreach(EnemySpawning enemy in _enemies)
        {
            _spawnTimers.Add(0f);
            var v = Instantiate(_emptyPrefab, _enemyParent.transform);
            v.transform.name = enemy.EnemyPrefab.name + " Group";
        }
    }

    // Update is called once per frame
    void Update()
    {
        _bounds.center = transform.position;
        //If camera gets resized, update the bounds
        for(int i = 0; i < _spawnTimers.Count; i++)
        {
            _spawnTimers[i] -= Time.deltaTime;
            if(_spawnTimers[i] <= 0f)
            {
                _spawnTimers[i] += _enemies[i].SpawnTime;
                for(int j = 0; j < _enemies[i].SpawnsPerWave; j++)
                {
                    SpawnEnemy(i);
                }
            }
        }
    }

    void SpawnEnemy(int index)
    {
        Enemy enemy = Instantiate(_enemies[index].EnemyPrefab, transform.position, Quaternion.identity);
        enemy.SetUpEnemy(_enemies[index].SpeedModifier * _speedMultiplier);
        enemy.transform.SetParent(_enemyParent.transform.GetChild(index));
    }

    public void UpdateSpawnList(List<EnemySpawning> enemies)
    {
        _enemies.Clear();
        _spawnTimers.Clear();
        while(_enemyParent.transform.childCount > 0)
        {
            ///Todo: When pooling implemented, return all enemies to pool
            DestroyImmediate(_enemyParent.transform.GetChild(0).gameObject);
        }
        foreach(EnemySpawning enemy in enemies)
        {
            _enemies.Add(enemy);
            _spawnTimers.Add(enemy.SpawnTime);
            Instantiate(_emptyPrefab, _enemyParent.transform);
        }
    }
}
