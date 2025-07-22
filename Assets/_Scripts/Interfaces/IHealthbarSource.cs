using UnityEngine;

namespace BearFalls
{
  public interface IHealthbarSource
  {
    float Health { get; }
    float MaxHealth { get; }
    float HealthPercent { get; }
    float HealthPercentNormalized { get; }
  }
}