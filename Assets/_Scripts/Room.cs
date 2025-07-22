using System.Collections.Generic;
using UnityEngine;

namespace BearFalls
{
  public class Room : MonoBehaviour
  {
    #region Declarations
    public AudioClip RoomMusic;
    public EnemySpawnList TimerEnemySpawnList, PersistentEnemySpawnList;

    [SerializeField]
    BoundsInt _roomBounds;
    public BoundsInt RoomBounds => _roomBounds;
    public static readonly Vector2 GridToWorldScale = new Vector2(20f, 10f);

    [SerializeField]
    List<DoorAvailability> _topBottomDoorAvailability = new List<DoorAvailability>();
    public List<DoorAvailability> TopBottomDoorAvailability => _topBottomDoorAvailability;
    [SerializeField]
    List<DoorAvailability> _leftRightDoorAvailability = new List<DoorAvailability>();
    public List<DoorAvailability> LeftRightDoorAvailability => _leftRightDoorAvailability;

    bool _persistentEnemiesSpawned = false;
    #endregion
    #region Public Methods
    public void TrySpawnPersistentEnemies()
    {
      if (_persistentEnemiesSpawned)
        return;
      _persistentEnemiesSpawned = true;
      //TODO: Spawn persistent enemies
    }
    public void SetRoomPos(Vector2Int gridPosition)
    {
      SetRoomPos(new Vector3Int(gridPosition.x, gridPosition.y, 0));
    }
    public void SetRoomPos(Vector3Int gridPosition)
    {
      _roomBounds.position = gridPosition;
      transform.position = new Vector3(_roomBounds.position.x * GridToWorldScale.x, _roomBounds.position.y * GridToWorldScale.y, 0f);
    }
    public bool ContainsLocation(Vector2Int location)
    {
      return ContainsLocation(new Vector3Int(location.x, location.y, 0));
    }
    public bool ContainsLocation(Vector3Int location)
    {
      return _roomBounds.Contains(location);
    }
    public void SetRoomOffset(Vector2Int offset)
    {
      SetRoomOffset(new Vector3Int(offset.x, offset.y, 0));
    }
    public void SetRoomOffset(Vector3Int offset)
    {
      _roomBounds.position = offset;
      transform.position = new Vector3(_roomBounds.min.x * GridToWorldScale.x, _roomBounds.min.y * GridToWorldScale.y, 0f);
    }
    public List<DoorInfo> GetPossibleDoors()
    {
      return GetPossibleDoors(new Vector2Int(0, 0));
    }
    public List<DoorInfo> GetPossibleDoors(Vector3Int offset)
    {
      return GetPossibleDoors(new Vector2Int(offset.x, offset.y));
    }

    public List<DoorInfo> GetPossibleDoors(Vector2Int offset)
    {
      List<DoorInfo> doors = new List<DoorInfo>();
      DoorAvailability doorAvailable;
      for (int i = 0; i < _topBottomDoorAvailability.Count; i++)
      {
        doorAvailable = _topBottomDoorAvailability[i];
        if (doorAvailable == DoorAvailability.Right_or_Top || doorAvailable == DoorAvailability.Both)
        {
          DoorInfo doorInfo = new DoorInfo(
            new Vector2Int(_roomBounds.min.x + i, _roomBounds.min.y + _roomBounds.size.y - 1) + offset,
            Direction.Top);
          doors.Add(doorInfo);
        }
        if (doorAvailable == DoorAvailability.Left_or_Bottom || doorAvailable == DoorAvailability.Both)
        {
          DoorInfo doorInfo = new DoorInfo(
            new Vector2Int(_roomBounds.min.x + i, _roomBounds.min.y) + offset,
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
            new Vector2Int(_roomBounds.min.x, _roomBounds.min.y + i) + offset,
            Direction.Left);
          doors.Add(doorInfo);
        }
        if (doorAvailable == DoorAvailability.Right_or_Top || doorAvailable == DoorAvailability.Both)
        {
          DoorInfo doorInfo = new DoorInfo(
            new Vector2Int(_roomBounds.min.x + _roomBounds.size.x - 1, _roomBounds.min.y + i) + offset,
            Direction.Right);
          doors.Add(doorInfo);
        }
      }

      return doors;
    }
    #endregion
    #region Context Menu
    [ContextMenu("Fix Door List Length")]
    public void FixDoorListLength()
    {
      while (_topBottomDoorAvailability.Count < _roomBounds.size.x)
      {
        _topBottomDoorAvailability.Add(DoorAvailability.None);
      }
      while (_leftRightDoorAvailability.Count < _roomBounds.size.y)
      {
        _leftRightDoorAvailability.Add(DoorAvailability.None);
      }
      while (_topBottomDoorAvailability.Count > _roomBounds.size.x)
      {
        _topBottomDoorAvailability.RemoveAt(_topBottomDoorAvailability.Count - 1);
      }
      while (_leftRightDoorAvailability.Count > _roomBounds.size.y)
      {
        _leftRightDoorAvailability.RemoveAt(_leftRightDoorAvailability.Count - 1);
      }
    }
    [ContextMenu("Set Active Room")]
    public void DebugSetRoomActive()
    {
      RoomManager.Instance.SetActiveRoom(gameObject);
    }
    [ContextMenu("Activate Room")]
    public void ActivateRoom()
    {
      foreach (Transform child in transform)
      {
        child.gameObject.SetActive(true);
      }
      TrySpawnPersistentEnemies();
    }
    [ContextMenu("Deactivate Room")]
    public void DeactivateRoom()
    {
      foreach (Transform child in transform)
      {
        child.gameObject.SetActive(false);
      }
    }
    #endregion
  }
}