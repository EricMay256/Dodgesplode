using UnityEngine;

enum MovementInputType
{
  Look = 0,
  Move = 1
}
public class Player : MonoBehaviour
{
  public static Player Instance;
  [SerializeField]
  private PlayerData _pd;
  Rigidbody2D _rb;
  public Vector3 Position => _rb.position;
  Vector3 _moveDelta;

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
    _rb = GetComponent<Rigidbody2D>();
    _pd.curHealth = _pd.maxHealth;
  }
  void Start()
  {
    ResetPlayer();
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
    _moveDelta = _movementInputType == MovementInputType.Move ?
    PlayerInputManager.Instance.Movement :
    PlayerInputManager.Instance.LookDelta;
    //Todo: Consider modifying how timescale is applied to player movement
  }

  void FixedUpdate()
  {
    _rb.MovePosition(new Vector3(_rb.position.x, _rb.position.y, transform.position.z)
     + new Vector3(_moveDelta.x, _moveDelta.y, 0)
     * Time.fixedDeltaTime * _moveScale * moveSpeed) ;

  }

  //Apply health regen over time
  void ApplyHealthRegen(float time)
  {
    if (_pd.curHealth < _pd.maxHealth)
    {
      _pd.curHealth += _pd.healthRegenPer5Sec * Time.deltaTime / 5f;
      _pd.curHealth = Mathf.Min(_pd.curHealth, _pd.maxHealth);
      _pd.healthPct = _pd.curHealth / _pd.maxHealth;
    }
  }

  public void TakeDamage(float damage = 10f)
  {
    _pd.curHealth -= damage;
    _pd.healthPct = Mathf.Max(_pd.curHealth
      / _pd.maxHealth, 0f);
    if (_pd.curHealth <= 0f)
    {
      GameManager.Instance.GameOver();
    }
  }

  public void ResetPlayer()
  {
    _pd.ResetData();
    transform.position = Vector3.zero;
  }

  void OnTriggerEnter2D(Collider2D collision)
  {
    TakeDamage();
    //Debug.Log("Player collided with " + collision.gameObject.name);
  }
}
