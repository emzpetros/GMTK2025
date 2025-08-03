using NUnit.Framework;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public EventHandler OnJump;
    public EventHandler OnLand;
    public EventHandler OnDeath;
    public EventHandler OnAttackedEnemy;
    public EventHandler OnAttackedBreak;
    public EventHandler OnItemUsed;
    public EventHandler OnItemInvalid;

    public UnityEvent<string> OnItemPickedUp;

    public EventHandler OnAttack;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform InteractCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask interactLayer;

    [SerializeField] private Transform attackPoint;


    private GameInput gameInput;

    private Rigidbody2D rigidBody;
    private CircleCollider2D circleCollider;

    private IBlock item = null;

    private float moveInput;

    private const int MOVE_SPEED = 3;
    private const int JUMP_POWER = 440;
    private const float GROUND_CHECK_RADIUS = 0.2f;
    private const float ATTACK_COOLDOWN = 0.01f;
    private float attackTimer = ATTACK_COOLDOWN;
    private bool canJump = false;
    private float jumpLevelIndex = 2;
    private float[] jumpLevels = { 0,350f,400f,440f,600f, 650f, 750f, 800f, 850f, 900f};
    //private float attackTimer = ATTACK_COOLDOWN;


    private bool grounded = true;
    private bool jump = false;
    private bool canAttack = true;
    //private float shiftAmount = 0.5f;
    private const float PLAYER_ACTIVE_DELAY = 2f;

    private bool canUncomment = false;
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
            IInteractableCodeLine blockSlot = null;

            if (hit) {
                blockSlot = collider.gameObject.GetComponent<IInteractableCodeLine>();
                    /*bool isBool = collider.gameObject.TryGetComponent<BooleanCodeLineInteracable>(out blockSlot);
                    if (isBool) {
                        blockSlot = collider.gameObject.GetComponent<BooleanCodeLineInteracable>() as BooleanCodeLineInteracable;
                    }
                    else { 
                         blockSlot = collider.gameObject.GetComponent<NumberCodeLineInteractable>() as NumberCodeLineInteractable;
                    }*/


                    bool itemUsed = blockSlot.TrySetText(item);
                if (itemUsed) {
                    item = null;
                    OnItemUsed?.Invoke(this, EventArgs.Empty);
                    Debug.Log("item used");

                } else {
                    Debug.Log("item invalid");
                    OnItemInvalid?.Invoke(this, EventArgs.Empty);   
                }
            } else {
                Debug.Log("not on an interactable area");
            }
        } else {
            Debug.Log("No item");
        }

 

        
    }

    private void GameInput_OnAttackInput(object sender, EventArgs e) {
        if (canAttack) {

            CircleCollider2D attackCollider = attackPoint.gameObject.GetComponent<CircleCollider2D>();
            Collider2D[] hits = Physics2D.OverlapCircleAll(attackCollider.bounds.center, attackCollider.radius);
            Debug.Log("attack");
            OnAttack?.Invoke(this, e);
            foreach (var hit in hits) {
                Debug.Log("HIT: " + hit.gameObject.name);
                if (hit.CompareTag("Enemy")) {
                    hit.GetComponent<Enemy>().death();
                    
                    OnAttackedEnemy?.Invoke(this, EventArgs.Empty);
                }
                else if (hit.CompareTag("Break")) {
                    Debug.Log("break break");
                    OnAttackedBreak?.Invoke(this, EventArgs.Empty);
                }
            }
            canAttack = false;
            attackTimer = ATTACK_COOLDOWN;
        } 

    }
    private void Update() {
        if (!canAttack) {
            attackTimer -= Time.deltaTime;
            if(attackTimer < 0) { 
                attackTimer = ATTACK_COOLDOWN;
                canAttack = true;
            }
        }

        if (moveInput > 0) {
            attackPoint.transform.localPosition = new Vector3(+0.397f, -0.054f, 0);
        }
        else if (moveInput < 0) {
            attackPoint.transform.localPosition = new Vector3(-0.397f, -0.054f, 0);
        }

        // Debug.Log(rigidBody.linearVelocity);

        if (Input.GetKeyDown(KeyCode.E)) {
            OnDeath?.Invoke(this, EventArgs.Empty); 
            /*if (item != null) {
                Debug.Log(item.GetValue().ToString() + item.GetBlockType());
            }
            else {
                Debug.Log("null");
            }*/
        }
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
            rigidBody.AddForceY(jumpLevels[(int)jumpLevelIndex]);
        }

        jump = false;
    }

    private void HandleMovement() {
        moveInput = gameInput.GetMovementInput();
        rigidBody.linearVelocityX = moveInput * MOVE_SPEED;

    }

    private void GameInput_OnJumpInput(object sender, EventArgs e) {
        if (canJump) {

            jump = true;
            OnJump?.Invoke(this, EventArgs.Empty);
        }
    }

    public void SetItem(IBlock item) {
        OnItemPickedUp?.Invoke(item.GetValue().ToString().ToLower());
        this.item = item;
    }

    public float GetMoveInput() {
        return moveInput;
    }

    public void SetCanJump(bool value) {
        canJump = value;
    }

    public void death() {
        OnDeath?.Invoke(this, EventArgs.Empty);
    }

    public void SetBreakAbility(bool state) {
        this.canUncomment = state;
    }

    public void SetJumpLevel(float level) {
        this.jumpLevelIndex = level;
    }
}
