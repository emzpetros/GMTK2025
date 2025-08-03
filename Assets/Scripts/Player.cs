using NUnit.Framework;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public EventHandler OnJump;
    public EventHandler OnLand;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform InteractCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask interactLayer;

    private GameInput gameInput;

    private Rigidbody2D rigidBody;
    private CircleCollider2D circleCollider;

    private IBlock item = null;

    private float moveInput;

    private const int MOVE_SPEED = 3;
    private const int JUMP_POWER = 430;
    private const float GROUND_CHECK_RADIUS = 0.2f;
    private const float ATTACK_COOLDOWN = 1.5f;
    private float attackTimer = ATTACK_COOLDOWN;


    private bool grounded = true;
    private bool jump = false;
    private bool canAttack = true;

    private const float PLAYER_ACTIVE_DELAY = 1f;

    private void Awake() {
        if(Instance != null) {
            Debug.LogError("More than one player instance");
        }
        Instance = this;

        rigidBody = GetComponent<Rigidbody2D>();
        //rigidBody.simulated = false;
        //circleCollider = this.GetComponent<CircleCollider2D>();
        //circleCollider.enabled = false;
    }

    private void Start() {
        gameInput = GameInput.Instance;
        gameInput.OnJumpInput += GameInput_OnJumpInput;
        gameInput.OnAttackInput += GameInput_OnAttackInput;
        gameInput.OnItemInput += GameInput_OnItemInput;
        //StartCoroutine(ActivatePlayer());
    }

    private IEnumerator ActivatePlayer() {

        yield return new WaitForSeconds(PLAYER_ACTIVE_DELAY);

        circleCollider.enabled = true;
        rigidBody.simulated = true;
        Debug.Log("Active");
        rigidBody.linearVelocity = Vector2.zero;

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

        Debug.Log(rigidBody.linearVelocity);

        /*        if(Input.GetKeyDown(KeyCode.E)) { 
                    if(item != null) {
                        Debug.Log(item.GetValue().ToString() + item.GetBlockType());
                    } else {
                        Debug.Log("null");
                    }
                }*/
    }

    private void FixedUpdate() {
        bool wasGrounded = grounded;
        grounded = Physics2D.OverlapCircle(groundCheck.position, GROUND_CHECK_RADIUS, groundLayer);

        if (grounded && !wasGrounded) {
            OnLand?.Invoke(this, EventArgs.Empty);
        }
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
        OnJump?.Invoke(this, EventArgs.Empty);
    }

    public void SetItem(IBlock item) {
        this.item = item;
    }

    public float GetMoveInput() {
        return moveInput;
    }

}
