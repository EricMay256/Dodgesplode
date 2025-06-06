using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
  public static PlayerInputManager Instance;
  public Vector2 Movement;
  public Vector2 LookDelta;
  public bool AttackPressed { get; private set; }
  public bool AttackHeld { get; private set; }
  public bool AttackReleased { get; private set; }
  public bool JumpPressed { get; private set; }
  public bool JumpHeld { get; private set; }
  public bool JumpReleased { get; private set; }
  public bool InteractPressed { get; private set; }
  public bool InteractHeld { get; private set; }
  public bool InteractReleased { get; private set; }
  public bool CrouchPressed { get; private set; }
  public bool CrouchHeld { get; private set; }
  public bool CrouchReleased { get; private set; }
  public bool SprintPressed { get; private set; }
  public bool SprintHeld { get; private set; }
  public bool SprintReleased { get; private set; }
  public bool PausePressed { get; private set; }
  public bool PauseHeld { get; private set; }
  public bool PauseReleased { get; private set; }
  private InputAction _attackAction, _jumpAction, _interactAction, _crouchAction, _sprintAction;
  private InputAction _pauseActionPlayer, _pauseActionUI;
  private InputAction _movementAction, _lookAction;
  private PlayerInput _playerInput;
  private InputProcessor _moveProcessor, _lookProcessor;
  float _movementScalar = 5f;//Scalar for normalized movement input
                             // Start is called once before the first execution of Update after the MonoBehaviour is created
  [SerializeField]
  private bool _logMovement = false;
  void Awake()
  {
    if (Instance == null)
    {
      Instance = this;
    }
    else
    {
      Destroy(gameObject);
    }
    _playerInput = GetComponent<PlayerInput>();
    //Warning: Locking the cursor breaks all UI interactions!
    //Cursor.lockState = CursorLockMode.Locked;
  }

  void Start()
  {
    _movementAction = _playerInput.actions["Move"];
    _lookAction = _playerInput.actions["Look"];
    _attackAction = _playerInput.actions["Attack"];
    _jumpAction = _playerInput.actions["Jump"];
    _interactAction = _playerInput.actions["Interact"];
    _crouchAction = _playerInput.actions["Crouch"];
    _sprintAction = _playerInput.actions["Sprint"];
    _pauseActionPlayer = _playerInput.actions["Player/Pause"];
    _pauseActionUI = _playerInput.actions["UI/Pause"];
    //SetLookScale(1);
  }

  // Update is called once per frame
  void Update()
  {
    AttackPressed = _attackAction.WasPressedThisFrame();
    AttackHeld = _attackAction.IsPressed();
    AttackReleased = _attackAction.WasReleasedThisFrame();

    JumpPressed = _jumpAction.WasPressedThisFrame();
    JumpHeld = _jumpAction.IsPressed();
    JumpReleased = _jumpAction.WasReleasedThisFrame();

    InteractPressed = _interactAction.WasPressedThisFrame();
    InteractHeld = _interactAction.IsPressed();
    InteractReleased = _interactAction.WasReleasedThisFrame();

    CrouchPressed = _crouchAction.WasPressedThisFrame();
    CrouchHeld = _crouchAction.IsPressed();
    CrouchReleased = _crouchAction.WasReleasedThisFrame();

    SprintPressed = _sprintAction.WasPressedThisFrame();
    SprintHeld = _sprintAction.IsPressed();
    SprintReleased = _sprintAction.WasReleasedThisFrame();


    PausePressed = _pauseActionPlayer.WasPressedThisFrame() || _pauseActionUI.WasPressedThisFrame();
    PauseHeld = _pauseActionPlayer.IsPressed() || _pauseActionUI.IsPressed();
    PauseReleased = _pauseActionPlayer.WasReleasedThisFrame() || _pauseActionUI.WasReleasedThisFrame();

    Movement = _movementAction.ReadValue<Vector2>();
    LookDelta = _lookAction.ReadValue<Vector2>();
    LookDelta.x = Mathf.Clamp(LookDelta.x, -50f, 50f);
    LookDelta.y = Mathf.Clamp(LookDelta.y, -50f, 50f);
    if (_logMovement)
    {
      Debug.Log($"Movement: {Movement}, LookDelta: {LookDelta}");
    }
  }

  //Not Currently Used
  public void SetLookScale(float scale)
  {
    _lookAction.ApplyParameterOverride("ScaleVector2:x", scale * _movementScalar, 0);
    _lookAction.ApplyParameterOverride("ScaleVector2:y", scale * _movementScalar, 0);
    for (int i = 1; i < _lookAction.bindings.Count; i++)
    {
      _lookAction.ApplyParameterOverride("ScaleVector2:x", scale, i);
      _lookAction.ApplyParameterOverride("ScaleVector2:y", scale, i);
    }
    _movementAction.ApplyParameterOverride("ScaleVector2:x", scale * _movementScalar);
    _movementAction.ApplyParameterOverride("ScaleVector2:y", scale * _movementScalar);
  }

  public void SetGameplayControlsActive(bool active)
  {
    if (active)
    {
      _playerInput.SwitchCurrentActionMap("Player");
      //Cursor.visible = false;//Note: Future gameplay may require cursor to be visible 
    }
    else
    {
      _playerInput.SwitchCurrentActionMap("UI");
      Cursor.visible = true;
    }
  }

}
