using UnityEngine;

public class Destroy : MonoBehaviour
{
    private BoxCollider2D boxCollider;

    private void Start() {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Destroy(collision.gameObject);
    }
}
