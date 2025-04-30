using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public Vector3 Position => transform.position;
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
    }
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Player collided with " + collision.gameObject.name);
    }
}
