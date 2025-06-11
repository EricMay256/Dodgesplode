using Unity.Cinemachine;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
  public static RoomManager Instance;
  public RoomData RoomData => _roomData;

  RoomData _roomData;
  private GameObject _currentRoomPrefab;
  CinemachineCamera _virtCam;

  [SerializeField]
  RoomData _startingRoom, _debugRoom;


  [ContextMenu("Set Debug Room")]
  public void SetDebugRoom()
  {
    if (_debugRoom == null) return;
    LoadRoom(_debugRoom);
  }

  void LoadRoom(RoomData roomData)
  {
    if (roomData == null)
    {
      Debug.LogError("Room data is null!");
      return;
    }
    GameManager.Instance.StartTransition();
    if (_currentRoomPrefab != null)
    {
      Destroy(_currentRoomPrefab, 2);
    }
    _roomData = roomData;
    // Load the room prefab and set up the camera bounds, music, etc.
    _currentRoomPrefab = Instantiate(_roomData._roomPrefab);
    _virtCam = _currentRoomPrefab.GetComponentInChildren<CinemachineCamera>();
    if (_virtCam == null)
    {
      Debug.LogError("CinemachineCamera component not found in children!");
    }
    _virtCam.Target.TrackingTarget = Player.Instance.transform;

    //Camera.main.GetComponent<Camera>().bounds = _roomData._cameraBounds;

    // Play room music if available
    AudioManager.Instance.PlayMusic(_roomData._roomMusic);
    EnemyManager.Instance.UpdateSpawnList(_roomData._enemySpawnList.EnemySpawns);
    GameManager.Instance.EndTransition();
  }

  void Start()
  {
    if (_startingRoom != null)
    {
      LoadRoom(_startingRoom);
    }
    else
    {
      Debug.LogError("Starting room data is not set!");
    }
  }

  void Awake()
  {
    if (Instance == null)
    {
      Instance = this;
      DontDestroyOnLoad(gameObject);
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
