using NUnit.Framework;
using System;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform InteractCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask interactLayer;

    private GameInput gameInput;

    private Rigidbody2D rigidBody;
    private IBlock item = null;

    private float moveInput;

    private const int MOVE_SPEED = 10;
    private const int JUMP_POWER = 600;
    private const float GROUND_CHECK_RADIUS = 0.2f;
    private const float ATTACK_COOLDOWN = 1.5f;
    private float attackTimer = ATTACK_COOLDOWN;


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

        if(item != null){
            Collider2D collider = Physics2D.OverlapCircle(InteractCheck.position, GROUND_CHECK_RADIUS, interactLayer);
            bool hit = collider != null; // true if something was found
            CodeLineInteractable blockSlot = null;

            if (hit) {
                blockSlot = collider.gameObject.GetComponent<CodeLineInteractable>();
                bool itemUsed = blockSlot.TrySetText(item);
                if (itemUsed) {
                    item = null;
                    Debug.Log("item used");
                } else {
                    Debug.Log("item invalid");
                }
            } else {
                Debug.Log("not on an interactable area");
            }
        } else {
            Debug.Log("No item");
        }

 

        
    }

    private void GameInput_OnAttackInput(object sender, EventArgs e) {
        if(canAttack) {
            Debug.Log("Attack!");
            canAttack = false;
        }
    }
    private void Update() {
        if(!canAttack) {
            attackTimer -= Time.deltaTime;
            if (attackTimer < 0) {
                canAttack = true;
                attackTimer = ATTACK_COOLDOWN;
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
        moveInput = gameInput.GetMovementInput();
        rigidBody.linearVelocityX = moveInput * MOVE_SPEED;

    }

    private void GameInput_OnJumpInput(object sender, EventArgs e) {
        jump = true;
    }

    public void SetItem(IBlock item) {
        this.item = item;
    }

    public float GetMoveInput() {
        return moveInput;
    }

}
