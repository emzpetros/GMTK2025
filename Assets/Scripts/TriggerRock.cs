using System.Collections;
using UnityEngine;

public class TriggerRock : MonoBehaviour
{
    [SerializeField] private GameObject rock;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            rock.GetComponent<Rigidbody2D>().simulated = true;
            StartCoroutine(PlaySound());
        }
    }

    IEnumerator PlaySound() {

        rock.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(1.5f);
        rock.GetComponent<AudioSource>().Stop();
    }
}
