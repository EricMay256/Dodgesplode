using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
  public float HealthPct = 1f;
  public float MaxHealth = 100f;
  public float CurHealth = 100f;
  public float HealthRegenPer5Sec = 5f;
  public float InvulnPeriod = 0.4f;

  public void ResetData()
  {
    HealthPct = 1f;
    CurHealth = MaxHealth;
  }

  public void ResetAllDefaults()
  {
    PlayerData defaultData = ScriptableObject.CreateInstance<PlayerData>();
    HealthPct = defaultData.HealthPct;
    MaxHealth = defaultData.MaxHealth;
    CurHealth = defaultData.CurHealth;
    HealthRegenPer5Sec = defaultData.HealthRegenPer5Sec;
    InvulnPeriod = defaultData.InvulnPeriod;
  }
}
