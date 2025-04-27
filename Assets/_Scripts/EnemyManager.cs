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
    private float _spawnTime = .33f;
    private float _spawnTimer;
    [SerializeField]
    private int _spawnsPerWave = 1;

    [SerializeField]
    List<Enemy> _enemies = new List<Enemy>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _spawnTimer = _spawnTime;
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
        _cam.GetComponent<Camera>().orthographicSize * 2f * _cam.aspect * Vector3.one);
        _spawnBounds = new Bounds(transform.position, _bounds.size * 1.25f);
    }

    // Update is called once per frame
    void Update()
    {
        _bounds.center = transform.position;
        _spawnTimer -= Time.deltaTime;
        if(_spawnTimer <= 0f)
        {
            _spawnTimer += _spawnTime;
            for(int i = 0; i < _spawnsPerWave; i++)
            {
                SpawnEnemy();
            }
        }
    }

    void SpawnEnemy()
    {
        Enemy enemy = Instantiate(_enemies[0], transform.position, Quaternion.identity);
        enemy.SetUpEnemy();
        enemy.transform.SetParent(transform);
    }
}
