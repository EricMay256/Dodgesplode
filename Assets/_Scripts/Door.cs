using UnityEngine;

public class Door : MonoBehaviour
{
  [SerializeField]
  DoorInfo _doorInfo;
  public DoorInfo DoorInfo => _doorInfo;
  GameObject _containingRoomObj, _connectedRoom;
  public GameObject ContainingRoom => _containingRoomObj;
  public GameObject ConnectedRoom => _connectedRoom;
  Room _containingRoom;

  public void SetDoorInfo(DoorInfo doorInfo)
  {
    //Set parent to containing room's door container

    //Calculate local grid position
    //calculate world position and set transform position
    _doorInfo = doorInfo;
    _containingRoom = GetComponentInParent<Room>();
    _containingRoomObj = _containingRoom.gameObject;

    // Set the door's position based on the containing room's position
    if (_containingRoomObj != null)
    {
      Vector3 containingRoomPosition = _containingRoomObj.transform.position;
      transform.position = new Vector3(
        _doorInfo.GridLocation.x * Room.GridToWorldScale.x,
        _doorInfo.GridLocation.y * Room.GridToWorldScale.y,
        0);
      switch(_doorInfo.Orientation)
      {
        case Direction.Top:
          transform.position += Vector3.up * 4.75f;
          break;
        case Direction.Right:
          transform.position += Vector3.right * 8.675f;
          break;
        case Direction.Bottom:
          transform.position += Vector3.down * 4.75f;
          break;
        case Direction.Left:
          transform.position += Vector3.left * 8.675f;
          break;
        default:
          Debug.LogError("Invalid door orientation specified!");
          break;
      }
    }
    else
    {
      Debug.LogError("Containing room is not set for the door!");
    }
  }

  public void SetDoorInfo(Vector2Int gridLocation, Direction edge)
  {
    SetDoorInfo(new DoorInfo(gridLocation, edge));

  }

  public void ConnectDoor(GameObject connectedRoom)
  {
    _connectedRoom = connectedRoom;
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
