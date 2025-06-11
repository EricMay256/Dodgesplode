using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "EnemySpawnList", menuName = "Scriptable Objects/EnemySpawnList")]
public class EnemySpawnList : ScriptableObject
{
  public List<EnemySpawnData> EnemySpawns;
  public EnemySpawnList()
  {
    EnemySpawns = new List<EnemySpawnData>();
  }
  public void Clear()
  {
    EnemySpawns.Clear();
  }
}
