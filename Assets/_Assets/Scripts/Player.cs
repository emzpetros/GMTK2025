using NUnit.Framework;
using System;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private GameInput gameInput;

    private Rigidbody2D rigidBody;
    private IBlock item = null;

    private const int MOVE_SPEED = 10;
    private const int JUMP_POWER = 600;
    private const float GROUND_CHECK_RADIUS = 0.2f;
    private float attackCooldown = 1.5f;


    private bool grounded = true;
    private bool jump = false;
    private bool canAttack = true;

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
        gameInput.OnAttackInput += GameInput_OnAttackInput;
        gameInput.OnItemInput += GameInput_OnItemInput;
    }

    private void GameInput_OnItemInput(object sender, EventArgs e) {
        Debug.Log("Use item");
    }

    private void GameInput_OnAttackInput(object sender, EventArgs e) {
        if(canAttack) {
            Debug.Log("Attack!");
            canAttack = false;
        }
    }
    private void Update() {
        if(!canAttack) {
            attackCooldown -= Time.deltaTime;
            Debug.Log("On cooldown");
            if (attackCooldown < 0) {
                canAttack = true;
            }
        }

        if(Input.GetKeyDown(KeyCode.E)) { 
            if(item != null) {
                Debug.Log(item.GetValue().ToString() + item.GetBlockType());
            } else {
                Debug.Log("null");
            }
        }
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

    public void SetItem(IBlock item) {
        this.item = item;
    }


}
