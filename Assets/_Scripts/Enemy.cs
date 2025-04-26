using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Range(0f, 360f)]
    public float MoveAngle = 0f;
    public float MoveSpeed = 4f;
    SpriteRenderer _sr;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        if(_sr == null)
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
        transform.Translate(Vector3.right * MoveSpeed * Time.deltaTime,Space.Self);
        if(Vector3.Angle(Vector3.right, transform.right) > 90f)
        {
            //Entity is moving to the left
            if(_sr.bounds.max.x < EnemyManager.Instance.Bounds.min.x)
            {
                Destroy(gameObject);
            }
        }
        else{
            //Entity is moving to the right
            if(_sr.bounds.min.x > EnemyManager.Instance.Bounds.max.x)
            {
                Destroy(gameObject);
            }
        }
        if(Vector3.Angle(Vector3.up, transform.right) > 90f)
        {
            //Entity is moving down
            if(_sr.bounds.max.y < EnemyManager.Instance.Bounds.min.y)
            {
                Destroy(gameObject);
            }
        }
        else{
            //Entity is moving up
            if(_sr.bounds.min.y > EnemyManager.Instance.Bounds.max.y)
            {
                Destroy(gameObject);
            }
        }
    }
}
