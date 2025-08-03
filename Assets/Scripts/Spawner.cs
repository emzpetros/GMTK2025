using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform enemeyPrefab;

    private float spawnInterval = 5f;
    private float spawnTimer;
    private int spawnMax = 5;
    private int spawnCount = 0;
    private float size = 1;
    private void Start() {
        spawnTimer = spawnInterval;
    }

    private void Update() {
        if(spawnCount < spawnMax) {
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0) {
                Spawn();
                spawnTimer = spawnMax;
            }
        }

    }

    private void Spawn() {
        Instantiate(enemeyPrefab, this.transform);
        spawnCount++;
    }

    public void SetTimer(float interval) {
        spawnInterval = interval;
    }

    public void SetSize(float size) {
        this.size = size;
    }
}
