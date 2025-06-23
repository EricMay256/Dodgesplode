using System.Collections.Generic;
using UnityEngine;

public enum DoorAvailability
{
  None = 0,
  Left_or_Bottom = 1,
  Right_or_Top = 2,
  Both = 3
}
public class RoomData : MonoBehaviour
{
  public AudioClip RoomMusic;
  public EnemySpawnList EnemySpawnList;

  [SerializeField]
  Vector2Int _roomSize = new Vector2Int(1, 1);
  public Vector2Int RoomSize => _roomSize;
  Vector2Int _roomOffset = new Vector2Int(0, 0);
  public Vector2Int RoomOffset => _roomOffset;
  static Vector2 GridToWorldScale = new Vector2(17.75f, 10f);

  [SerializeField]
  List<DoorAvailability> _topBottomDoorAvailability = new List<DoorAvailability>() ;
  public List<DoorAvailability> TopBottomDoorAvailability => _topBottomDoorAvailability;
  [SerializeField]
  List<DoorAvailability> _leftRightDoorAvailability = new List<DoorAvailability>();
  public List<DoorAvailability> LeftRightDoorAvailability => _leftRightDoorAvailability;
  
  public void SetRoomPos(Vector2Int gridPosition)
  {
    _roomOffset = gridPosition;
    transform.position = new Vector3(_roomOffset.x * GridToWorldScale.x, _roomOffset.y * GridToWorldScale.y, 0f);
  }
  public bool ContainsLocation(Vector2Int location)
  {
    return location.x >= _roomOffset.x && location.x < _roomOffset.x + _roomSize.x &&
           location.y >= _roomOffset.y && location.y < _roomOffset.y + _roomSize.y;
  }
  public void SetRoomOffset(Vector2Int offset)
  {
    _roomOffset = offset;
    transform.position = new Vector3(_roomOffset.x * GridToWorldScale.x, _roomOffset.y * GridToWorldScale.y, 0f);
  }
  public List<DoorInfo> GetPossibleDoors()
  {
    List<DoorInfo> doors = new List<DoorInfo>();
    DoorAvailability doorAvailable;
    for (int i = 0; i < _topBottomDoorAvailability.Count; i++)
    {
      doorAvailable = _topBottomDoorAvailability[i];
      if (doorAvailable == DoorAvailability.Right_or_Top || doorAvailable == DoorAvailability.Both)
      {
        DoorInfo doorInfo = new DoorInfo(
          new Vector2Int(_roomOffset.x + i, _roomOffset.y + _roomSize.y - 1),
          Direction.Top);
        doors.Add(doorInfo);
      }
      if (doorAvailable == DoorAvailability.Left_or_Bottom || doorAvailable == DoorAvailability.Both)
      {
        DoorInfo doorInfo = new DoorInfo(
          new Vector2Int(_roomOffset.x + i, _roomOffset.y),
          Direction.Bottom);
        doors.Add(doorInfo);
      }
    }
    for (int i = 0; i < _leftRightDoorAvailability.Count; i++)
    {
      doorAvailable = _leftRightDoorAvailability[i];
      if (doorAvailable == DoorAvailability.Left_or_Bottom || doorAvailable == DoorAvailability.Both)
      {
        DoorInfo doorInfo = new DoorInfo(
          new Vector2Int(_roomOffset.x, _roomOffset.y + i),
          Direction.Left);
        doors.Add(doorInfo);
      }
      if (doorAvailable == DoorAvailability.Right_or_Top || doorAvailable == DoorAvailability.Both)
      {
        DoorInfo doorInfo = new DoorInfo(
          new Vector2Int(_roomOffset.x + _roomSize.x - 1, _roomOffset.y + i),
          Direction.Right);
        doors.Add(doorInfo);
      }
    }

    return doors;
  }
  [ContextMenu("Fix Door List Length")]
  public void FixDoorListLength()
  {
    while (_topBottomDoorAvailability.Count < _roomSize.x)
    {
      _topBottomDoorAvailability.Add(DoorAvailability.None);
    }
    while (_leftRightDoorAvailability.Count < _roomSize.y)
    {
      _leftRightDoorAvailability.Add(DoorAvailability.None);
    }
    while (_topBottomDoorAvailability.Count > _roomSize.x)
    {
      _topBottomDoorAvailability.RemoveAt(_topBottomDoorAvailability.Count - 1);
    }
    while (_leftRightDoorAvailability.Count > _roomSize.y)
    {
      _leftRightDoorAvailability.RemoveAt(_leftRightDoorAvailability.Count - 1);
    }
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
