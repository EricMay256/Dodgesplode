using UnityEngine;

public class Player : MonoBehaviour, IHealthbarSource
{
    public static Player Instance;
    public Vector3 Position => transform.position;
    private float _maxHealth = 100f, _health, _healthRegenPer5Sec = 5f;
    public float Health => _health;
    public float MaxHealth => _maxHealth;
    public float HealthPercent => _health / _maxHealth;
    public float HealthPercentNormalized => Mathf.Clamp01(_health / _maxHealth);
    
    [SerializeField] Timer _timer;
    [field:SerializeField] public float moveSpeed = 10f;//Universal and constant speed multiplier for all forms of input
    private float _moveScale = 1f;//Universal and variable speed multiplier for all forms of input
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
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
        if(PlayerInputManager.Instance.CrouchPressed)
        {
            //Apply processor scaling movement down
            _moveScale = 0.25f;
            PlayerInputManager.Instance.SetLookScale(_moveScale);
        }
        else if(PlayerInputManager.Instance.CrouchReleased)
        {
            _moveScale = 1f;
            PlayerInputManager.Instance.SetLookScale(_moveScale);
        }
        else if(PlayerInputManager.Instance.SprintPressed)
        {
            _moveScale = 2f;
            PlayerInputManager.Instance.SetLookScale(_moveScale);
        }
        else if(PlayerInputManager.Instance.SprintReleased)
        {
            _moveScale = 1f;
            PlayerInputManager.Instance.SetLookScale(_moveScale);
        }
        //Movement via mouse delta or left joystick applied to player position 
        Vector2 delta = PlayerInputManager.Instance.LookDelta;
        transform.position += new Vector3(delta.x, delta.y, 0) * Time.deltaTime * moveSpeed; // */
    }

    //Apply health regen over time
    void ApplyHealthRegen(float time)
    {
        if(_health < _maxHealth)
        {
            _health += _healthRegenPer5Sec * Time.deltaTime / 5f;
            if(_health > _maxHealth)
            {
                _health = _maxHealth;
            }
        }
    }

    public void TakeDamage(float damage = 10f)
    {
        _health -= damage;
        if(_health <= 0f)
        {
            if(_timer != null)
            {
                Debug.Log($"Player is dead after {_timer.TimeElapsed.ToString("0")} seconds!");
            }
            else
            {
            Debug.Log("Player is dead! (No timer found)");
            }
            //Player is dead
            //PlayerInputManager.Instance.SetGameOver(true);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        TakeDamage();
        //Debug.Log("Player collided with " + collision.gameObject.name);
    }
}
