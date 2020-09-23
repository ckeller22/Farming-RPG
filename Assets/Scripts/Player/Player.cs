
using UnityEditor;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class Player : SingletonMonobehavior<Player>
{
    // Movement Parameters
    private float inputX;
    private float inputY;
    private bool isWalking;
    private bool isRunning;
    private bool isIdle;
    private bool isCarrying = false;
    private ToolEffect toolEffect = ToolEffect.none;
    private bool isUsingToolRight;
    private bool isUsingToolLeft;
    private bool isUsingToolUp;
    private bool isUsingToolDown;
    private bool isLiftingToolRight;
    private bool isLiftingToolLeft;
    private bool isLiftingToolUp;
    private bool isLiftingToolDown;
    private bool isPickingRight;
    private bool isPickingLeft;
    private bool isPickingUp;
    private bool isPickingDown;
    private bool isSwingingToolRight;
    private bool isSwingingToolLeft;
    private bool isSwingingToolUp;
    private bool isSwingingToolDown;

    private Rigidbody2D rigidBody2D;

    private float movementSpeed;

    private bool _playerInputIsDisabled = false;

    public bool PlayerInputIsDisabled { get => _playerInputIsDisabled; set => _playerInputIsDisabled = value; }

    protected override void Awake()
    {
        base.Awake();

        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        #region Player Input

        ResetAnimationTriggers();

        PlayerMovementInput();

        PlayerWalkInput();

        EventHandler.CallMovementEvent(inputX, inputY, isWalking, isRunning, isIdle, isCarrying, toolEffect,
            isUsingToolRight, isUsingToolLeft, isUsingToolUp, isUsingToolDown,
            isLiftingToolRight, isLiftingToolLeft, isLiftingToolUp, isLiftingToolDown,
            isPickingRight, isPickingLeft, isPickingUp, isPickingDown,
            isSwingingToolRight, isSwingingToolLeft, isSwingingToolUp, isSwingingToolDown,
            false, false, false, false);

        #endregion
    }

    private void FixedUpdate()
    {
        PlayerMovement();
    }

    private void ResetAnimationTriggers()
    {
        toolEffect = ToolEffect.none;
        isUsingToolRight = false;
        isUsingToolLeft = false;
        isUsingToolUp = false;
        isUsingToolDown = false;
        isLiftingToolRight = false;
        isLiftingToolLeft = false;
        isLiftingToolUp = false;
        isLiftingToolDown = false;
        isPickingRight = false;
        isPickingLeft = false;
        isPickingUp = false;
        isPickingDown = false;
        isSwingingToolRight = false;
        isSwingingToolLeft = false;
        isSwingingToolUp = false;
        isSwingingToolDown = false;
    }

    private void PlayerMovementInput()
    {   
        // Detects player movement, manages animation triggers. 
        inputY = Input.GetAxisRaw("Vertical");
        inputX = Input.GetAxisRaw("Horizontal");


        if (inputX != 0 && inputY != 0)
        {
            inputX = inputX * 0.71f;
            inputY = inputY * 0.71f;
        }

        if (inputX != 0 || inputY != 0)
        {
            isRunning = true;
            isWalking = false;
            isIdle = false;
            movementSpeed = Settings.runningSpeed;

            // TODO: Grab current direction for saving game.
        }
        else if (inputX == 0 && inputY == 0)
        {
            isRunning = false;
            isWalking = false;
            isIdle = true;
        }
    }

    private void PlayerWalkInput()
    {   
        // Detects if either shift key is pressed, and sets triggers/animation to walk.
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
            isRunning = false;
            isWalking = true;
            movementSpeed = Settings.walkingSpeed;
        }
        else
        {
            isRunning = true;
            isWalking = false;
            movementSpeed = Settings.runningSpeed;
        }
    }

    private void PlayerMovement()
    {
        Vector2 move = new Vector2(inputX * movementSpeed * Time.deltaTime, inputY * movementSpeed * Time.deltaTime);
        rigidBody2D.MovePosition(rigidBody2D.position + move);
    }
}
