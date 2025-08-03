using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
  [SerializeField] private AudioClipRefSO audioClipRefSO;

    private float volume = 1f;

    private void Awake() {
        Instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Player.Instance.OnAttack += Player_OnAttack;
        Player.Instance.OnDeath += Player_OnDeath;
        Player.Instance.OnJump += Player_OnJump;
        Player.Instance.OnItemPickedUp.AddListener(Player_OnItemPickedUp);
        Player.Instance.OnItemUsed += Player_OnItemUsed;
        Player.Instance.OnItemInvalid += Player_OnItemInvalid;
        Enemy.OnAnyEnemeyRun += Enemey_OnRun;
        Enemy.OnAnyEnemyDeath += Enemey_OnDeath;
    }

    private void Player_OnJump(object sender, EventArgs e) {
        Player player = Player.Instance;
        PlaySound(audioClipRefSO.jump, player.transform.position, 2f);
    }

    private void Update() {
        float moveInput = Player.Instance.GetMoveInput();
        if (moveInput != 0) {

            //aySound(audioClipRefSO.run, Player.Instance.transform.position);
        }
    }
    private void Player_OnItemPickedUp(string itemValue) {
        Player player = Player.Instance;
        PlaySound(audioClipRefSO.pickup, player.transform.position);
    }
    private void Player_OnItemUsed(object sender, EventArgs e) {
        Player player = Player.Instance;
        PlaySound(audioClipRefSO.success, player.transform.position);
    }

    private void Player_OnItemInvalid(object sender, EventArgs e) {
        Player player = Player.Instance;
        PlaySound(audioClipRefSO.failure, player.transform.position);
    }

    private void Enemey_OnRun(object sender, EventArgs e) {
        Enemy enemy = sender as Enemy;
        //PlaySound(audioClipRefSO.enemyRun, enemy.transform.position, 0.25f);
    }

    private void Enemey_OnDeath(object sender, EventArgs e) {
        Enemy enemy = sender as Enemy;
       PlaySound(audioClipRefSO.enemyDeath, enemy.transform.position);
    }

    private void Player_OnDeath(object sender, EventArgs e) {
        Player player = Player.Instance;
        PlaySound(audioClipRefSO.death, player.transform.position);
    }

    private void Player_OnAttack(object sender, EventArgs e) {
        Player player = Player.Instance;
        PlaySound(audioClipRefSO.attack, player.transform.position, 0.4f);
    }

    private void PlaySound(AudioClip audioClip, Vector3 pos, float volumeMult = 1f) {
        AudioSource.PlayClipAtPoint(audioClip, pos, volumeMult * volume);
    }
}
