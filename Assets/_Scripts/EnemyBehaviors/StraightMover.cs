using UnityEngine;

public class StraightMover : Enemy
{
  [Range(0f, 360f)]
  public float MoveAngle = 0f;
  public float MoveSpeed = 4f;
  [SerializeField] AimType _aimType = AimType.Random;
  /// <summary>
  /// Random offset from the MoveAngle for Aimed or Orthogonal enemies.
  /// </summary>
  [SerializeField] float _aimArc = 5f;
  public override void SetUpEnemy(float speedModifier = 1f, float scale = 1f)
  {
    base.SetUpEnemy(speedModifier, scale);//sets base _speedModifier, available through base SpeedModifier property
    MoveSpeed *= SpeedModifier;

    PlaceOnSpawningBounds();
    if (_aimType == AimType.Aimed)//Aimed logic doesn't depend on the spawned edge
    {
      MoveAngle = Vector3.SignedAngle(Vector3.right,
      Player.Instance.Position - transform.position, Vector3.forward) + Random.Range(-_aimArc, _aimArc);
    }
    // Set the rotation based on the spawned edge
    switch (_spawnedEdge)
    {
      case Direction.Right:
        _sr.color = Color.red;
        switch (_aimType)
        {
          case AimType.Orthogonal:
            MoveAngle = 180f + Random.Range(-_aimArc, _aimArc);
            break;
          case AimType.Random:
            MoveAngle = Random.Range(90f, 270f);
            break;
          default:
            break;
        }
        break;
      case Direction.Top:
        _sr.color = Color.green;
        switch (_aimType)
        {
          case AimType.Orthogonal:
            MoveAngle = 270f + Random.Range(-_aimArc, _aimArc);
            break;
          case AimType.Random:
            MoveAngle = Random.Range(180f, 360f);
            break;
          default:
            break;
        }
        break;
      case Direction.Left:
        _sr.color = Color.blue;
        switch (_aimType)
        {
          case AimType.Orthogonal:
            MoveAngle = 0f + Random.Range(-_aimArc, _aimArc);
            break;
          case AimType.Random:
            MoveAngle = Random.Range(-90f, 90f) % 360f;
            break;
          default:
            break;
        }
        break;
      case Direction.Bottom:
        _sr.color = Color.yellow;
        switch (_aimType)
        {
          case AimType.Orthogonal:
            MoveAngle = 90f + Random.Range(-_aimArc, _aimArc);
            break;
          case AimType.Random:
            MoveAngle = Random.Range(0f, 180f);
            break;
          default:
            break;
        }
        break;
    }
    transform.rotation = Quaternion.Euler(0f, 0f, MoveAngle);
  }

  void DestroyPastBounds()
  {
    if (Vector3.Angle(Vector3.right, transform.right) > 90f)
    {
      //Entity is moving to the left
      if (_sr.bounds.max.x < RoomManager.Instance.RoomBounds.min.x)
      {
        DestroyEnemy();
      }
    }
    else
    {
      //Entity is moving to the right
      if (_sr.bounds.min.x > RoomManager.Instance.RoomBounds.max.x)
      {
        DestroyEnemy();
      }
    }
    if (Vector3.Angle(Vector3.up, transform.right) > 90f)
    {
      //Entity is moving down
      if (_sr.bounds.max.y < RoomManager.Instance.RoomBounds.min.y)
      {
        DestroyEnemy();
      }
    }
    else
    {
      //Entity is moving up
      if (_sr.bounds.min.y > RoomManager.Instance.RoomBounds.max.y)
      {
        DestroyEnemy();
      }
    }
  }

  void Awake()
  {
    _sr = GetComponent<SpriteRenderer>();
    if (_sr == null)
    {
      Debug.LogError("No SpriteRenderer found on this GameObject.");
    }
  }

  void Start()
  {
  }

  // Update is called once per frame
  void Update()
  {
    transform.Translate(Vector3.right * MoveSpeed * Time.deltaTime, Space.Self);
    DestroyPastBounds();
  }
}
