using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    
    [SerializeField]
    GameObject _projectilePrefab;
    public GameObject ProjectilePrefab => _projectilePrefab;
    [SerializeField]
    Transform _projectileParent;
    public Transform ProjectileParent => _projectileParent;

    public static ProjectileManager Instance { get; private set; }
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
