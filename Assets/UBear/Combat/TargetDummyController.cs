using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UBear.Combat
{
  public class TargetDummyController : MonoBehaviour
  {
    TargetDummy _targetDummy;
    [SerializeField] InputActionAsset _inputActions;
    InputAction _inputAction;

    void Awake()
    {
      _targetDummy = new TargetDummy(invulnerable: false, health: 100);
    }

    void OnEnable()
    {
      if (_inputActions != null)
      {
        _inputActions.Enable();
      }
      else
      {
        Debug.LogWarning("InputActionAsset is not assigned in the inspector.");
      }
    }

    void OnDisable()
    {
      if (_inputActions != null)
      {
        _inputActions.Disable();
      }
    }

    void Update()
    {
      if (_inputActions["Player/Jump"].triggered)
      {
        _targetDummy.TakeDamage(10);
        Debug.Log($"TargetDummy Health: {_targetDummy.CurHealth}/{_targetDummy.MaxHealth} ({_targetDummy.HealthRatio * 100}%)");
      }
    } 
  }
}
