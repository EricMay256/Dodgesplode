using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySpawning", menuName = "Scriptable Objects/EnemySpawning")]
public class EnemySpawnData : ScriptableObject
{
  [SerializeField]
  public List<EnemyLevelStats> EnemyLevels = new List<EnemyLevelStats>();
  [SerializeField]
  public Enemy EnemyPrefab;
  [SerializeField]
  public IEnumerable<Direction> SpawnableEdges = EnemyManager.SpawnableEdges;
}
[System.Serializable]
public class EnemyLevelStats
{
  public float SpawnTime = 1f;
  public int SpawnsPerWave = 1;
  public float SpeedModifier1 = 1f;//Speed modifier for unit
  public float SpeedModifier2 = 1f;//Speed modifier for projectiles or other effects
  public int RemovedEdges = 0;
}
[System.Serializable]
public class EnemySpawnEntry
{
  public EnemySpawnData EnemyData;
  public int level;
  [System.NonSerialized]
  public EnemyLevelStats CurrentLevelStats;
  [System.NonSerialized]
  public List<Direction> SpawnableEdges = new List<Direction>(EnemyManager.SpawnableEdges);

  public EnemySpawnEntry(EnemySpawnData enemyData, int curLevel)
  {
    EnemyData = enemyData;
    level = curLevel;
    CurrentLevelStats = enemyData.EnemyLevels[curLevel];
    SpawnableEdges = new List<Direction>(EnemyManager.SpawnableEdges);
    for (int i = 0; i < CurrentLevelStats.RemovedEdges; i++)
    {
      if (SpawnableEdges.Count > 0)
      {
        SpawnableEdges.RemoveAt(Random.Range(0, SpawnableEdges.Count));
      }
    }
  }
  
  public void GetCurLevelStats()
  {
    if (level < EnemyData.EnemyLevels.Count)
    {
      CurrentLevelStats = EnemyData.EnemyLevels[level];
      SpawnableEdges = new List<Direction>(EnemyManager.SpawnableEdges);
      for (int i = 0; i < CurrentLevelStats.RemovedEdges; i++)
      {
        if (SpawnableEdges.Count > 0)
        {
          SpawnableEdges.RemoveAt(Random.Range(0, SpawnableEdges.Count));
        }
      }
    }
  }
  
  public void SetLevel(int newLevel)
  {
    if (newLevel < EnemyData.EnemyLevels.Count)
    {
      level = newLevel;
      GetCurLevelStats();
    }
  }
}

