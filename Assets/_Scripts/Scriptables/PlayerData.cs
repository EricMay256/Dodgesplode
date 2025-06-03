using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
  public float healthPct = 1f;
  public float maxHealth = 100f;
  public float curHealth = 100f;
  public float healthRegenPer5Sec = 5f;

  public void ResetData()
  {
    healthPct = 1f;
    curHealth = maxHealth;
  }

  public void ResetAllDefaults()
  {
    PlayerData defaultData = ScriptableObject.CreateInstance<PlayerData>();
    healthPct = defaultData.healthPct;
    maxHealth = defaultData.maxHealth;
    curHealth = defaultData.curHealth;
    healthRegenPer5Sec = defaultData.healthRegenPer5Sec;
  }
}
