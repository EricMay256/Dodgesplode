using UnityEngine;

[CreateAssetMenu(fileName = "EnemySpawning", menuName = "Scriptable Objects/EnemySpawning")]
public class EnemySpawning : ScriptableObject
{
    [SerializeField]
    public float SpawnTime = .33f;
    [SerializeField]
    public int SpawnsPerWave = 1;
    [SerializeField]
    public Enemy EnemyPrefab;
    [SerializeField]
    public float SpeedModifier = 1f;
    [SerializeField]
    public float SpawnTimeDecreaseStep = 0.05f;
}
[System.Serializable]
public class EnemySpawnData
{
    public Enemy EnemyPrefab;
    public float SpawnTime;
    public int SpawnsPerWave;
    public float SpeedModifier;
    public float SpawnTimeDecreaseStep;

    public EnemySpawnData(Enemy enemyPrefab, float spawnTime, int spawnsPerWave, float speedModifier, float spawnTimeDecreaseStep)
    {
        EnemyPrefab = enemyPrefab;
        SpawnTime = spawnTime;
        SpawnsPerWave = spawnsPerWave;
        SpeedModifier = speedModifier;
        SpawnTimeDecreaseStep = spawnTimeDecreaseStep;
    }
    public EnemySpawnData(EnemySpawning es)
    {
        EnemyPrefab = es.EnemyPrefab;
        SpawnTime = es.SpawnTime;
        SpawnsPerWave = es.SpawnsPerWave;
        SpeedModifier = es.SpeedModifier;
        SpawnTimeDecreaseStep = es.SpawnTimeDecreaseStep;
    }
}
