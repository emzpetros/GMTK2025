using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAnimator : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private const string JUMP_VAR = "isJumping";
    private const string MOVE_VAR = "moveInput";
    private const string DEATH_VAR = "death";
    private const string ATTACK_VAR = "attack";
        
 
    private void Awake() {
        animator = GetComponent<Animator>();

    }
    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();

        Player.Instance.OnJump += Player_OnJump;
        Player.Instance.OnLand += Player_OnLand;
        Player.Instance.OnDeath += Player_OnDeath;
        Player.Instance.OnAttack += Player_OnAttack;
   


    }

    private void Player_OnAttack(object sender, EventArgs e) {
        animator.SetTrigger(ATTACK_VAR);
    }

    private void Player_OnDeath(object sender, EventArgs e) {
        animator.SetBool(DEATH_VAR,true);
        StartCoroutine(DeathEffects());

    }
    IEnumerator DeathEffects() {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Death");
        Destroy(transform.parent.gameObject);
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
