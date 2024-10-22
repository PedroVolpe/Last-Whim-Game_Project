using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [Header("Input action Asset")]
    [SerializeField] private InputActionAsset playerControls;

    [Header("Map Name References")]
    [SerializeField] private string actionMapName = "Player";

    [Header("Action Name References")]
    [SerializeField] private string move = "Move";
    [SerializeField] private string dodge = "Dodge";
    [SerializeField] private string fire = "Fire";
    [SerializeField] private string interact = "Interact";

    private InputAction moveAction;
    private InputAction dodgeAction;
    private InputAction fireAction;
    private InputAction interactAction;


    public Vector2 MoveInput { get; private set; }
    public bool DodgeInput { get; private set; }
    public bool FireInput { get; private set; }
    public bool InteractInput { get; private set; }

    public static PlayerInputHandler Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(gameObject);

        moveAction = playerControls.FindActionMap(actionMapName).FindAction(move);
        dodgeAction = playerControls.FindActionMap(actionMapName).FindAction(dodge);
        fireAction = playerControls.FindActionMap(actionMapName).FindAction(fire);
        interactAction = playerControls.FindActionMap(actionMapName).FindAction(interact);
        RegisterInputActions();
    }

    void RegisterInputActions()
    {
        moveAction.performed += context => MoveInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => MoveInput = Vector2.zero;

        dodgeAction.performed += context => DodgeInput = true;
        dodgeAction.canceled += context => DodgeInput = false;

        fireAction.performed += context => FireInput = true;
        fireAction.canceled += context => FireInput = false;

        interactAction.performed += context => InteractInput = true;
        interactAction.canceled += context => InteractInput = false;
    }

    private void OnEnable()
    {
        moveAction.Enable();
        dodgeAction.Enable();
        fireAction.Enable();
        interactAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        dodgeAction.Disable();
        fireAction.Disable();
        interactAction.Disable();
    }
}
