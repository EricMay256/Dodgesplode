using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager Instance;
    public Vector2 Movement;
    public Vector2 LookDelta;
    public bool AttackPressed {get; private set;} 
    public bool AttackHeld {get; private set;}  
    public bool AttackReleased {get; private set;} 
    public bool JumpPressed {get; private set;} 
    public bool JumpHeld {get; private set;}  
    public bool JumpReleased{get; private set;} 
    public bool InteractPressed {get; private set;}  
    public bool InteractHeld {get; private set;}  
    public bool InteractReleased {get; private set;} 
    public bool CrouchPressed {get; private set;} 
    public bool CrouchHeld {get; private set;} 
    public bool CrouchReleased {get; private set;} 
    public bool SprintPressed {get; private set;}  
    public bool SprintHeld {get; private set;}  
    public bool SprintReleased{get; private set;} 
    private InputAction _attackAction, _jumpAction, _interactAction, _crouchAction, _sprintAction;
    private InputAction _movementAction, _lookAction;
    private PlayerInput _playerInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        _playerInput = GetComponent<PlayerInput>();
        Cursor.lockState = CursorLockMode.Locked;
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
    }

    // Update is called once per frame
    void Update()
    {
        AttackPressed = _jumpAction.WasPressedThisFrame();
        AttackHeld = _jumpAction.IsPressed();
        AttackReleased = _jumpAction.WasReleasedThisFrame();

        JumpPressed = _jumpAction.WasPressedThisFrame();
        JumpHeld = _jumpAction.IsPressed();
        JumpReleased = _jumpAction.WasReleasedThisFrame();
        
        InteractPressed = _jumpAction.WasPressedThisFrame();
        InteractHeld = _jumpAction.IsPressed();
        InteractReleased = _jumpAction.WasReleasedThisFrame();
        
        CrouchPressed = _jumpAction.WasPressedThisFrame();
        CrouchHeld = _jumpAction.IsPressed();
        CrouchReleased = _jumpAction.WasReleasedThisFrame();
        
        SprintPressed = _jumpAction.WasPressedThisFrame();
        SprintHeld = _jumpAction.IsPressed();
        SprintReleased = _jumpAction.WasReleasedThisFrame();

        Movement = _movementAction.ReadValue<Vector2>();
        LookDelta = _lookAction.ReadValue<Vector2>();

        //Keep the mouse position in the center of the screen
        
    }
}
