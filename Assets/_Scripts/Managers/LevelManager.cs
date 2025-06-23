using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
  public static LevelManager Instance;
  [SerializeField]
  List<GameObject> _roomPrefabs;
  [SerializeField]
  GameObject _startingRoomPrefab;
  List<RoomData> _roomDataList = new List<RoomData>();
  Dictionary<Vector2Int, List<GameObject>> _roomDictionary = new Dictionary<Vector2Int, List<GameObject>>();
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  [SerializeField]
  int _maxRoomDimension = 3;
  bool IsLocationOccupied(Vector2Int location)
  {
    foreach (var room in _roomDataList)
    {
      if (room.ContainsLocation(location))
      {
        return true;
      }
    }
    return false;
  }
  public RoomData GetRoomFromGridLocation(Vector2Int gridLocation)
  {
    foreach (var room in _roomDataList)
    {
      if (room.ContainsLocation(gridLocation))
      {
        return room;
      }
    }
    return null;
  }
  void Awake()
  {
    if (Instance == null)
    {
      Instance = this;
      //DontDestroyOnLoad(gameObject);
    }
    else
    {
      Destroy(gameObject);
    }
    //Initialize lists in room dictionary
    for (int i = 1; i <= _maxRoomDimension; i++)
    {
      for (int j = 1; j <= _maxRoomDimension; j++)
      {
        Vector2Int key = new Vector2Int(i, j);
        if (!_roomDictionary.ContainsKey(key))
        {
          _roomDictionary.Add(key, new List<GameObject>());
        }
      }
    }
    //Populate the room dictionary with prefabs
    foreach (GameObject roomPrefab in _roomPrefabs)
    {
      if (roomPrefab == null) continue;
      RoomData roomData = roomPrefab.GetComponent<RoomData>();
      if (roomData == null) continue;
      _roomDictionary[roomData.RoomSize].Add(roomPrefab);
    }
  }

  public void GenerateLevel()
  {
    // Clear Existing rooms

    // Generate starting room

    // Add each possible door to potential addition list

    // Designate starting room as occupied in grid

    // While room count is not yet reached,

    /// Pick a random door from the potential addition list
    //// If the door is blocked by a room, remove it and pick again
    /// Attempt to expand based on random chance and existing layout
    /// Select random room that has space for a door connected to the expanding door
    /// Generate new room
    /// Add new room's possible doors to potential addition list
    //// If a door has a matching partner, connect them based on percentage chance
    /// Designate new room as occupied in grid

    /// Last X rooms will be pickup rooms, which will not add their doors to the potential addition list
    //// Pickup rooms should have a pickup and a portal back to the origin
  }

  // Update is called once per frame
  void Update()
  {

  }

}
