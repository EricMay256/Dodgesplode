using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "EnemySpawnList", menuName = "Scriptable Objects/EnemySpawnList")]
public class EnemySpawnList : ScriptableObject
{
  public List<EnemySpawnEntry> EnemySpawns;
  public EnemySpawnList()
  {
    EnemySpawns = new List<EnemySpawnEntry>();
  }
  public void Clear()
  {
    EnemySpawns.Clear();
  }
}
