using UnityEngine;

namespace UBear.Combat
{
[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private float _speed = 5f;
    /// <summary>
    /// Use a value of 0 for infinite lifetime or to destroy only via other means
    /// </summary>
    private float _lifetime = 10f;

    void Awake()
    {
      Debug.Assert(_lifetime >= 0, "Lifetime must be non-negative");
      Debug.Assert(_speed >= 0, "Speed must be non-negative");
      _rigidbody2D = GetComponent<Rigidbody2D>();
      Debug.Assert(_rigidbody2D != null, "Rigidbody2D component is required");
      if(_lifetime > 0)
      {
        Destroy(gameObject, _lifetime);
      }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      _rigidbody2D.MovePosition(transform.position + transform.right * _speed * Time.fixedDeltaTime);
    }
}
}