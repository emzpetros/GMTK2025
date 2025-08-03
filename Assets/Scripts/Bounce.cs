using UnityEngine;

public class BouncingSprite : MonoBehaviour {
    public float speed = 5f;               // Movement speed in units per second
    private Vector2 direction;             // Current movement direction
    private SpriteRenderer spriteRenderer;

    private float spriteWidth;
    private float spriteHeight;

    private Camera mainCamera;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Start moving in a random direction (either left or right)
        float startXDir = Random.value < 0.5f ? -1f : 1f;
        float startYDir = Random.Range(-1f, 1f);
        direction = new Vector2(startXDir, startYDir).normalized;

        mainCamera = Camera.main;

        // Calculate sprite size in world units
        if (spriteRenderer != null) {
            spriteWidth = spriteRenderer.bounds.size.x / 2;
            spriteHeight = spriteRenderer.bounds.size.y / 2;
        }
        else {
            spriteWidth = 0.5f;
            spriteHeight = 0.5f;
            Debug.LogWarning("No SpriteRenderer found on this GameObject.");
        }
    }

    void Update() {
        Vector3 position = transform.position;

        // Move the object
        position += (Vector3)(direction * speed * Time.deltaTime);

        // Get camera bounds in world coordinates
        Vector2 minScreenBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 maxScreenBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));

        // Bounce off left or right edges
        if (position.x - spriteWidth < minScreenBounds.x) {
            position.x = minScreenBounds.x + spriteWidth;
            direction.x = -direction.x;
        }
        else if (position.x + spriteWidth > maxScreenBounds.x) {
            position.x = maxScreenBounds.x - spriteWidth;
            direction.x = -direction.x;
        }

        // Bounce off top or bottom edges
        if (position.y - spriteHeight < minScreenBounds.y) {
            position.y = minScreenBounds.y + spriteHeight;
            direction.y = -direction.y;
        }
        else if (position.y + spriteHeight > maxScreenBounds.y) {
            position.y = maxScreenBounds.y - spriteHeight;
            direction.y = -direction.y;
        }

        transform.position = position;

        // Flip spriteRenderer based on horizontal direction
        if (direction.x > 0)
            spriteRenderer.flipX = true;
        else if (direction.x < 0)
            spriteRenderer.flipX = false;
    }
}
