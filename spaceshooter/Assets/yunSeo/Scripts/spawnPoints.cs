using UnityEngine;

public class spawnPoints : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public GameObject enemyA;
    public GameObject enemyB;
    public GameObject enemyC;

    [Header("Spawn Points")]
    public Transform spawnPoint_0;
    public Transform spawnPoint_1;
    public Transform spawnPoint_2;
    public Transform spawnPoint_3;
    public Transform spawnPoint_4;

    [Header("Spawn Settings")]
    public float spawnInterval = 2f;

    private float delta = 0;
    private GameObject[] enemies;
    private Transform[] spawnPointList;

    void Start()
    {
        // 프리팹 배열 구성 (null 제외)
        enemies = new GameObject[] { enemyA, enemyB, enemyC };
        spawnPointList = new Transform[] { spawnPoint_0, spawnPoint_1, spawnPoint_2, spawnPoint_3, spawnPoint_4 };
    }

    void Update()
    {
        delta += Time.deltaTime;

        if (delta >= spawnInterval)
        {
            delta = 0;
            SpawnEnemy();
        }
    }

    // spawnPoints_0~4까지에서 enemy가 랜덤하게 스폰
    // spawnPoints에서 가만히 있는게 아닌 스폰되고나서 아래로 천천히 떨어져 감
    private void SpawnEnemy()
    {
        if (enemies == null || spawnPointList == null) return;

        // null이 아닌 스폰 포인트 중 랜덤 선택
        System.Collections.Generic.List<Transform> validPoints = new System.Collections.Generic.List<Transform>();
        foreach (var sp in spawnPointList)
            if (sp != null) validPoints.Add(sp);

        if (validPoints.Count == 0) return;

        // null이 아닌 에너미 프리팹 중 랜덤 선택
        System.Collections.Generic.List<GameObject> validEnemies = new System.Collections.Generic.List<GameObject>();
        foreach (var e in enemies)
            if (e != null) validEnemies.Add(e);

        if (validEnemies.Count == 0) return;

        Transform spawnPoint = validPoints[Random.Range(0, validPoints.Count)];
        GameObject prefab = validEnemies[Random.Range(0, validEnemies.Count)];

        Instantiate(prefab, spawnPoint.position, Quaternion.identity);
    }
}
