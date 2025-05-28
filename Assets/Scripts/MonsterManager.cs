using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    private float timer;
    private float distance;
    private int spawnIndex;

    public int monsterSpawnLimit = 10;
    public int monsterSpawnCount;

    private GameObject[] monsterSpawnPoints;
    private ObjectPool monsterPool;
    public GameObject[] monsterPrefabs;
    public Transform playerTransform;

    private void Start()
    {
        monsterPool = GetComponent<ObjectPool>();
        monsterSpawnPoints = GameObject.FindGameObjectsWithTag("MonsterSpawnPoint");
        monsterSpawnCount = 0;
    }

    private void Update()
    {
        MonsterSpawn();
    }

    public void MonsterSpawn()
    {
        if (monsterSpawnCount >= monsterSpawnLimit || playerTransform == null)
            return;

        if (monsterPrefabs == null || monsterPrefabs.Length == 0)
            return;

        timer += Time.deltaTime;

        if (timer >= 1.5f)
        {
            int maxAttempts = 10; // �ִ� 10���� �õ�
            int attempts = 0;
            bool spawned = false;

            while (attempts < maxAttempts)
            {
                spawnIndex = Random.Range(0, monsterSpawnPoints.Length);
                distance = Vector3.Distance(monsterSpawnPoints[spawnIndex].transform.position, playerTransform.position);

                if (distance >= 15f)
                {
                    monsterPool.prefab = monsterPrefabs[Random.Range(0, monsterPrefabs.Length)];
                    monsterPool.GetObject(monsterSpawnPoints[spawnIndex].transform.position, Quaternion.identity);
                    monsterSpawnCount++;
                    timer = 0;
                    spawned = true;
                    break;
                }

                attempts++;
            }

            if (!spawned)
            {
                Debug.LogWarning("���� ������ �����ϴ� ��ġ�� �����ϴ�.");
                timer = 0; // Ÿ�̸� �����ؼ� ���� �����ӿ� �ٽ� �õ�
            }
        }
    }
}
