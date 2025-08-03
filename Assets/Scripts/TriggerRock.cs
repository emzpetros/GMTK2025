using UnityEngine;

public class TriggerRock : MonoBehaviour
{
    [SerializeField] private GameObject rock;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            rock.GetComponent<Rigidbody2D>().simulated = true;
        }
    }
}
