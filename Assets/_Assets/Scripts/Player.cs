using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private GameInput gameInput;

    private Rigidbody2D rigidBody;

    private const int MOVE_SPEED = 10;
    private const int JUMP_POWER = 600;
    private const float GROUND_CHECK_RADIUS = 0.2f;

    private bool grounded = true;
    private bool jump = false;

    private void Awake() {
        if(Instance != null) {
            Debug.LogError("More than one player instance");
        }
        Instance = this;

        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        gameInput = GameInput.Instance;
        gameInput.OnJumpInput += GameInput_OnJumpInput;
    }


    private void FixedUpdate() {
        bool wasGrounded = grounded;
        grounded = Physics2D.OverlapCircle(groundCheck.position, GROUND_CHECK_RADIUS, groundLayer);

        HandleMovement();

        if(grounded && jump) {
            grounded = false;
            rigidBody.AddForceY(JUMP_POWER);
        }

        jump = false;
    }

    private void HandleMovement() {
        float moveInput = gameInput.GetMovementInput();
        rigidBody.linearVelocityX = moveInput * MOVE_SPEED;

    }

    private void GameInput_OnJumpInput(object sender, EventArgs e) {
        jump = true;
    }
}
