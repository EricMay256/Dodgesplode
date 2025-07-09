using UnityEngine;

public class TrackingMover : Enemy
{
  #region Declarations
  [Range(0f, 360f)]
  private float MoveAngle = 0f;
  private float MoveSpeed = 4f;
  /// <summary>
  /// Degrees per second to change angle towards target.
  /// </summary>
  private float AngleChangeRate = 20f;
  private float ChaseDuration = -1f; // -1 means infinite chase duration
  #endregion
  #region Public Methods
  public override void SetUpEnemy(EnemyLevelStats levelStats)
  {
    base.SetUpEnemy(levelStats); // sets base _speedModifier, available through base SpeedModifier property
    MoveSpeed *= SpeedModifier;

    ChaseDuration = levelStats.ChaseDuration;
    AngleChangeRate *= levelStats.AngleChangeRateMulti;
  }

  public override void PlaceOnSpawningBounds(Direction edge)
  {
    base.PlaceOnSpawningBounds(edge);
    UpdateAngle();
  }
  public override void PlaceOnSpawningBounds()
  {
    base.PlaceOnSpawningBounds();
    UpdateAngle();
  }
  public override void PlaceClosestToPlayer()
  {
    base.PlaceClosestToPlayer();
    UpdateAngle();
  }
  public override void PlaceClosestToPlayer(Direction edge)
  {
    base.PlaceClosestToPlayer(edge);
    UpdateAngle();
  }
  #endregion
  #region Helper Methods
  void UpdateAngle()
  {
    // Set the rotation based on the spawned edge
    switch (_spawnedEdge)
    {
      case Direction.Right:
        MoveAngle = 180f;
        break;
      case Direction.Top:
        MoveAngle = 270f;
        break;
      case Direction.Left:
        MoveAngle = 0f;
        break;
      case Direction.Bottom:
        MoveAngle = 90f;
        break;
    }
    transform.rotation = Quaternion.Euler(0f, 0f, MoveAngle);
  }
  #endregion
  #region Monobehaviours
  void Awake()
  {
    _sr = GetComponent<SpriteRenderer>();
  }

  // Update is called once per frame
  protected override void Update()
  {
    base.Update();
    if (_timeAlive < ChaseDuration || ChaseDuration < 0f)
    {
      Vector3 targetPosition = Player.Instance.Position;
      Vector3 directionToTarget = (targetPosition - transform.position).normalized;

      // Calculate the angle to the target
      float targetAngle = Vector3.SignedAngle(Vector3.right, directionToTarget, Vector3.forward);

      // Smoothly change the angle towards the target
      MoveAngle = Mathf.MoveTowardsAngle(MoveAngle, targetAngle, AngleChangeRate * Time.deltaTime);

      // Set the rotation
      transform.rotation = Quaternion.Euler(0f, 0f, MoveAngle);
    }
    // Move towards the target
    transform.Translate(Vector3.right * MoveSpeed * Time.deltaTime, Space.Self);
  }
  #endregion
}
