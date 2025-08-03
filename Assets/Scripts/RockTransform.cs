using UnityEngine;

public class RockTransform : MonoBehaviour
{
    [SerializeField] private GameObject box;
    [SerializeField] private GameObject rock;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Rock")
        {
            box.GetComponent<Rigidbody2D>().AddForceX(2f);
            Destroy(rock);
        }
    }
}
