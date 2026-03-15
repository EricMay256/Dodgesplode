using UnityEngine;

namespace UBear.Combat
{
public class RangedWeapon : MonoBehaviour, IWeapon
{
  [SerializeField]
  private RangedWeaponObject _weaponObject;
  int nextFiringPointIndex = -1;//Initialized at -1 so that incrementing prior to access starts us at index 0

  void Awake()
  {
    if (_weaponObject == null)
    {
      Debug.LogError("RangedWeapon component on " + gameObject.name + " is missing a reference to a RangedWeaponObject ScriptableObject.");
    }
  }
  void FiringPointActivation(FiringPoint firingPoint, Vector3 targetDirection)
  {
    switch (firingPoint.FirePattern)
    {
      case FireCoordinationPattern.Sequential:
      firingPoint.nextProjectileIndex = firingPoint.nextProjectileIndex + 1 % firingPoint.Projectiles.Count;
      
      Instantiate(firingPoint.Projectiles[firingPoint.nextProjectileIndex].Prefab, 
      transform.TransformPoint(firingPoint.Position), 
      Quaternion.Euler(0,0,Mathf.Atan2(targetDirection.y,targetDirection.x)*Mathf.Rad2Deg));
      break;
      case FireCoordinationPattern.AllTogether:
      foreach (var proj in firingPoint.Projectiles)
      {
        Instantiate(proj.Prefab, 
        transform.TransformPoint(firingPoint.Position), 
        Quaternion.Euler(0,0,Mathf.Atan2(targetDirection.y,targetDirection.x)*Mathf.Rad2Deg));
      }
      break;
      case FireCoordinationPattern.Random:
      var projectile = firingPoint.Projectiles[Random.Range(0,firingPoint.Projectiles.Count)];
      Instantiate(projectile.Prefab,
      transform.TransformPoint(firingPoint.Position),
      Quaternion.Euler(0,0,Mathf.Atan2(targetDirection.y,targetDirection.x)*Mathf.Rad2Deg));
      break;
      case FireCoordinationPattern.Combo:
      Debug.LogError("Combo firing behavior for projectiles on firing points not yet implemented");
      //StartCoroutine(ComboFire(firingPoint, targetDirection));
      break;
    }
  }
  #region Attack Overrides
  public void Attack(Vector3 direction)
  {
    switch(_weaponObject.FirepointPattern)
    {
      case FireCoordinationPattern.Sequential:
      nextFiringPointIndex = nextFiringPointIndex + 1 % _weaponObject.FiringPoints.Count;
      FiringPointActivation(_weaponObject.FiringPoints[nextFiringPointIndex],direction);
      break;

      case FireCoordinationPattern.AllTogether:
      foreach (var fp in _weaponObject.FiringPoints)
      {
        FiringPointActivation(fp, direction);
      }
      break;

      case FireCoordinationPattern.Random:
      var firingPoint = 
        _weaponObject.FiringPoints[Random.Range(0,_weaponObject.FiringPoints.Count)];
      FiringPointActivation(firingPoint, direction);

      break;

      case FireCoordinationPattern.Combo:
      Debug.LogError("Combo firing behavior for firing points not yet implemented");
      break;
    }
  }

  public void Attack()
  {
    Attack(Vector3.right);
  }
  #endregion
}
}