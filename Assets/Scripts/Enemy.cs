using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public static event EventHandler OnAnyEnemeyRun;
    public static event EventHandler OnAnyEnemyDeath;

    private SpriteRenderer sprite;
    private Animator animator;
    [SerializeField] private Transform trueBlockPrefab;
    [SerializeField] private Transform falseBlockPrefab;
    [SerializeField] private Transform numBlockPrefab;

    [Header("Drop Rates (they must add up to 1.0 or 100%)")]
    [Range(0, 1)] public float trueBlockChance = 0.15f;   // Example: 30%
    [Range(0, 1)] public float falseBlockChance = 0.15f;  // Example: 40%
    // The remainder is for numBlockPrefab

    private float speed = 0.4f;              // Movement speed, adjustable in Inspector
    private float distance = 3f;           // Distance to move from start point
    private float minSwitchTime = 3f;      // Minimum time before switching direction
    private float maxSwitchTime = 10f;      // Maximum time before switching direction

    private Vector3 startPos;
    private int direction = 1;            // 1 for right, -1 for left
    private float switchTimer;
    private bool alive = true;
    void Start() {
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        startPos = transform.position;
        SetRandomSwitchTime();
    }

    void Update() {

        if (alive) {
            // Move the object
            transform.position += Vector3.right * direction * speed * Time.deltaTime;

            if (direction < 0) {
                sprite.flipX = false; // Or true, if you want right to be flipped
            }
            else if (direction > 0) {
                sprite.flipX = true;  // Or false, adjust for your art
            }

            // If out of bounds, reverse direction
            if (Mathf.Abs(transform.position.x - startPos.x) > distance) {
                direction *= -1;
                ClampToRange();
                SetRandomSwitchTime();
            }

            // Timer for random direction change
            switchTimer -= Time.deltaTime;
            if (switchTimer <= 0f) {
                direction *= -1; // Switch direction randomly
                SetRandomSwitchTime();
            }

            OnAnyEnemeyRun?.Invoke(this, EventArgs.Empty);
        }
       
    }

    void SetRandomSwitchTime() {
        switchTimer = UnityEngine.Random.Range(minSwitchTime, maxSwitchTime);
    }

    void ClampToRange() {
        // Clamp position to stay within allowed range
        float clampedX = Mathf.Clamp(transform.position.x, startPos.x - distance, startPos.x + distance);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (alive){
            if (collision.gameObject.name == "Player") {
                Player.Instance.death();
            }
        }
        
    }

    public void death() {
        alive = false;
        Debug.Log("enemy die");
        OnAnyEnemyDeath?.Invoke(this, EventArgs.Empty); 
        StartCoroutine(DeathEffects());


       //animate, drop item
    }

    IEnumerator DeathEffects() {
        animator.SetBool("death", true);
        yield return new WaitForSeconds(1f);
        DropRandomBlock(); // Spawn one of the prefabs here

        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }

    private void DropRandomBlock() {
        float rand = UnityEngine.Random.value; // value between 0 and 1

        Transform prefabToDrop = null;
        if (rand < trueBlockChance) {
            prefabToDrop = trueBlockPrefab;
        }
        else if (rand < trueBlockChance + falseBlockChance) {
            prefabToDrop = falseBlockPrefab;
        }
        else {
            prefabToDrop = numBlockPrefab;
        }

        if (prefabToDrop != null)
            Instantiate(prefabToDrop, transform.position, Quaternion.identity);
    }
}
