using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "EnemySpawnList", menuName = "Scriptable Objects/EnemySpawnList")]
public class EnemySpawnList : ScriptableObject
{
  public List<EnemySpawnEntry> TimerEnemySpawns, PersistentEnemySpawns;
  public EnemySpawnList()
  {
    TimerEnemySpawns = new List<EnemySpawnEntry>();
    PersistentEnemySpawns = new List<EnemySpawnEntry>();
  }
  public void Clear()
  {
    TimerEnemySpawns.Clear();
    PersistentEnemySpawns.Clear();
  }
}
