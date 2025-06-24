using UnityEngine;

public class Door : MonoBehaviour
{
  DoorInfo _doorInfo;
  public DoorInfo DoorInfo => _doorInfo;
  GameObject _containingRoom, _connectedRoom;
  public GameObject ContainingRoom => _containingRoom;
  public GameObject ConnectedRoom => _connectedRoom;

  public void SetDoorInfo(DoorInfo doorInfo)
  {
    _doorInfo = doorInfo;
    
    var roomInfo = LevelManager.Instance.GetRoomFromGridLocation(_doorInfo.GridLocation);

    _containingRoom = roomInfo.gameObject;
    //Set parent to containing room's door container

    //Calculate local grid position
    //calculate world position and set transform position
  }

  public void SetDoorInfo(Vector2Int gridLocation, Direction edge)
  {
    SetDoorInfo(new DoorInfo(gridLocation, edge));

  }

  public void ConnectDoor(GameObject connectedRoom)
  {
    _connectedRoom = connectedRoom;
    // Set the door's position based on the connected room's position
    if (_connectedRoom != null)
    {
      Vector3 connectedRoomPosition = _connectedRoom.transform.position;
      transform.position = new Vector3(connectedRoomPosition.x, connectedRoomPosition.y, transform.position.z);
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
