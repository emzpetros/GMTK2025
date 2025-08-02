using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform enemeyPrefab;

    private const float spawnInterval = 2f;
    private float spawnTimer = spawnInterval;
    private int spawnMax = 3;
    private int spawnCount = 0;
    

    private void Update() {
        if(spawnCount < spawnMax) {
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0) {
                Spawn();
            }
        }

    }

    private void Spawn() {
        Instantiate(enemeyPrefab, this.transform);
        spawnCount++;
    }
}
