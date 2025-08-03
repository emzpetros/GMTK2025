using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform enemeyPrefab;

    private float spawnInterval = 5f;
    private float spawnTimer;
    //private int spawnMax = 5;
    private int spawnCount = 0;
    private float size = 1;
    private void Start() {
        spawnTimer = spawnInterval;
    }

    private void Update() {
       // if(spawnCount < spawnMax) {
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0) {
                Spawn();
                spawnTimer = spawnInterval;
            }
        //}

    }

    private void Spawn() {
        Transform enemy = Instantiate(enemeyPrefab, this.transform);
        enemy.localScale = 1.2f * size * Vector3.one;
        spawnCount++;
    }

    public void SetTimer(float interval) {
        spawnInterval = interval;
    }

    public void SetSize(float size) {
        this.size = size;
    }
}
