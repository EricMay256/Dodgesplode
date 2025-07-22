using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using TMPro.EditorUtilities;

namespace BearFalls
{
  public class Player : MonoBehaviour
  {
    #region Declarations
    public static Player Instance;
    [SerializeField]
    private PlayerData _pd;
    Rigidbody2D _rb;
    public Vector3 Position => _rb.position;
    Vector3 _moveDelta;
    const float _doorMoveAmount = 3f;
    float _invulnTimer = 0f;
    SpriteRenderer _sr;

    [SerializeField]
    MovementInputType _movementInputType = MovementInputType.Look;

    [field: SerializeField] public float moveSpeed = 10f;//Universal and constant speed multiplier for all forms of input
    private float _moveScale = 1f;//Universal and variable speed multiplier for all forms of input
                                  // Start is called once before the first execution of Update after the MonoBehaviour is created
    #endregion
    #region Monobehaviours
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

      _rb = GetComponent<Rigidbody2D>();
      _pd.CurHealth = _pd.MaxHealth;
      _sr = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
      ResetPlayer();
    }

    void FixedUpdate()
    {
      _rb.MovePosition(new Vector3(_rb.position.x, _rb.position.y, transform.position.z)
       + new Vector3(_moveDelta.x, _moveDelta.y, 0)
       * Time.fixedDeltaTime * _moveScale * moveSpeed);
    }

    // Update is called once per frame
    void Update()
    {
      if (_invulnTimer < _pd.InvulnPeriod)
        _invulnTimer += Time.deltaTime;
      ApplyHealthRegen(Time.deltaTime);
      //Apply scaling to input motion based on crouch/sprint
      CheckMovementMultiplier();
      //Movement via mouse delta or left joystick applied to player position 
      _moveDelta = _movementInputType == MovementInputType.Move ?
      PlayerInputManager.Instance.Movement :
      PlayerInputManager.Instance.LookDelta;
      //Todo: Consider modifying how timescale is applied to player movement
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
      if (collision.CompareTag("Door") && GameManager.Instance.CurrentGameState == GameState.Active)
      {
        Door door = collision.GetComponent<Door>();
        if (door != null)
        {
          Debug.Log("Player collided with door: " + collision.gameObject.name);
          door.TravelThroughDoor();
        }
        else
        {
          Debug.LogError("Door component not found on the collided object!");
        }
      }
      if (collision.CompareTag("Enemy"))
      {
        var enemy = collision.GetComponent<Enemy>();
        TakeDamage(enemy.Damage);
      }
      if (collision.CompareTag("EnemyTrigger"))
      {
        collision.GetComponent<TriggerArea>().StartTriggered();
        Debug.Log("EnemyTrigger detected");
      }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
      if (collision.CompareTag("EnemyTrigger"))
      {
        collision.GetComponent<TriggerArea>().StopTriggered();
      }
    }
    #endregion

    #region Coroutines
    IEnumerator InvulnFlashing()
    {
      int flashes = 0;
      _sr.color = Color.red;
      flashes++;
      while (_invulnTimer < _pd.InvulnPeriod)
      {
        yield return new WaitForSeconds(0.2f);
        _sr.color = _sr.color == Color.red ? Color.white : Color.red;
        flashes++;
      }
      _sr.color = Color.white;
    }

    IEnumerator DoorMovementCoroutine(Vector2 targetPosition, Room previousRoom, float duration = 2f)
    {
      Vector2 startPosition = _rb.position;
      Debug.Log("Starting door transition from " + startPosition + " to " + targetPosition);
      float elapsedTime = 0f;
      _rb.bodyType = RigidbodyType2D.Kinematic; // Re-enable physics after transition

      while (elapsedTime < duration)
      {
        _rb.position = Vector2.Lerp(startPosition, targetPosition, elapsedTime / duration);
        elapsedTime += Time.unscaledDeltaTime;
        yield return null;
      }
      _rb.position = targetPosition;
      previousRoom.DeactivateRoom();
      _rb.bodyType = RigidbodyType2D.Dynamic; // Re-enable physics after transition
      GameManager.Instance.EndTransition();
    }
    #endregion

    #region Helper Methods
    //Apply health regen over time
    void ApplyHealthRegen(float time)
    {
      if (GameManager.Instance.CurrentGameState == GameState.Active && _pd.CurHealth < _pd.MaxHealth)
      {
        _pd.CurHealth += _pd.HealthRegenPer5Sec * Time.deltaTime / 5f;
        _pd.CurHealth = Mathf.Min(_pd.CurHealth, _pd.MaxHealth);
        _pd.HealthPct = _pd.CurHealth / _pd.MaxHealth;
      }
    }

    void TakeDamage(float damage = 10f)
    {
      if (_invulnTimer < _pd.InvulnPeriod)
        return;
      _pd.CurHealth -= damage;
      _pd.HealthPct = Mathf.Max(_pd.CurHealth
        / _pd.MaxHealth, 0f);
      _invulnTimer = 0f;
      if (_pd.CurHealth <= 0f)
      {
        GameManager.Instance.GameOver();
      }
      else
      {
        StartCoroutine("InvulnFlashing");
      }
    }
    #endregion
    #region Public Methods
    public void CheckMovementMultiplier()
    {
      if (PlayerInputManager.Instance.CrouchHeld)
      {
        _moveScale = 0.25f;
      }
      else if (PlayerInputManager.Instance.SprintHeld)
      {
        _moveScale = 2f;
      }
      else
      {
        _moveScale = 1f;
      }
    }


    public void DoorMotion(Direction doorDirection, Room previousRoom)
    {
      Vector3 targetPosition = transform.position;
      switch (doorDirection)
      {
        case Direction.Top:
          targetPosition += Vector3.up * _doorMoveAmount;
          break;
        case Direction.Right:
          targetPosition += Vector3.right * _doorMoveAmount;
          break;
        case Direction.Bottom:
          targetPosition += Vector3.down * _doorMoveAmount;
          break;
        case Direction.Left:
          targetPosition += Vector3.left * _doorMoveAmount;
          break;
        default:
          Debug.LogError("Invalid door direction specified!");
          break;
      }
      StartCoroutine(DoorMovementCoroutine(targetPosition, previousRoom));
    }

    public void ResetPlayer()
    {
      _pd.ResetData();
      //Todo? : Reload starting room?
      transform.position = RoomManager.Instance.RoomBounds.center;
    }
    #endregion
  }
}