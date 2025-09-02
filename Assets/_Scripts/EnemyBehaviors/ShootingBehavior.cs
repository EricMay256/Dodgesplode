using System.Collections.Generic;
//using System.Numerics;
using BearFalls;
using UnityEngine;

public class ShootingBehavior : MonoBehaviour
{
    List<Vector3> _shootingOffsets = new List<Vector3>()
    {
        Vector3.zero
    };
    [SerializeField] Projectile _projectile;
    [SerializeField] FiringSequence _firingSequence = FiringSequence.Simultaneous;
    private float _timeBetweenShots = 1f;
    private float _spreadAngle = 0f;
    private float _shotSpeedMultiplier = 1f;
    private float _activeFiringRadius = 5f;

    private float _shotTimer = 0f;
    private int _nextSequentialShootingOffsetIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.CurrentGameState == GameState.Active)
        {
            _shotTimer += Time.deltaTime;
            if (_shotTimer >= _timeBetweenShots &&
                Vector2.Distance(transform.position, Player.Instance.transform.position) <= _activeFiringRadius)
            {
                Shoot();
                _shotTimer = 0f;
            }
        }
    }

    void Shoot()
    {
        //Todo: Implement object pooling
        switch (_firingSequence)
        {
            case FiringSequence.Simultaneous:
                foreach (var offset in _shootingOffsets)
                {
                    FireBullet(Random.Range(-_spreadAngle, _spreadAngle), offset);
                }
                break;
            case FiringSequence.Sequential:
                FireBullet(Random.Range(-_spreadAngle, _spreadAngle), _shootingOffsets[_nextSequentialShootingOffsetIndex++]);
                _nextSequentialShootingOffsetIndex %= _shootingOffsets.Count;
                break;
            case FiringSequence.Random:
                FireBullet(Random.Range(-_spreadAngle, _spreadAngle), _shootingOffsets[Random.Range(0, _shootingOffsets.Count)]);
                break;
            default:
                break;
        }
    }
    void FireBullet(float angleOffset, Vector3 offset)
    {
        GameObject obj = Instantiate(ProjectileManager.Instance.ProjectilePrefab, transform);
        obj.transform.localPosition = offset;
        obj.transform.SetParent(ProjectileManager.Instance.ProjectileParent);//Todo: replace with bullet parent from ProjectileManager
        obj.transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.right, Player.Instance.transform.position - obj.transform.position) + angleOffset);
        obj.transform.localScale = Vector3.one * _projectile.scale;
    }
}
