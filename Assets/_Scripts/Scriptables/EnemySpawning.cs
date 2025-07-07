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
  public List<Direction> SpawnableEdges = new List<Direction>(EnumUtilities.AllDirections);
  public EnemyRoomSizeScaling EnemyScaling = EnemyRoomSizeScaling.FullPerimeter;
  public EnemySpawnType SpawnType = EnemySpawnType.SpawnOnTimer;
  public float AngleChangeRate = 5f;
  public float ChaseDuration = -1f; // -1 means infinite chase duration
  public float LifeSpan = -1f; // Time after which the enemy will be destroyed if not destroyed earlier. -1 means infinite lifespan
}
[System.Serializable]
public class EnemyLevelStats
{
  /// <summary>
  /// Time in seconds between spawns of this enemy type.
  /// If the enemy is persistent or triggered, this value is ignored.
  /// </summary>
  public float SpawnTime = 1f;
  public int SpawnsPerWave = 1;
  public float SpeedModifier1 = 1f;//Speed modifier for unit
  public float SpeedModifier2 = 1f;//Speed modifier for projectiles or other effects
  public float Scale = 1f; // Scale of the enemy, if null, uses the default scale of the prefab
  public int RemovedEdges = 0;
  public float AngleChangeRateMulti = 1f;
  public float ChaseDuration = -1f; // -1 means infinite chase duration
  public float LifeSpan = -1f; // Time after which the enemy will be destroyed if not destroyed earlier. -1 means infinite lifespan

  public EnemyLevelStats() {SpawnTime = 1f; SpawnsPerWave = 1; SpeedModifier1 = 1f; SpeedModifier2 = 1f; Scale = 1f; RemovedEdges = 0; AngleChangeRateMulti = 5f; ChaseDuration = -1f; LifeSpan = -1f;}
}
[System.Serializable]
public class EnemySpawnEntry
{
  public EnemySpawnData EnemyData;
  public int level;
  [System.NonSerialized]
  public EnemyLevelStats CurrentLevelStats;
  [System.NonSerialized]
  public List<Direction> SpawnableEdges;

  public EnemySpawnEntry(EnemySpawnData enemyData, int curLevel)
  {
    EnemyData = enemyData;
    level = curLevel;
    CurrentLevelStats = enemyData.EnemyLevels[curLevel];
    SpawnableEdges = new List<Direction>(enemyData.SpawnableEdges);
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
      SpawnableEdges = new List<Direction>(EnemyData.SpawnableEdges);
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

