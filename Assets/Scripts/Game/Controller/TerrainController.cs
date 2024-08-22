using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour
{
    public float TerrainSpeed { get { return terrainsSpeed; } }


    [SerializeField] PlayerController playerController;
    [SerializeField] float terrainDistance;
    [SerializeField] float terrainOffset;

    [SerializeField] float spawnTimer;
    [SerializeField] Material terrainMaterial;

    [SerializeField] Transform obstacleSpawnPosition;

    [SerializeField] bool startSpawnObstacle = false;

    [SerializeField] float terrainsSpeed;
    [SerializeField] float reduceTerainSpeed;

    [SerializeField] float reduceSpeedTime;

    private List<GameObject> terrainPool;
    private List<GameObject> obstaclePool;
    private List<GameObject> coinPool;
    private float timer;

    private int currentObstacleIndex = 0;

    private float speedTimer;
    private bool isSpeedReduce = false;


    private void Awake()
    {
        ResourceManager.instance.IntializeIslandObjectData();
    }

    // Start is called before the first frame update
    void Start()
    {
        terrainPool = PoolManager.instance.GetObjectPool("Terrain");
        obstaclePool = PoolManager.instance.GetObjectPool("Obstacle");
        coinPool = PoolManager.instance.GetObjectPool("Coin");

        timer = 0f;
        speedTimer = 0f;

        InitializeTerrain();

        GameEvents.OnChangeTerrainSpeed.Invoke(terrainsSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSpawnObstacle();
        UpdateSpeed();
    }

    public void StartSpawnObstacle(bool start)
    {
        startSpawnObstacle = start;
    }

    void InitializeTerrain()
    {
        float maxDistance = ( terrainPool.Count - 1 ) * terrainDistance;
        for (int i = 0; i < terrainPool.Count; i++)
        {
            terrainPool[i].SetActive(true);

            Vector3 pos = terrainPool[i].transform.position;
            pos.z += i * terrainDistance;
            terrainPool[i].transform.position = pos;

            TerrainObject to = terrainPool[i].GetComponent<TerrainObject>();

            to.SetMaterial(terrainMaterial);
            to.Initialized(maxDistance, -(terrainDistance + terrainOffset));
            to.StartUpdate(true);
        }
    }

    void UpdateSpawnObstacle()
    {
        if (!startSpawnObstacle)
            return;

        if(timer + spawnTimer < Time.time)
        {
            SpawnObstacle();
            timer = Time.time;
        }
    }

    void SpawnObstacle()
    {
        if (currentObstacleIndex >= obstaclePool.Count)
            currentObstacleIndex = 0;

        GameObject obstacle = obstaclePool[currentObstacleIndex];

        ObstacleObject oo = obstacle.GetComponent<ObstacleObject>();

        oo.ShowObstacle(obstacleSpawnPosition.position, 
            -(terrainDistance + terrainOffset));
        oo.StartUpdate(true);

        currentObstacleIndex++;
    }

    public void ReduceSpeed()
    {
        isSpeedReduce = true;
        speedTimer = Time.time;

        GameEvents.OnChangeTerrainSpeed.Invoke(reduceTerainSpeed);
    }

    public void UpdateSpeed()
    {
        if (isSpeedReduce)
        {
            if (speedTimer + reduceSpeedTime < Time.time)
            {
                GameEvents.OnChangeTerrainSpeed.Invoke(terrainsSpeed);
                speedTimer = Time.time;
                isSpeedReduce = false;
            }
        }
    }
}
