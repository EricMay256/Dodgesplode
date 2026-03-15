using UBear.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UBear.InputSystem
{
    /// <summary>
    /// Handles core player input using Unity's Input System and broadcasts actions via GameEvents.
    /// This component handles core movement inputs (Move, Look, Jump, Sprint).
    /// </summary>
    public class CoreInputHandler : MonoBehaviour
    {
        #region Fields

        [Header("Core Game Events")]
        [Tooltip("Raised when the player provides movement input.")]
        [SerializeField] private Vector2Event onMoveInput;
        [Tooltip("Raised when the player provides look/camera input.")]
        [SerializeField] private Vector2Event onLookInput;
        [Tooltip("Raised when the jump button is pressed.")]
        [SerializeField] private GameEvent onJumpPressed;
        [Tooltip("Raised when the jump button is released.")]
        [SerializeField] private GameEvent onJumpReleased;
        [Tooltip("Raised when the sprint state changes (pressed or released).")]
        [SerializeField] private BoolEvent onSprintStateChanged;
        [Tooltip("Raised when the primary action button is pressed.")]
        [SerializeField] private GameEvent onInteractActionPressed;
        [Tooltip("Raised when the primary action button is released.")]
        [SerializeField] private GameEvent onInteractReleased;
        [Tooltip("Raised when the menu button is pressed.")]
        [SerializeField] private GameEvent onMenuPressed;

        private UBearInputActions m_InputActions;

        #endregion

        #region Unity Lifecycle & Network Callbacks

        private void Awake()
        {
            m_InputActions = new UBearInputActions();
        }

        public void OnEnable()
        {
            if (m_InputActions != null)
            {
                RegisterInputActions();
                m_InputActions.Player.Enable();
            }
        }

        public void OnDisable()
        {
            if (m_InputActions != null)
            {
                m_InputActions.Player.Disable();
                UnregisterInputActions();
            }
        }

        #endregion

        #region Input Registration

        private void RegisterInputActions()
        {
            m_InputActions.Player.Move.performed += HandleMove;
            m_InputActions.Player.Move.canceled += HandleMove;

            m_InputActions.Player.Look.performed += HandleLook;
            m_InputActions.Player.Look.canceled += HandleLook;

            m_InputActions.Player.Jump.performed += HandleJumpPressed;
            m_InputActions.Player.Jump.canceled += HandleJumpReleased;

            m_InputActions.Player.Sprint.started += HandleSprintState;
            m_InputActions.Player.Sprint.canceled += HandleSprintState;

            m_InputActions.Player.Interact.started += HandleInteractPressed;
            m_InputActions.Player.Interact.canceled += HandleInteractReleased;

            m_InputActions.Player.Menu.performed += HandleMenuPressed;
        }

        private void UnregisterInputActions()
        {
            m_InputActions.Player.Move.performed -= HandleMove;
            m_InputActions.Player.Move.canceled -= HandleMove;

            m_InputActions.Player.Look.performed -= HandleLook;
            m_InputActions.Player.Look.canceled -= HandleLook;

            m_InputActions.Player.Jump.performed -= HandleJumpPressed;
            m_InputActions.Player.Jump.canceled -= HandleJumpReleased;

            m_InputActions.Player.Sprint.started -= HandleSprintState;
            m_InputActions.Player.Sprint.canceled -= HandleSprintState;

            m_InputActions.Player.Interact.started -= HandleInteractPressed;
            m_InputActions.Player.Interact.canceled -= HandleInteractReleased;

            m_InputActions.Player.Menu.performed -= HandleMenuPressed;
        }

        #endregion

        #region Input Handlers

        private void HandleMove(InputAction.CallbackContext context) => onMoveInput?.Raise(context.ReadValue<Vector2>());
        private void HandleLook(InputAction.CallbackContext context) => onLookInput?.Raise(context.ReadValue<Vector2>());
        private void HandleJumpPressed(InputAction.CallbackContext context) => onJumpPressed?.Raise();
        private void HandleJumpReleased(InputAction.CallbackContext context) => onJumpReleased?.Raise();
        private void HandleSprintState(InputAction.CallbackContext context) => onSprintStateChanged?.Raise(context.ReadValueAsButton());
        private void HandleInteractPressed(InputAction.CallbackContext context) => onInteractActionPressed?.Raise();
        private void HandleInteractReleased(InputAction.CallbackContext context) => onInteractReleased?.Raise();
        private void HandleMenuPressed(InputAction.CallbackContext context) => onMenuPressed?.Raise();

        #endregion
    }
}
