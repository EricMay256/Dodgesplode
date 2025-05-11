using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField]
    GameObject _healthbarSourceObject;
    IHealthbarSource _healthbarSource;
    Slider _healthbarSlider;
    float _previousHealthPercent = 1f;
    void Awake()
    {
        _healthbarSlider = GetComponent<Slider>();
        _healthbarSource = _healthbarSourceObject.GetComponent<IHealthbarSource>();
        if(_healthbarSlider == null || _healthbarSource == null)
        {
            Debug.LogError("Healthbar slider or source not found on the GameObject.");
            enabled = false; // Disable this script if dependencies are not found
        }
    }
    void Start()
    {
        _previousHealthPercent = _healthbarSource.HealthPercentNormalized;
        _healthbarSlider.value = _previousHealthPercent;
    }
    // Update is called once per frame
    void Update()
    {
        if(_healthbarSource.HealthPercentNormalized != _previousHealthPercent)
        {
            _previousHealthPercent = _healthbarSource.HealthPercentNormalized;
            _healthbarSlider.value = _previousHealthPercent;
            //Update the health bar UI here
            //For example, set the fill amount of a UI Image component to the health percent
            //GetComponent<Image>().fillAmount = _healthbarSource.HealthPercentNormalized;
        }
    }
}
