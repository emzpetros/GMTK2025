using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Break : MonoBehaviour
{
    private void Start() {
        Player.Instance.OnAttackedBreak += Player_OnAttackBreak;
    }

    private void Player_OnAttackBreak(object sender, EventArgs e) {
        StartCoroutine(BreakEffects());
    }

    IEnumerator BreakEffects() {
        TextMeshProUGUI text = this.GetComponent<TextMeshProUGUI>();
        text.text = "break";
        yield return new WaitForSeconds(1f);
        text.color = Color.white;

        float duration = 3f;
        float elapsed = 0f;

        Vector3 originalScale = transform.localScale;
        Vector3 targetScale = originalScale * 5f; // scale up to 2x size

        Quaternion originalRotation = transform.rotation;
        float rotations = 3f; // Number of full rotations over duration

        while (elapsed < duration) {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            // Scale up smoothly (linear interpolation)
            transform.localScale = Vector3.Lerp(originalScale, targetScale, t);

            // Spin around Z axis smoothly
            transform.localRotation = originalRotation * Quaternion.Euler(0f, 0f, 360f * rotations * t);

            yield return null;
        }

        // Optional: make sure it ends exactly at target scale and rotation
        transform.localScale = targetScale;
        transform.rotation = originalRotation * Quaternion.Euler(0f, 0f, 360f * rotations);
        yield return new WaitForSeconds(1f);
        // Load next scene
        SceneManager.LoadScene("End");
    }

}
