using UnityEngine;

public class Enemy : MonoBehaviour {
    private SpriteRenderer sprite;

    public float speed = 0.5f;              // Movement speed, adjustable in Inspector
    public float distance = 3f;           // Distance to move from start point
    public float minSwitchTime = 3f;      // Minimum time before switching direction
    public float maxSwitchTime = 5f;      // Maximum time before switching direction

    private Vector3 startPos;
    private int direction = 1;            // 1 for right, -1 for left
    private float switchTimer;

    void Start() {
        sprite = GetComponent<SpriteRenderer>();
        startPos = transform.position;
        SetRandomSwitchTime();
    }

    void Update() {
        // Move the object
        transform.position += Vector3.right * direction * speed * Time.deltaTime;

        if(transform.forward.x > 0) {
            sprite.flipX = true;
        }else if(transform.forward.x < 0) { 
            sprite.flipX = false; }

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
    }

    void SetRandomSwitchTime() {
        switchTimer = Random.Range(minSwitchTime, maxSwitchTime);
    }

    void ClampToRange() {
        // Clamp position to stay within allowed range
        float clampedX = Mathf.Clamp(transform.position.x, startPos.x - distance, startPos.x + distance);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.name == "Player") {
            Player.Instance.death();
        }
    }
}
