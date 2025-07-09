using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField]
    PlayerData _playerData;
    Slider _healthbarSlider;
    float _previousHealthPercent = 1f;
    void Awake()
    {
        _healthbarSlider = GetComponent<Slider>();
        if(_healthbarSlider == null)
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
        if(_playerData.HealthPct != _previousHealthPercent)
        {
            _previousHealthPercent = _playerData.HealthPct;
            _healthbarSlider.value = _previousHealthPercent;
            //Update the health bar UI here
            //For example, set the fill amount of a UI Image component to the health percent
            //GetComponent<Image>().fillAmount = _healthbarSource.HealthPercentNormalized;
        }
    }
}
