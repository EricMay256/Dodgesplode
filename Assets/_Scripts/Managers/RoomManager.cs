using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
  public static RoomManager Instance;
  public Room Room => _roomData;

  private GameObject _currentRoomObject;
  public Room RoomData => _roomData;
  Room _roomData;
  CinemachineCamera _virtCam;
  [SerializeField]
  private Bounds _roomBounds;
  public Bounds RoomBounds => _roomBounds;

  private List<Bounds> _leftBounds = new List<Bounds>();
  private List<Bounds> _rightBounds = new List<Bounds>();
  private List<Bounds> _topBounds = new List<Bounds>();
  private List<Bounds> _bottomBounds = new List<Bounds>();

  /// <summary>
  /// Returns a random spawn location on the specified edge of the room.
  /// </summary>
  /// <param name="edge">Which collider direction is being used for spawn</param>
  /// <returns>World space position chosen from specified collider along the closest edge to the room</returns>
  public Vector3 GetSpawnLocation(Direction edge)
  {
    if (_roomData == null)
    {
      Debug.LogError("Room data is not set!");
      return Vector3.zero;
    }

    Bounds chosenEdge;
    switch (edge)//Select a random edge to spawn from
    {
      //Place transform on a random point on the selected edge
      case Direction.Right:
        chosenEdge = _rightBounds[Random.Range(0, _rightBounds.Count)];
        return new Vector3(chosenEdge.min.x + 1, Random.Range(chosenEdge.min.y, chosenEdge.max.y), 0f);
      case Direction.Top:
        chosenEdge = _topBounds[Random.Range(0, _topBounds.Count)];
        return new Vector3(Random.Range(chosenEdge.min.x, chosenEdge.max.x), chosenEdge.min.y + 1, 0f);
      case Direction.Left:
        chosenEdge = _leftBounds[Random.Range(0, _leftBounds.Count)];
        return new Vector3(chosenEdge.max.x - 1, Random.Range(chosenEdge.min.y, chosenEdge.max.y), 0f);
      case Direction.Bottom:
        chosenEdge = _bottomBounds[Random.Range(0, _bottomBounds.Count)];
        return new Vector3(Random.Range(chosenEdge.min.x, chosenEdge.max.x), chosenEdge.max.y - 1, 0f);
      default:
        Debug.LogError("Invalid edge specified for spawn location!");
        return Vector3.zero;
    }
  }

  /// <summary>
  /// Loads a new room based on the provided Room.
  /// </summary>
  /// <param name="roomData">Object containing information on room to be loaded</param>
  public void SetActiveRoom(GameObject roomObj)
  {
    //Avoid null reference exceptions
    if (roomObj == null)
    {
      Debug.LogError("Room prefab is null!");
      return;
    }
    if(_roomData != null)
    {
      //Deactivate current room
      _roomData.DeactivateRoom();
    }
    //Update game state
      if (GameManager.Instance.CurrentGameState == GameState.Active)
        GameManager.Instance.StartTransition();

    //Destroy current room prefab on delay and load new room
    if (_currentRoomObject != null)
    {
      //Deactivate current room
    }
    _currentRoomObject = roomObj;
    _roomData = _currentRoomObject.GetComponent<Room>();
    _roomData.ActivateRoom();

    //Set the new room's virtual camera target to the player
    _virtCam = _currentRoomObject.GetComponentInChildren<CinemachineCamera>();
    if (_virtCam == null)
    {
      Debug.LogError("CinemachineCamera component not found in children!");
    }
    _virtCam.Target.TrackingTarget = Player.Instance.transform;
    _virtCam.Prioritize();
    

    //Update bounds used for spawning enemies based on room edges
    _leftBounds.Clear();
    _rightBounds.Clear();
    _topBounds.Clear();
    _bottomBounds.Clear();
    foreach (var col in _currentRoomObject.transform.GetChild(0).GetComponentsInChildren<BoxCollider2D>())
    {
      Bounds bounds = col.bounds;
      switch (col.tag)
      {
        case "LeftEdge":
          _leftBounds.Add(bounds);
          break;
        case "RightEdge":
          _rightBounds.Add(bounds);
          break;
        case "TopEdge":
          _topBounds.Add(bounds);
          break;
        case "BottomEdge":
          _bottomBounds.Add(bounds);
          break;
        default:
          Debug.LogWarning("Unknown edge tag: " + col.tag);
          break;
      }
    }
    _roomBounds = _currentRoomObject.transform.GetChild(4).GetComponent<CompositeCollider2D>().bounds;


    //Play room music if available
    AudioManager.Instance.PlayMusic(_roomData.RoomMusic);
    //Inform the enemy manager about the new room's enemy spawn list
    EnemyManager.Instance.UpdateSpawnList(_roomData.EnemySpawnList.EnemySpawns);
    //Update game state
    if(GameManager.Instance.CurrentGameState == GameState.Transition)
      GameManager.Instance.EndTransition();
  }

  public void CenterPlayerInActiveRoom()
  {
    if (_currentRoomObject == null)
    {
      Debug.LogError("No active room set!");
      return;
    }
    Player.Instance.transform.position = _roomBounds.center;
    _virtCam.transform.position = _roomBounds.center;
  }

  void Start()
  {
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
  }

  // Update is called once per frame
  void Update()
  {

  }
}
