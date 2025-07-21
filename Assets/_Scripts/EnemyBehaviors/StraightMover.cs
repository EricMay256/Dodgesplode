using UnityEngine;

public class StraightMover : Enemy
{
  #region Declarations
  [Range(0f, 360f)]
  private float MoveAngle = 0f;
  private float MoveSpeed = 4f;
  /// <summary>
  /// Random offset from the MoveAngle for Aimed or Orthogonal enemies.
  /// </summary>
  [SerializeField] float _aimArc = 5f;
  #endregion
  #region Public Methods
  public override void SetUpEnemy(EnemyLevelStats levelStats)
  {
    base.SetUpEnemy(levelStats);//sets base _speedModifier, available through base SpeedModifier property
    MoveSpeed *= SpeedModifier;
  }

  public override void PlaceOnSpawningBounds()
  {
    base.PlaceOnSpawningBounds();
    UpdateAngleAndColor();
  }

  public override void PlaceOnSpawningBounds(Direction edge)
  {
    base.PlaceOnSpawningBounds(edge);
    UpdateAngleAndColor();
  }
  public override void PlaceClosestToPlayer()
  {
    base.PlaceClosestToPlayer();
    UpdateAngleAndColor();
  }
  public override void PlaceClosestToPlayer(Direction edge)
  {
    base.PlaceClosestToPlayer(edge);
    UpdateAngleAndColor();
  }
  #endregion
  #region Helper Methods
  void UpdateAngleAndColor()
  {
    if (AimType == AimType.Aimed)//Aimed logic doesn't depend on the spawned edge
    {
      MoveAngle = Vector3.SignedAngle(Vector3.right,
      Player.Instance.Position - transform.position, Vector3.forward) + Random.Range(-_aimArc, _aimArc);
    }
    // Set the rotation based on the spawned edge
    switch (_spawnedEdge)
    {
      case Direction.Right:
        _sr.color = Color.red;
        switch (AimType)
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
        switch (AimType)
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
        switch (AimType)
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
        switch (AimType)
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
  #endregion
  #region Monobehaviours
  void Awake()
  {
    _sr = GetComponent<SpriteRenderer>();
    if (_sr == null)
    {
      Debug.LogError("No SpriteRenderer found on this GameObject.");
    }
  }

  // Update is called once per frame
  protected override void Update()
  {
    base.Update();
    transform.Translate(Vector3.right * MoveSpeed * Time.deltaTime, Space.Self);
    DestroyPastBounds();
  }
  #endregion
}
