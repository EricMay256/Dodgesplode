using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public Vector3 Position => transform.position;
    [field:SerializeField] public float moveSpeed = 10f;
    Camera _camera;
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
        _camera = Camera.main;
    }
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
        //Apply mouse movement to player position
         //Movement via mouse delta applied to player position 
        Vector2 delta = PlayerInputManager.Instance.LookDelta;
        Vector2 screensize = new Vector2(Screen.width, Screen.height);
        transform.position += new Vector3(delta.x / screensize.x, delta.y / screensize.y, 0) * Time.deltaTime * moveSpeed; // */
        
        /* //Movement directly tracking mouse position
        Vector3 pos = _camera.ScreenToWorldPoint(Input.mousePosition); 
        pos.z = 0;
        transform.position = pos;// */
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Player collided with " + collision.gameObject.name);
    }
}
