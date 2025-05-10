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
}
