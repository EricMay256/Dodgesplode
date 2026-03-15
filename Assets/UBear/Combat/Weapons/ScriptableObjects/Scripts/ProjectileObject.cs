using UnityEngine;

[CreateAssetMenu(fileName = "New Projectile", menuName = "UBear/Combat/Weapons/Projectile")]
public class ProjectileObject : ScriptableObject
{
  public GameObject Prefab;
  public float ComboTime = .1f;
  public bool DestroyOnCollision = true;
}
