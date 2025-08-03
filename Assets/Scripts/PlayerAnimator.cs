using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private const string JUMP_VAR = "isJumping";
    private const string MOVE_VAR = "moveInput";



 
    private void Awake() {
        animator = GetComponent<Animator>();

    }
    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();

        Player.Instance.OnJump += Player_OnJump;
        Player.Instance.OnLand += Player_OnLand;


   


    }

    private void Player_OnLand(object sender, EventArgs e) {
        animator.SetBool(JUMP_VAR, false);
    }

    private void Player_OnJump(object sender, EventArgs e) {
        animator.SetBool(JUMP_VAR, true);
    }

    

    private void Update() {


        float moveInput = Player.Instance.GetMoveInput();

        if(moveInput < 0) {
            spriteRenderer.flipX = false;
        } else if(moveInput > 0){
            spriteRenderer.flipX = true;
        }

        animator.SetFloat(MOVE_VAR, Math.Abs(moveInput));
    }
}
