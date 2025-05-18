using UnityEngine;

enum MovementInputType
{
  Look = 0,
  Move = 1
}
public class Player : MonoBehaviour, IHealthbarSource
{
  public static Player Instance;
  public Vector3 Position => transform.position;
  private float _maxHealth = 100f, _health, _healthRegenPer5Sec = 5f;
  public float Health => _health;
  public float MaxHealth => _maxHealth;
  public float HealthPercent => _health / _maxHealth;
  public float HealthPercentNormalized => Mathf.Clamp01(_health / _maxHealth);

  [SerializeField]
  MovementInputType _movementInputType = MovementInputType.Look;
  Camera _mainCam;
  Bounds _camBounds;

  [field: SerializeField] public float moveSpeed = 10f;//Universal and constant speed multiplier for all forms of input
  private float _moveScale = 1f;//Universal and variable speed multiplier for all forms of input
                                // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Awake()
  {
    if (Instance == null)
    {
      Instance = this;
    }
    else
    {
      Destroy(gameObject);
    }

    _mainCam = Camera.main;
    if (_mainCam == null)
    {
      Debug.LogError("No main camera found!");
    }
    else
    {
      _camBounds = new Bounds(_mainCam.transform.position, _mainCam.GetComponent<Camera>().orthographicSize * 2f * new Vector3(_mainCam.aspect, 1));
    }

    _health = _maxHealth;
  }
  void Start()
  {
    transform.position = new Vector3(0, 0, 0);
  }

  // Update is called once per frame
  void Update()
  {
    ApplyHealthRegen(Time.deltaTime);
    //Apply scaling to input motion based on crouch/sprint
    if (PlayerInputManager.Instance.CrouchPressed)
    {
      //Apply processor scaling movement down
      _moveScale = 0.25f;
      //PlayerInputManager.Instance.SetLookScale(_moveScale);
    }
    else if (PlayerInputManager.Instance.CrouchReleased)
    {
      _moveScale = 1f;
      //PlayerInputManager.Instance.SetLookScale(_moveScale);
    }
    else if (PlayerInputManager.Instance.SprintPressed)
    {
      _moveScale = 2f;
      //PlayerInputManager.Instance.SetLookScale(_moveScale);
    }
    else if (PlayerInputManager.Instance.SprintReleased)
    {
      _moveScale = 1f;
      //PlayerInputManager.Instance.SetLookScale(_moveScale);
    }
    //Movement via mouse delta or left joystick applied to player position 
    Vector2 moveDelta = _movementInputType == MovementInputType.Move ? PlayerInputManager.Instance.Movement : PlayerInputManager.Instance.LookDelta;
    //Todo: Consider modifying how timescale is applied to player movement
    transform.position += new Vector3(moveDelta.x, moveDelta.y, 0) * Time.deltaTime * _moveScale * moveSpeed * Time.timeScale; // */
  }

  //Apply health regen over time
  void ApplyHealthRegen(float time)
  {
    if (_health < _maxHealth)
    {
      _health += _healthRegenPer5Sec * Time.deltaTime / 5f;
      if (_health > _maxHealth)
      {
        _health = _maxHealth;
      }
    }
  }

  public void TakeDamage(float damage = 10f)
  {
    _health -= damage;
    if (_health <= 0f)
    {
      GameManager.Instance.GameOver();
    }
  }

  public void ResetPlayer()
  {
    _health = _maxHealth;
    transform.position = Vector3.zero;
  }

  void OnTriggerEnter2D(Collider2D collision)
  {
    TakeDamage();
    //Debug.Log("Player collided with " + collision.gameObject.name);
  }
}
