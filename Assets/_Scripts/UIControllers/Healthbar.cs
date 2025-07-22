using UnityEngine;
using UnityEngine.UI;

namespace BearFalls
{
  public class Healthbar : MonoBehaviour
  {
    #region Declarations
    [SerializeField]
    PlayerData _playerData;
    Slider _healthbarSlider;
    float _previousHealthPercent = 1f;
    #endregion
    #region Monobehaviours
    void Awake()
    {
      _healthbarSlider = GetComponent<Slider>();
      if (_healthbarSlider == null)
      {
        Debug.LogError("Healthbar slider not found on the GameObject.");
        enabled = false; // Disable this script if dependencies are not found
      }
    }
    void Start()
    {
      _previousHealthPercent = _playerData.HealthPct;
      _healthbarSlider.value = _previousHealthPercent;
    }
    // Update is called once per frame
    void Update()
    {
      if (Mathf.Abs(_playerData.HealthPct - _previousHealthPercent) > Mathf.Epsilon)
      {
        _previousHealthPercent = _playerData.HealthPct;
        _healthbarSlider.value = _previousHealthPercent;
      }
    }
    #endregion
  }
}