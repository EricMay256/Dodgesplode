using UnityEngine;

namespace BearFalls
{
  public class Door : MonoBehaviour
  {
    #region Declarations
    [SerializeField]
    DoorInfo _doorInfo;
    public DoorInfo DoorInfo => _doorInfo;
    GameObject _containingRoomObj, _connectedRoom;
    Room _containingRoom;
    #endregion

    #region Public Methods
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
        transform.position = new Vector3(
          _doorInfo.GridLocation.x * Room.GridToWorldScale.x,
          _doorInfo.GridLocation.y * Room.GridToWorldScale.y,
          0);
        switch (_doorInfo.Orientation)
        {
          case Direction.Top:
            transform.position += Vector3.up * (Room.GridToWorldScale.y / 2 - 0.75f);
            break;
          case Direction.Right:
            transform.position += Vector3.right * (Room.GridToWorldScale.x / 2 - 0.75f);
            break;
          case Direction.Bottom:
            transform.position += Vector3.down * (Room.GridToWorldScale.y / 2 - 0.75f);
            break;
          case Direction.Left:
            transform.position += Vector3.left * (Room.GridToWorldScale.x / 2 - 0.75f);
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

    public void TravelThroughDoor()
    {
      if (_connectedRoom == null)
      {
        Debug.LogError("Connected room is not set for the door!");
        return;
      }
      if (_containingRoom == null)
      {
        Debug.LogError("Containing room is not set for the door!");
        return;
      }
      // Handle the logic for traveling through the door
      GameManager.Instance.StartTransition();
      Player.Instance.DoorMotion(_doorInfo.Orientation, _containingRoom);
      RoomManager.Instance.SetActiveRoom(_connectedRoom);
    }

    public void ConnectDoor(GameObject connectedRoom)
    {
      _connectedRoom = connectedRoom;
    }
    #endregion
  }
}