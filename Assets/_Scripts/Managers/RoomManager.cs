using UnityEngine;

public class RoomManager : MonoBehaviour
{
  public static RoomManager Instance;
  public RoomData RoomData => _roomData;
  public Bounds CameraBounds => _roomData._cameraBounds;

  RoomData _roomData;
  private GameObject _currentRoomPrefab;


  void LoadRoom(RoomData roomData)
  {
    if (roomData == null)
    {
      Debug.LogError("Room data is null!");
      return;
    }
    if (_currentRoomPrefab != null)
    {
      Destroy(_currentRoomPrefab);
    }
    _roomData = roomData;
    // Load the room prefab and set up the camera bounds, music, etc.
    _currentRoomPrefab = Instantiate(_roomData._roomPrefab);

    //Camera.main.GetComponent<Camera>().bounds = _roomData._cameraBounds;

    // Play room music if available
    if (_roomData._roomMusic != null)
    {
      AudioSource audioSource = Camera.main.GetComponent<AudioSource>();
      if (audioSource != null)
      {
        audioSource.clip = _roomData._roomMusic;
        audioSource.Play();
      }
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

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }
}
