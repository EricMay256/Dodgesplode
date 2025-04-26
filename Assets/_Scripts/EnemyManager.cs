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
        enemy.MoveAngle = Random.Range(0f, 360f);
        enemy.transform.rotation = Quaternion.Euler(0f, 0f, enemy.MoveAngle);
        //Place enemy along appropriate spawning edge
        if(enemy.MoveAngle >= 0f && enemy.MoveAngle < 90f)
        {
            if(Random.Range(0f, 1f) < 0.5f)
            {
                enemy.transform.position = new Vector3(_spawnBounds.min.x, Random.Range(_spawnBounds.min.y, _spawnBounds.max.y), 0f);
            }
            else
            {
                enemy.transform.position = new Vector3(Random.Range(_spawnBounds.min.x, _spawnBounds.max.x), _spawnBounds.min.y, 0f);
            }
        }
        else if(enemy.MoveAngle >= 90f && enemy.MoveAngle < 180f)
        {
            if(Random.Range(0f, 1f) < 0.5f)
            {
                enemy.transform.position = new Vector3(_spawnBounds.max.x, Random.Range(_spawnBounds.min.y, _spawnBounds.max.y), 0f);
            }
            else
            {
                enemy.transform.position = new Vector3(Random.Range(_spawnBounds.min.x, _spawnBounds.max.x), _spawnBounds.min.y, 0f);
            }
        }
        else if(enemy.MoveAngle >= 180f && enemy.MoveAngle < 270f)
        {
            if(Random.Range(0f, 1f) < 0.5f)
            {
                enemy.transform.position = new Vector3(_spawnBounds.max.x, Random.Range(_spawnBounds.min.y, _spawnBounds.max.y), 0f);
            }
            else
            {
                enemy.transform.position = new Vector3(Random.Range(_spawnBounds.min.x, _spawnBounds.max.x), _spawnBounds.min.y, 0f);
            }        
        }
        else
        {

            if(Random.Range(0f, 1f) < 0.5f)
            {
                enemy.transform.position = new Vector3(_spawnBounds.min.x, Random.Range(_spawnBounds.min.y, _spawnBounds.max.y), 0f);
            }
            else
            {
                enemy.transform.position = new Vector3(Random.Range(_spawnBounds.min.x, _spawnBounds.max.x), _spawnBounds.max.y, 0f);
            }        
        }
        enemy.transform.SetParent(transform);
    }
}
