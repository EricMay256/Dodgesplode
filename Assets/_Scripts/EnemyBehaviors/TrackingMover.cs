using UnityEngine;

public class TrackingMover : Enemy
{
  [Range(0f, 360f)]
  private float MoveAngle = 0f;
  private float MoveSpeed = 4f;
  /// <summary>
  /// Degrees per second to change angle towards target.
  /// </summary>
  private float AngleChangeRate = 20f;
  private float ChaseDuration = -1f; // -1 means infinite chase duration
  private float LifeSpan = -1f; // Time after which the enemy will be destroyed if not destroyed earlier. -1 means infinite lifespan
  private float _timeAlive = 0f; // Timer to track chase duration

  public override void SetUpEnemy(EnemyLevelStats levelStats)

  {
    base.SetUpEnemy(levelStats); // sets base _speedModifier, available through base SpeedModifier property
    MoveSpeed *= SpeedModifier;
    
    ChaseDuration = levelStats.ChaseDuration;
    LifeSpan = levelStats.LifeSpan;
    AngleChangeRate *= levelStats.AngleChangeRateMulti;
    PlaceOnSpawningBounds();
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

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {

  }

  void Awake()
  {
    _sr = GetComponent<SpriteRenderer>();
  }

  // Update is called once per frame
  void Update()
  {
    _timeAlive += Time.deltaTime;
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
    if( LifeSpan > 0f && _timeAlive >= LifeSpan)
    {
      DestroyEnemy();
      return;
    }
    // Move towards the target
    transform.Translate(Vector3.right * MoveSpeed * Time.deltaTime, Space.Self);
  }
}
