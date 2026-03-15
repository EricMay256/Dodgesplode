using UnityEngine;
using UBear.InputSystem;
using Unity.VisualScripting;

namespace UBear {
public class PlayerTest : MonoBehaviour
{
  [SerializeField] private Vector2Event _onMoveInput;
  Vector2 _currentMoveInput;
  [SerializeField] float _moveSpeed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        _onMoveInput.RegisterListener(HandleMoveInput);
    }

    void OnDisable()
    {
        _onMoveInput.UnregisterListener(HandleMoveInput);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_currentMoveInput * _moveSpeed * Time.deltaTime);
    }

    void HandleMoveInput(Vector2 moveInput)
    {
      _currentMoveInput = moveInput;
        Debug.Log($"Received Move Input: {moveInput}");
    }
}
}