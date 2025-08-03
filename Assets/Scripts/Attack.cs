using System;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private bool allowCollisions = false;
    public EventHandler OnAttackedEnemy;
    public EventHandler OnAttackedBreak;

    private float shiftAmount = 0.5f;
    private bool flipped = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Player.Instance.OnAllowedAttack += Player_AllowedAttack;
        //Player.Instance.OnAttackCooldown += Player_AttackCooldown;
    }

    private void Player_AttackCooldown(object sender, EventArgs e) {
        allowCollisions = false;
    }
    private void Player_AllowedAttack(object sender, EventArgs e) {
        allowCollisions = true;
    }

    // Update is called once per frame
    void Update()
    {
        float moveInput = Player.Instance.GetMoveInput();
        if (moveInput > 0) {
            transform.localPosition = new Vector3(-0.259f + shiftAmount, -0.054f, 0);
        } else if(moveInput < 0) {
            transform.localPosition = new Vector3(-0.259f , -0.054f, 0);
        }
    }
    private void OnTriggerStay2D(Collider2D collision) {
        Debug.Log(allowCollisions);
        if (allowCollisions) {
            Debug.Log("checking for collision");
            GameObject attackTarget = collision.gameObject;
            if (attackTarget.tag == "Enemy") {
                Debug.Log("enemey attack");
                attackTarget.GetComponent<Enemy>().death();
                OnAttackedEnemy?.Invoke(this, EventArgs.Empty); //unused for now, will be sound
            } else if (attackTarget.tag == "Break") {
                OnAttackedBreak?.Invoke(this, EventArgs.Empty); // trigger endgame state
                Debug.Log("break attacked");
            }
            allowCollisions = false;
        }
    }
  /*  private void OnTriggerEnter2D(Collider2D collider) {
        Debug.Log(allowCollisions);
        if (allowCollisions) {
            Debug.Log("checking for collision");
            GameObject attackTarget = collider.gameObject;
            if(attackTarget.tag == "Enemy") {
                Debug.Log("enemey attack");
                attackTarget.GetComponent<Enemy>().death();
                OnAttackedEnemy?.Invoke(this, EventArgs.Empty); //unused for now, will be sound
            } else if(attackTarget.tag == "Break"){
                OnAttackedBreak?.Invoke(this, EventArgs.Empty); // trigger endgame state
                Debug.Log("break attacked");
            }
            allowCollisions = false; 
        }
    }*/
}
