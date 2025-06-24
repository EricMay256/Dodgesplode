using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
  public static LevelManager Instance;
  [SerializeField]
  List<GameObject> _roomPrefabs;
  [SerializeField]
  GameObject _startingRoomPrefab;
  [SerializeField]
  Transform _roomParentTransform;
  List<RoomData> _roomDataList = new List<RoomData>();
  List<DoorInfo> _expandableDoorList = new List<DoorInfo>();
  Dictionary<Vector3Int, List<GameObject>> _roomPrefabDictionary = new Dictionary<Vector3Int, List<GameObject>>();
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
        Vector3Int key = new Vector3Int(i, j, 1);
        if (!_roomPrefabDictionary.ContainsKey(key))
        {
          _roomPrefabDictionary.Add(key, new List<GameObject>());
        }
      }
    }
    //Populate the room dictionary with prefabs
    foreach (GameObject roomPrefab in _roomPrefabs)
    {
      if (roomPrefab == null) continue;
      RoomData roomData = roomPrefab.GetComponent<RoomData>();
      if (roomData == null) continue;
      _roomPrefabDictionary[roomData.RoomBounds.size].Add(roomPrefab);
    }
    _roomPrefabs.Clear();

  }

  void Start()
  {
  }

  public void GenerateLevel()
  {
    int roomCount = 2;
    // Clear Existing rooms
    foreach (var room in _roomDataList)
    {
      Destroy(room.gameObject);
    }
    _roomDataList.Clear();
    _expandableDoorList.Clear();

    RoomData roomData;
    GameObject roomPrefab, roomObject;
    List<GameObject> sizedRooms;
    DoorInfo selectedDoor, newDoor;
    Vector2Int newRoomPosition2;
    Vector3Int newRoomPosition3;

    // Generate starting room
    roomObject = Instantiate(_startingRoomPrefab, _roomParentTransform);
    roomData = roomObject.GetComponent<RoomData>();
    _roomDataList.Add(roomData);

    //Initialize player and set room as active
    RoomManager.Instance.SetActiveRoom(roomObject);
    RoomManager.Instance.CenterPlayerInActiveRoom();

    // Add each possible door to potential addition list
    _expandableDoorList.AddRange(roomData.GetPossibleDoors());

    // While room count is not yet reached,
    while (_roomDataList.Count < roomCount)
    {
      /// Pick a random door from the potential addition list
      selectedDoor = _expandableDoorList[Random.Range(0, _expandableDoorList.Count)];
      _expandableDoorList.Remove(selectedDoor);
      newDoor = selectedDoor.GetMatchingDoor();
      ///Create the new room
      newRoomPosition2 = selectedDoor.GetPointedPosition();
      newRoomPosition3 = new Vector3Int(newRoomPosition2.x, newRoomPosition2.y, 0);
      BoundsInt newRoomBounds = new BoundsInt(newRoomPosition3, Vector3Int.one);

      /// Attempt to expand based on random chance and existing layout

      /// Select random room that has space for a door connected to the expanding door
      sizedRooms = _roomPrefabDictionary[newRoomBounds.size];
      foreach (GameObject room in sizedRooms)
      {
        //roomObject = room;
        //roomData = roomObject.GetComponent<RoomData>();
        //roomData.SetRoomPos(newRoomPosition3);
        // If the room has a door that matches the selected door, instantiate it
        if (roomData.GetPossibleDoors().Contains(newDoor))
        {
          roomPrefab = room;
          break;
        }
      }
      roomPrefab = sizedRooms[Random.Range(0, sizedRooms.Count)];

      /// Generate new room      
      roomObject = Instantiate(roomPrefab, _roomParentTransform);
      roomData = roomObject.GetComponent<RoomData>();
      roomData.SetRoomPos(selectedDoor.GetPointedPosition());
      _roomDataList.Add(roomData);
      //// If a door has a matching partner, connect them based on percentage chance
      //// Remove all doors pointing to the new room from the potential addition list
      /// Designate new room as occupied in grid
    }


      /// Last X rooms will be pickup rooms, which will not add their doors to the potential addition list
      //// Pickup rooms should have a pickup and a portal back to the origin
  }

  // Update is called once per frame
  void Update()
  {

  }

}
