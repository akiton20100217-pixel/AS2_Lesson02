using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Spawner : MonoBehaviour
{

    [SerializeField] 
    public GameObject enemy;
    public Transform SpawnPointPre;

    public float spawnInterval = 3f;
    public float spawnTimer = 0f;

    void Start()
    {
        
    }

    void Update()
    {
        SpawnTimer();
        SpawnPoint();
    }

    void SpawnTimer()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            Instantiate(enemy, transform.position, transform.rotation);
            spawnTimer = 0f;
        }
    }

    private void SpawnPoint()
    {
        Player player = GameObject.FindAnyObjectByType<Player>();
        float playerZ = player.transform.position.z;

        Vector3 randomPos = Vector3.zero;
        randomPos.x = Random.Range(-8f, 8f);
         
        randomPos.z = playerZ + 100;
        Instantiate(SpawnPointPre, randomPos, transform.rotation);
    }
}
