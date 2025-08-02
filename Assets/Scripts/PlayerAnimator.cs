using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update() {
        float moveInput = Player.Instance.GetMoveInput();

        if(moveInput < 0) {
            spriteRenderer.flipX = false;
        } else if(moveInput > 0){
            spriteRenderer.flipX = true;
        }
    }
}
