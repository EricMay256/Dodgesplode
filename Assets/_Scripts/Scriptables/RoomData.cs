using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "RoomData", menuName = "Scriptable Objects/RoomData")]
public class RoomData : ScriptableObject
{
  public GameObject _roomPrefab;
  public AudioClip _roomMusic;
  public EnemySpawnList _enemySpawnList;
}
