using UnityEngine;

public class RoomData : MonoBehaviour
{
  public AudioClip RoomMusic;
  public EnemySpawnList EnemySpawnList;
  [SerializeField]
  Vector2Int _roomSize = new Vector2Int(1, 1);
  Vector2Int _roomOffset = new Vector2Int(0, 0);
  static Vector2 GridToWorldScale = new Vector2(17.75f, 10f);
  void SetRoomOffset(Vector2Int offset)
  {
    _roomOffset = offset;
    transform.position = new Vector3(_roomOffset.x * GridToWorldScale.x, _roomOffset.y * GridToWorldScale.y, 0f);
  }
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }
    
}
